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
        /// Creates a click action at the provided point. 
        /// Contains metadata about the window that was clicked.
        /// </summary>
        public ClickAction(int x, int y, MouseButton button, string process)
        {
            this.X = x;
            this.Y = y;
            this.PressedButton = button;
            this.Process = process;
        }

        /// <summary>
        /// Creates a click action at the provided point.
        /// </summary>
        public ClickAction(int x, int y, MouseButton button) : this(x, y, button, "")
        {
        }

        /// <summary>
        /// The Y coordinate of the click.
        /// </summary>
        public int Y
        {
            get;
            protected set;
        }
        /// <summary>
        /// The X coordinate of the click.
        /// </summary>
        public int X
        {
            get;
            protected set;
        }

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
