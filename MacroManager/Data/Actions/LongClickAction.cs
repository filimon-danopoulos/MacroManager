using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Data.Actions
{
    public class LongClickAction : UserAction
    {
        public LongClickAction(int duration)
        {
            this.Duration = duration;
        }
        public int Duration { get; private set; }
    }
}
