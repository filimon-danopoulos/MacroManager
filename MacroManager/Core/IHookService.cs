using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Core
{
    public interface IHookService
    {
        void StartRecording(Macro macro);
        void StopRecording();
        void ReplayMacro(Macro macro);
    }
}
