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
    public class VirtualMouse
    {
        #region Fields

        private IntPtr mouseHookId;
        private DateTime clickDown;
        private POINT clickDownPoint;

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes a single click
        /// </summary>
        public void Click(ClickAction action)
        {
            uint mouseEvent;
            switch (action.PressedButton)
            {
                case ClickAction.MouseButton.Right:
                    mouseEvent = (uint)Event.MOUSEEVENTF_RIGHTDOWN | (uint)Event.MOUSEEVENTF_RIGHTUP;
                    break;
                case ClickAction.MouseButton.Left:
                    mouseEvent = (uint)Event.MOUSEEVENTF_LEFTDOWN | (uint)Event.MOUSEEVENTF_LEFTUP;
                    break;
                default:
                    throw new NotImplementedException();
            }
            SetCursorPos(action.X, action.Y);
            mouse_event(mouseEvent, (uint)action.X, (uint)action.Y, 0, 0);
        }

        /// <summary>
        /// Executes a long click
        /// </summary>
        public async Task LongClickAsync(LongClickAction action)
        {
            uint mouseDownEvent;
            uint mouseUpEvent;
            switch (action.PressedButton)
            {
                case ClickAction.MouseButton.Right:
                    mouseDownEvent = (uint)Event.MOUSEEVENTF_RIGHTDOWN;
                    mouseUpEvent = (uint)Event.MOUSEEVENTF_RIGHTUP;
                    break;
                case ClickAction.MouseButton.Left:
                    mouseDownEvent = (uint)Event.MOUSEEVENTF_LEFTDOWN;
                    mouseUpEvent = (uint)Event.MOUSEEVENTF_LEFTUP;
                    break;
                default:
                    throw new NotImplementedException();
            }
            SetCursorPos(action.X, action.Y);
            mouse_event(mouseDownEvent, (uint)action.X, (uint)action.Y, 0, 0);
            await Task.Delay(action.Duration).ConfigureAwait(false);
            mouse_event(mouseUpEvent, (uint)action.X, (uint)action.Y, 0, 0);
        }

        /// <summary>
        /// Starts recording all mouse activity the user makes
        /// </summary>
        public void StartRecording()
        {
            this.mouseHookId = this.SetMouseHook(this.HandleMouseHook);
        }

        /// <summary>
        /// Stops recording all the mouse activity the user makes.
        /// </summary>
        public void StopRecording()
        {
            HookHelper.UnhookWindowsHookEx(this.mouseHookId);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the low level mouse hook and fires an event on each click action.
        /// </summary>
        private IntPtr HandleMouseHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (Message)wParam;
            if (nCode >= 0 && message != Message.WM_MOUSEMOVE)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                if (message == Message.WM_LBUTTONDOWN || message == Message.WM_RBUTTONDOWN)
                {
                    this.clickDown = DateTime.Now;
                    this.clickDownPoint = hookStruct.pt;
                }
                else if (message == Message.WM_LBUTTONUP || message == Message.WM_RBUTTONUP)
                {
                    var ellapsedTime = (int)Math.Floor((DateTime.Now - this.clickDown).TotalMilliseconds);
                    var pressedButton = message == Message.WM_LBUTTONUP ? ClickAction.MouseButton.Left : ClickAction.MouseButton.Right;
                    var mouseMoved = this.clickDownPoint.x != hookStruct.pt.x || this.clickDownPoint.y != hookStruct.pt.y;
                    if (ellapsedTime > 200 && !mouseMoved)
                    {
                        var args = new MouseEventArgs(new LongClickAction(hookStruct.pt.x, hookStruct.pt.y, pressedButton, ellapsedTime));
                        this.OnMouseClicked(args);
                    }
                    else
                    {
                        var args = new MouseEventArgs(new ClickAction(hookStruct.pt.x, hookStruct.pt.y, pressedButton));
                        this.OnMouseClicked(args);
                    }
                }
            }
            return HookHelper.CallNextHookEx(mouseHookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// Sets a low level mouse hook
        /// </summary>
        private IntPtr SetMouseHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_MOUSE_LL, proc, HookHelper.GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        #endregion

        #region Events

        #region Nested class MouseEventArgs
        public class MouseEventArgs : EventArgs
        {
            public MouseEventArgs(ClickAction action)
            {
                this.Action = action;
            }
            public ClickAction Action
            {
                get;
                private set;
            }
        }
        #endregion

        public event EventHandler<MouseEventArgs> MouseClicked;
        private void OnMouseClicked(MouseEventArgs args)
        {
            var handler = this.MouseClicked;
            if (handler != null)
            {
                this.MouseClicked(this, args);
            }
        }

        #endregion

        #region Hook related code

        /// <summary>
        /// Low level mouse hook identifier
        /// </summary>
        private const int WH_MOUSE_LL = 14;

        /// <summary>
        /// Defines the signature for the low level hook mouse callback
        /// </summary>
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Enumeration of mouse messages, used when intercepting messages.
        /// </summary>
        private enum Message
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        /// <summary>
        /// Enumeration of mouse events, used when emulating events.
        /// </summary>
        private enum Event
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10
        }

        /// <summary>
        /// Managed wrapper for the point struct used in the un-managed code
        /// </summary>
        [StructLayout(LayoutKind.Sequential)] // StructLayout is specified as sequential so that it behaves as C/C++ would
        private struct POINT
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// Managed wrapper for the hook struct used in the un-managed code
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }


        #region Unmanaged imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        #endregion

        #endregion
    }
}
