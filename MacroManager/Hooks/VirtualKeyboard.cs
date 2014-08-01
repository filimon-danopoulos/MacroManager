using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Hooks
{
    public class VirtualKeyboard
    {
        #region Fields

        private IntPtr keyboardHookId = IntPtr.Zero;

        #endregion

        #region Public Methods

        public void KeyPress(KeyPressAction keyPressAction)
        {
            var key = (byte)keyPressAction.VirtualKey;
            keybd_event(key, 0, (int)Events.KEYBOARDEVENTF_KEYDOWN, 0);
            keybd_event(key, 0, (int)Events.KEYBOARDEVENTF_KEYUP, 0);
        }

        public void StartRecording()
        {
            this.SetKeyboardHook(this.HandleKeyboardHook);
        }

        public void StopRecording()
        {
            HookHelper.UnhookWindowsHookEx(this.keyboardHookId);
        }
        #endregion

        #region Private Methods

        private IntPtr HandleKeyboardHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (VirtualKeyboard.Messages)wParam;
            if (nCode >= 0 && message == VirtualKeyboard.Messages.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var args = new KeyboardEventArgs(new KeyPressAction(vkCode));
                this.OnKeyPressed(args);
            }
            return HookHelper.CallNextHookEx(this.keyboardHookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// Sets a low level keyboard hook
        /// </summary>
        private IntPtr SetKeyboardHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(VirtualKeyboard.WH_KEYBOARD_LL, proc, HookHelper.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
        #endregion

        #region Events

        #region Nested class KeyBoardEventArgs

        public class KeyboardEventArgs : EventArgs
        {
            public KeyboardEventArgs(KeyPressAction action)
            {
                this.Action = action;
            }
            public KeyPressAction Action
            {
                get;
                private set;
            }
        }

        #endregion

        public event EventHandler<KeyboardEventArgs> KeyPressed;
        private void OnKeyPressed(KeyboardEventArgs args)
        {
            var handler = this.KeyPressed;
            if (handler != null)
            {
                this.KeyPressed(this, args);
            }
        }

        #endregion

        #region Hook related code

        /// <summary>
        /// Low level keyboard hook identifier
        /// </summary>
        private const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Enumeration of keyboard messages
        /// </summary>
        private enum Messages
        {
            WM_KEYDOWN = 0x0100
        }

        /// <summary>
        /// Enumeration of keyboard events, used when emulating events.
        /// </summary>
        private enum Events
        {
            KEYBOARDEVENTF_KEYDOWN = 0x00,
            KEYBOARDEVENTF_KEYUP = 0x7F
        }

        /// <summary>
        /// Defines the signature for the low level keyboard callback
        /// </summary>
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        #region Unmanaged code imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        #endregion


        #endregion
    }
}
