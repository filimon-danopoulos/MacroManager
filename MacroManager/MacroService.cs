using MacroManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager
{
    public class MacroService : IMacroService
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
        public MacroService(IMacroRepository macroRepository, IHookService hookService)
        {
            this.macroRepository = macroRepository;
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
            this.hookService.StartRecording(macro);
            this.OnRecordingStarted();
        }

        /// <summary>
        /// Stopes recording the supplied Macro and saves it into the repository. 
        /// Fires the RecordingStopped event.
        /// </summary>
        public void StopRecording(Macro macro)
        {
            this.hookService.StopRecording();
            this.macroRepository.Add(macro);
            this.OnRecordingStopped();
        }

        /// <summary>
        /// Replays the supplied macro. 
        /// </summary>
        public void ReplayMacro(Macro macro)
        {
            this.hookService.ReplayMacro(macro);
        }

        /// <summary>
        /// Returns all the macros from the Macro repository.
        /// </summary>
        public IEnumerable<Macro> GetAllMacros()
        {
            return this.macroRepository.Read();
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
