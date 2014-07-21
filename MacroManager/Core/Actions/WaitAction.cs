using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Core.Action
{
    public class WaitAction : UserAction
    {
        public WaitAction(int duration)
        {
            this.Duration = duration;
        }
        public int Duration { get; private set; }
    }
}
