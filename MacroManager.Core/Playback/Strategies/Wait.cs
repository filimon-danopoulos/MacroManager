using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback.Strategies
{
    [Executes(typeof(WaitAction))]
    public class Wait : PlaybackStrategy
    {
        public override async Task ExecuteAsync(UserAction action)
        {
            await Task.Delay((action as WaitAction).Duration);
        }
    }
}
