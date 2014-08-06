using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Recording
{
    public class MouseRecorder : IRecorder
    {
        #region Fields

        private IntPtr mouseHookId;
        private DateTime clickDownTime;
        private DateTime previousPathReadTime;
        private POINT clickDownPoint;
        private List<POINT> path;
        private bool mouseDown;

        #endregion

        #region Constructors

        public MouseRecorder()
        {
            this.path = new List<POINT>();
            this.previousPathReadTime = DateTime.Now;
        }

        #endregion

        #region Public Methods

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
            MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            if (nCode >= 0 && message != Message.WM_MOUSEMOVE)
            {
                if (message == Message.WM_LBUTTONDOWN || message == Message.WM_RBUTTONDOWN)
                {
                    this.clickDownTime = DateTime.Now;
                    this.clickDownPoint = hookStruct.pt;
                    this.mouseDown = true;
                }
                else if (message == Message.WM_LBUTTONUP || message == Message.WM_RBUTTONUP)
                {
                    this.mouseDown = false;
                    var ellapsedTime = (int)Math.Floor((DateTime.Now - this.clickDownTime).TotalMilliseconds);
                    var pressedButton = message == Message.WM_LBUTTONUP ? ClickAction.MouseButton.Left : ClickAction.MouseButton.Right;
                    var mouseMoved = this.clickDownPoint.x != hookStruct.pt.x || this.clickDownPoint.y != hookStruct.pt.y;
                    if (ellapsedTime > 200 && !mouseMoved)
                    {
                        var args = new MouseEventArgs(new LongClickAction(hookStruct.pt.x, hookStruct.pt.y, pressedButton, ellapsedTime));
                        this.OnMouseClicked(args);
                    }
                    else if (ellapsedTime > 200 && mouseMoved)
                    {
                        var args = new MouseEventArgs(new DragAction(pressedButton, this.path.Select(x => new Point(x.x, x.y)).ToList()));
                        this.OnMouseClicked(args);
                    }
                    else
                    {
                        var args = new MouseEventArgs(new ClickAction(hookStruct.pt.x, hookStruct.pt.y, pressedButton));
                        this.OnMouseClicked(args);
                    }
                    this.path.Clear();
                }
            }
            else if (nCode >= 0 && message == Message.WM_MOUSEMOVE && this.mouseDown)
            {
                var now = DateTime.Now;
                var ellapsedTime = (int)Math.Floor((now - this.previousPathReadTime).TotalMilliseconds);
                if (ellapsedTime >= 16)
                {
                    this.previousPathReadTime = now;
                    this.path.Add(hookStruct.pt);
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
            public MouseEventArgs(UserAction action)
            {
                this.Action = action;
            }
            public UserAction Action
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
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        #endregion

        #endregion
    }
}
