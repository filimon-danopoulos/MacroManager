using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Core.Action
{
    public class RightClickAction : ClickAction
    {
        public RightClickAction(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
