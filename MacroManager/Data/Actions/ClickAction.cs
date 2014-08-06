using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Data.Actions
{
    /// <summary>
    /// Abstract class that represent a Click action
    /// </summary>
    public class ClickAction : MouseAction
    {
        /// <summary>
        /// What type of comment should I write on a constructor?
        /// </summary>
        public ClickAction(int x, int y, MouseButton button)
        {
            this.X = x;
            this.Y = y;
            this.PressedButton = button;
        }

        /// <summary>
        /// The Y coordinate of the click.
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// The X coordinate of the click.
        /// </summary>
        public int X { get; private set; }

        public override string ToString()
        {
            return String.Format("{0} click at ({1}, {2})", 
                this.PressedButton == MouseButton.Left ? "Left" : "Right",
                this.X, 
                this.Y
            );
        }
    }
}
