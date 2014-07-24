using MacroManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager
{
    public class MacroService
    {

        #region Fields

        /// <summary>
        /// A repository of macros. Used to persist changes
        /// </summary>
        private IMacroRepository macroRepository;
        /// <summary>
        /// A service for managing the low level hooks. 
        /// </summary>
        private IHookService hookService;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor, two dependencies are injected. 
        /// </summary>
        public MacroService(IHookService hookService)
        {
            this.hookService = hookService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new Macro. 
        /// </summary>
        public Macro CreateMacro(string name, string description = "")
        {
            return new Macro(name, description);
        }

        /// <summary>
        /// Starts recording a new Macro. 
        /// Fires the RecordingStarted event.
        /// </summary>
        public void StartRecording(Macro macro)
        {
            if (this.macroRepository == null)
            {
                throw new Exception("Can't start recording if the Macro repository has not been initialized. Run InitializeRepository.");
            }
            this.hookService.StartRecording(macro);
            this.OnRecordingStarted();
        }

        /// <summary>
        /// Stopes recording the supplied Macro and saves it into the repository. 
        /// Fires the RecordingStopped event.
        /// </summary>
        public void StopRecording(Macro macro)
        {
            if (this.macroRepository == null)
            {
                throw new Exception("Can't start recording if the Macro repository has not been initialized. Run InitializeRepository.");
            }
            this.hookService.StopRecording();
            this.macroRepository.Add(macro);
            this.OnRecordingStopped();
        }

        /// <summary>
        /// Replays the supplied macro. 
        /// </summary>
        public void ReplayMacro(Macro macro)
        {
            if (this.macroRepository == null)
            {
                throw new Exception("Can't start recording if the Macro repository has not been initialized. Run InitializeRepository.");
            }
            this.hookService.ReplayMacro(macro);
        }

        /// <summary>
        /// Returns all the macros from the Macro repository.
        /// </summary>
        public IEnumerable<Macro> GetAllMacros()
        {
            if (this.macroRepository == null)
            {
                throw new Exception("Can't start recording if the Macro repository has not been initialized. Run InitializeRepository.");
            }
            return this.macroRepository.Read();
        }

        /// <summary>
        /// Initializes the repository of the service. 
        /// </summary>
        public void IntitializeRepository(IMacroRepository macroRepository)
        {
            this.macroRepository = macroRepository;
        }

        /// <summary>
        /// Saves all the changes that have been made.
        /// </summary>
        public void SaveChanges()
        {
            if (this.macroRepository == null)
            {
                throw new Exception("Can't start recording if the Macro repository has not been initialized. Run InitializeRepository.");
            }
            this.macroRepository.SaveChanges();
        }

        /// <summary>
        /// Saves changes to a specific file, only works when an XML-file is used as a persistence.
        /// </summary>
        public void SaveChanges(string fileName)
        {
            if (this.macroRepository == null)
            {
                throw new Exception("Can't start recording if the Macro repository has not been initialized. Run InitializeRepository.");
            }
            if (!(this.macroRepository is XmlMacroRepository))
            {
                throw new Exception("The save changes with a fileName parameter method only works if the repository is an XmlMacroRepository.");
            }
            var existingMacros = this.macroRepository.Read();
            this.macroRepository = new XmlMacroRepository(fileName, true);
            foreach (var macro in existingMacros)
            {
                this.macroRepository.Add(macro);
            }
            this.macroRepository.SaveChanges();
        }

        /// <summary>
        /// Removes a macro from presistent storage.
        /// </summary>
        public void RemoveMacro(Macro macro)
        {
            this.macroRepository.Remove(macro);
        }

        #endregion

        #region Events

        /// <summary>
        /// This event indicates that the recording of a new Macro has started.
        /// </summary>
        public event EventHandler RecordingStarted;
        /// <summary>
        /// Wrapper for firing the RecordingStarted event that includes null checking.
        /// </summary>
        private void OnRecordingStarted()
        {
            var handler = RecordingStarted;
            if (handler != null)
            {
                this.RecordingStarted(this, null);
            }
        }

        /// <summary>
        /// This event indicates that the recording of a Macro is over.
        /// </summary>
        public event EventHandler RecordingStopped;
        /// <summary>
        /// Wrapper for firing the RecordingStoped event that includes null checking.
        /// </summary>
        private void OnRecordingStopped()
        {
            var handler = RecordingStopped;
            if (handler != null)
            {
                this.RecordingStopped(this, null);
            }
        }

        #endregion


    }
}
