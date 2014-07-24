using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Data.Actions
{
    /// <summary>
    /// A concrete click action, represents the left clik.
    /// </summary>
    public class LeftClickAction : ClickAction
    {
        /// <summary>
        /// Constructor that initializes the X and Y coordinates.
        /// </summary>
        public LeftClickAction(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
