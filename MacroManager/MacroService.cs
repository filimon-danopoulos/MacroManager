using MacroManager.Hooks;
using MacroManager.Data;
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
        public MacroService(IHookService hookService, IMacroRepository macroRepository)
        {
            // TODO: Complete member initialization
            this.hookService = hookService;
            this.macroRepository = macroRepository;
        }

        #endregion

        #region Public methods

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
        public void StartRecording()
        {
            this.hookService.StartRecording();
            this.OnRecordingStarted();
        }

        /// <summary>
        /// Stopes recording the supplied Macro and saves it into the repository. 
        /// Fires the RecordingStopped event.
        /// </summary>
        public void StopRecording(string macroName, string macroDescription)
        {
            var macro = this.hookService.StopRecording();
            macro.Name = macroName;
            macro.Description = macroDescription;
            this.macroRepository.Add(macro);
            this.OnRecordingStopped();
        }

        /// <summary>
        /// Replays the supplied macro. 
        /// </summary>
        public void StartPlayback(Macro macro)
        {
            this.hookService.PlaybackMacroAsync(macro).ConfigureAwait(false);
        }

        public void StopPlayback()
        {
        }

        /// <summary>
        /// Returns all the macros from the Macro repository.
        /// </summary>
        public IEnumerable<Macro> GetAllMacros()
        {
            return this.macroRepository.Read();
        }

        /// <summary>
        /// Updates the repository of the service. 
        /// </summary>
        public void UpdateRepository(IMacroRepository macroRepository)
        {
            this.macroRepository = macroRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasChanges()
        {
            return this.macroRepository.HasChanges();
        }


        /// <summary>
        /// Saves all the changes that have been made.
        /// </summary>
        public void SaveChanges()
        {
            this.macroRepository.SaveChanges();
        }

        /// <summary>
        /// Saves changes to a specific file, only works when an XML-file is used as a persistence.
        /// </summary>
        /// 
        public void SaveChanges(string fileName)
        {
            if (!(this.macroRepository is XmlMacroRepository))
            {
                throw new Exception("The method SaveChanges(string fileName) only works if the service was initialized with an XmlMacroRepository.");
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
                handler(this, null);
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
                handler(this, null);
            }
        }

        /// <summary>
        /// Fires after playback starts.
        /// </summary>
        public event EventHandler PlaybackStarted;

        /// <summary>
        /// Wrapper for firing PlaybackStarted event including a null check on handler.
        /// </summary>
        private void OnPlaybackStarted()
        {
            var handler = this.PlaybackStarted;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Fires after playback stops.
        /// </summary>
        public event EventHandler PlaybackStoped;

        /// <summary>
        /// Wrapper for firing PlaybackStoped event including a null check on handler.
        /// </summary>
        private void OnPlaybackStoped()
        {
            var handler = this.PlaybackStoped;
            if (handler != null)
            {
                handler(this, null);
            }
        }



        #endregion

    }
}
