using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Playback
{
    public class VirtualMouse
    {
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

        public async Task DragAsync(DragAction action)
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
            var firstPoint = action.Path.First();
            SetCursorPos(firstPoint.X, firstPoint.Y);
            mouse_event(mouseDownEvent, (uint)firstPoint.X, (uint)firstPoint.Y, 0, 0);
            foreach (var point in action.Path.Skip(1))
            {
                await Task.Delay(16).ConfigureAwait(false);
                SetCursorPos(point.X, point.Y);
            }
            var lastPoint = action.Path.Last();
            mouse_event(mouseUpEvent, (uint)lastPoint.X, (uint)lastPoint.Y, 0, 0);
        }

        #endregion

        #region Hook related code

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

        #region Unmanaged imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        #endregion

        #endregion
    }
}
