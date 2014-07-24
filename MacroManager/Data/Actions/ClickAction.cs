using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Data.Actions
{
    /// <summary>
    /// Abstract class that represent a Click action
    /// </summary>
    public abstract class ClickAction : UserAction
    {
        /// <summary>
        /// The Y coordinate of the click.
        /// </summary>
        public int Y { get; protected set; }
        /// <summary>
        /// The X coordinate of the click.
        /// </summary>
        public int X { get; protected set; }
    }
}
