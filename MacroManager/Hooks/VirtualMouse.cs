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
        /// <summary>
        /// Low level mouse hook identifier
        /// </summary>
        public const int WH_MOUSE_LL = 14;

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
        public enum Events
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

    }
}
