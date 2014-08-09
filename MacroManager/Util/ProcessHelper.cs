using MacroManager.Recording;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Util
{
    internal static class ProcessHelper
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr WindowFromPoint(Point pnt);
    }
}
