using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Playback
{
    public class VirtualKeyboard
    {
        #region Public Methods

        public void KeyPress(KeyboardAction keyPressAction)
        {
            var key = (byte)keyPressAction.VirtualKey;
            keybd_event(key, 0, (int)Events.KEYBOARDEVENTF_KEYDOWN, 0);
            keybd_event(key, 0, (int)Events.KEYBOARDEVENTF_KEYUP, 0);
        }

        #endregion

        #region Hook related code

        /// <summary>
        /// Enumeration of keyboard events, used when emulating events.
        /// </summary>
        private enum Events
        {
            KEYBOARDEVENTF_KEYDOWN = 0x00,
            KEYBOARDEVENTF_KEYUP = 0x7F
        }

        #region Unmanaged code imports

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        #endregion

        #endregion
    }
}
