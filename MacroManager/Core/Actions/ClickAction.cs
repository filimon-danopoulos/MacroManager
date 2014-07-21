using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Core.Action
{
    public abstract class ClickAction : UserAction
    {
        public int Y { get; protected set; }
        public int X { get; protected set; }
    }
}
