using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback.Strategies
{
    public class UnkownStrategy : PlaybackStrategy
    {
        public override Task ExecuteAsync(UserAction action)
        {
            var message = String.Format(
                "Action of type {0} does not have a known strategy. Someone messed up!",
                action.GetType().Name
            );
            throw new NotImplementedException(message);
        }
    }
}
