using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Hooks
{
    public class VirtualMouse
    {
        #region Constants

        /// <summary>
        /// Low level mouse hook identifier
        /// </summary>
        public const int WH_MOUSE_LL = 14;

        #endregion

        #region Enums

        /// <summary>
        /// Enumeration of mouse messages, used when intercepting messages.
        /// </summary>
        public enum Messages
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

        #endregion

        #region Structs

        /// <summary>
        /// Managed wrapper for the point struct used in the un-managed code
        /// </summary>
        [StructLayout(LayoutKind.Sequential)] // StructLayout is specified as sequential so that it behaves as C/C++ would
        public struct POINT
        {
            public int x;
            public int y;
        }
        /// <summary>
        /// Managed wrapper for the hook struct used in the un-managed code
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        #endregion

        #region Methods

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

        #endregion

        #region Unmanaged imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        #endregion
    }
}
