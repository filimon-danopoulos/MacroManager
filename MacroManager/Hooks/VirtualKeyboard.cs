using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Hooks
{
    public class VirtualKeyboard
    {
        /// <summary>
        /// Low level keyboard hook identifier
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Enumeration of keyboard messages
        /// </summary>
        public enum Messages
        {
            WM_KEYDOWN = 0x0100
        }

        /// <summary>
        /// Enumeration of keyboard events, used when emulating events.
        /// </summary>
        public enum Events
        {
            KEYBOARDEVENTF_KEYDOWN = 0x00,
            KEYBOARDEVENTF_KEYUP = 0x7F
        }

    }
}
