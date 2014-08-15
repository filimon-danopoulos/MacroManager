using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback.Strategies
{
    [Executes(typeof(KeyboardAction))]
    public class KeyPress : PlaybackStrategy
    {
        public override async Task ExecuteAsync(UserAction action)
        {
            await Task.Run(() =>
            {
                this.virtualKeyboard.KeyPress((action as KeyboardAction));
            });

        }
    }
}
