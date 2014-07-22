using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Core.Action
{
    /// <summary>
    /// A concrete click action, represents the right clik.
    /// </summary>
    public class RightClickAction : ClickAction
    {
        /// <summary>
        /// Constructor that initializes the X and Y coordinates.
        /// </summary>
        public RightClickAction(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
