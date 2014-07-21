using MacroManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager
{
    public class MacroService : IMacroService
    {
        private IMacroRepository macroRepository;
        private IHookService hookService;

        public MacroService(IMacroRepository macroRepository, IHookService hookService)
        {
            this.macroRepository = macroRepository;
            this.hookService = hookService;
        }

        public Macro CreateNewMacro()
        {
            return new Macro();
        }

        public void StartRecording(Macro macro)
        {
            this.hookService.StartRecording(macro);
        }

        public void StopRecording(Macro macro)
        {
            this.hookService.StopRecording();
            this.macroRepository.Add(macro);
        }

        public void ReplayMacro(Macro macro)
        {
            this.hookService.ReplayMacro(macro);
        }

        public IEnumerable<Macro> GetAllMacros()
        {
            return this.macroRepository.Read();
        }

    }
}
