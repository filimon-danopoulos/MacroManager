using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Recording
{
    public class KeyboardRecorder : IRecorder
    {
        #region Fields

        private IntPtr keyboardHookId = IntPtr.Zero;
        private LowLevelKeyboardProc hookCallback;

        #endregion

        #region Constructors

        public KeyboardRecorder()
        {
            // We have to save the callback as a field other wise it is GC:d
            this.hookCallback = this.HandleKeyboardHook;
        }

        #endregion

        #region Public Methods

        public void StartRecording()
        {
            this.keyboardHookId = this.SetKeyboardHook();
        }

        public void StopRecording()
        {
            HookHelper.UnhookWindowsHookEx(this.keyboardHookId);
        }
        #endregion

        #region Private Methods

        private IntPtr HandleKeyboardHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (Messages)wParam;
            if (nCode >= 0 && message == Messages.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var args = new KeyboardEventArgs(new KeyboardAction(vkCode));
                this.OnKeyPressed(args);
            }
            return HookHelper.CallNextHookEx(this.keyboardHookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// Sets a low level keyboard hook
        /// </summary>
        private IntPtr SetKeyboardHook()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, this.hookCallback, HookHelper.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }
        #endregion

        #region Events

        #region Nested class KeyBoardEventArgs

        public class KeyboardEventArgs : EventArgs
        {
            public KeyboardEventArgs(KeyboardAction action)
            {
                this.Action = action;
            }
            public KeyboardAction Action
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
        /// Defines the signature for the low level keyboard callback
        /// </summary>
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        #region Unmanaged code imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        #endregion


        #endregion
    }
}
