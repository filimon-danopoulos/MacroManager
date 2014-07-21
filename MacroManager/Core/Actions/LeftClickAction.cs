using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Core.Action
{
    public class LeftClickAction : ClickAction
    {
        public LeftClickAction(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
