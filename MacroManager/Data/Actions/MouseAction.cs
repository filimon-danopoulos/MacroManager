using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Data.Actions
{
    public abstract class MouseAction : UserAction
    {
        /// <summary>
        /// Shows wether or not this action was a left of right button.
        /// </summary>
        public MouseButton PressedButton
        {
            get;
            set;
        }

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
