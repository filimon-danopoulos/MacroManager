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

        private IMacroRepository macroRepository;
        private IHookService hookService;

        #endregion

        #region Constructors

        public MacroService(IMacroRepository macroRepository, IHookService hookService)
        {
            this.macroRepository = macroRepository;
            this.hookService = hookService;
        }

        #endregion

        #region Methods

        public Macro CreateMacro(string name, string description = "")
        {
            return new Macro();
        }

        public void StartRecording(Macro macro)
        {
            this.hookService.StartRecording(macro);
            this.OnRecordingStarted();
        }

        public void StopRecording(Macro macro)
        {
            this.hookService.StopRecording();
            this.macroRepository.Add(macro);
            this.OnRecordingStoped();
        }

        public void ReplayMacro(Macro macro)
        {
            this.hookService.ReplayMacro(macro);
        }

        public IEnumerable<Macro> GetAllMacros()
        {
            return this.macroRepository.Read();
        }

        #endregion

        #region Events

        public event EventHandler RecordingStarted;
        private void OnRecordingStarted()
        {
            var handler = RecordingStarted;
            if (handler != null)
            {
                this.RecordingStarted(this, null);
            }
        }

        public event EventHandler RecordingStoped;
        private void OnRecordingStoped()
        {
            var handler = RecordingStoped;
            if (handler != null)
            {
                this.RecordingStoped(this, null);
            }
        }

        #endregion

    }
}
