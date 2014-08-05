using MacroManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Hooks
{
    public interface IHookService
    {
        void StartRecording();
        Macro StopRecording();
        Task PlaybackMacroAsync(Macro macro);
    }
}
