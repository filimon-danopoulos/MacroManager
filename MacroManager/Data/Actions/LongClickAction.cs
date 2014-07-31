using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Data.Actions
{
    public class LongClickAction : ClickAction
    {
        public LongClickAction(int x, int y, ClickAction.MouseButton button, int duration) : base(x, y, button)
        {
            this.Duration = duration;
        }
        public int Duration { get; private set; }
    }
}
