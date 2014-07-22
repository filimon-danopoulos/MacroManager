using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core
{
    public interface IMacroService
    {
        event EventHandler RecordingStarted;
        event EventHandler RecordingStopped;

        Macro CreateMacro(string name, string description = "");
        void StartRecording(Macro macro);
        void StopRecording(Macro macro);
        void ReplayMacro(Macro macro);
        IEnumerable<Macro> GetAllMacros();
    }
}
