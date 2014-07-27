using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Data.Actions
{
    /// <summary>
    /// Abstract class that represent a Click action
    /// </summary>
    public class ClickAction : UserAction
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

        /// <summary>
        /// Shows wether or not this action was a left of right button.
        /// </summary>
        public MouseButton PressedButton { get; set; }

        /// <summary>
        /// Enumeraion of all supported mouse buttons.
        /// </summary>
        public enum MouseButton
        {
            Left,
            Right
        }
    }
}
