using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Recording
{
    public interface IRecorder
    {
        void StartRecording();
        void StopRecording();
    }
}
