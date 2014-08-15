using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback
{
    public class VirtualMouse
    {
        public const int DRAG_INTERVAL = 16;

        /// <summary>
        /// Moves the mouse cursor to the specified coordinates.
        /// </summary>
        public void MoveCursor(int x, int y)
        {
            SetCursorPos(x, y);
        }

        /// <summary>
        /// Executes a mouse action at the specified coordinates.
        /// </summary>
        public void ExecuteMouseEvent(Event mouseEvent, int x, int y)
        {
            mouse_event((uint)mouseEvent, (uint)x, (uint)y, 0, 0);
        }

        /// <summary>
        /// Enumeration of mouse events.
        /// </summary>
        public enum Event
        {
            LeftDown = 0x02,
            LeftUp = 0x04,
            LeftClick = Event.LeftDown | Event.LeftUp,
            RightDown = 0x08,
            RightUp = 0x10,
            RightClick = Event.RightDown | Event.RightUp,
            
        }

        #region Unmanaged imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        #endregion

    }
}
