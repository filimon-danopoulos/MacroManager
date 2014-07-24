using MacroManager.Hooks;
using MacroManager.Data;
using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MacroManager
{
    /// <summary>
    /// A service that allows you to record and emulate user actions via low level hooks.
    /// </summary>
    public class HookService : IHookService
    {
        /// <summary>
        /// The mouse hook callback function, this is where the money is at! 
        /// </summary>
        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (MouseMessages)wParam;
            if (nCode >= 0 && message != MouseMessages.WM_MOUSEMOVE)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                if (message == MouseMessages.WM_LBUTTONDOWN)
                {
                    AddActionToMacro(new LeftClickAction(hookStruct.pt.x, hookStruct.pt.y));
                }
                else if (message == MouseMessages.WM_RBUTTONDOWN)
                {

                    AddActionToMacro(new RightClickAction(hookStruct.pt.x, hookStruct.pt.y));
                }
            }
            return CallNextHookEx(mouseHookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// The macro that is used as a container for all user actions
        /// </summary>
        private static Macro macro = null;

        /// <summary>
        /// Keeps track of when the last user action was recorded. Used to add a WaitingAction before each action.
        /// </summary>
        private static DateTime previousAction = DateTime.MinValue;

        /// <summary>
        /// Keeps track of all the recorded actions.
        /// </summary>
        private static IList<UserAction> actions = new List<UserAction>();

        /// <summary>
        /// Add an action to the macro, also adds a WaitAction before the supplied action. 
        /// The WaitAction is added so that the macro replays in the same time the user entered it.
        /// </summary>
        private static void AddActionToMacro(UserAction action)
        {
            if (previousAction == DateTime.MinValue)
            {
                previousAction = DateTime.Now;
            }
            else
            {
                var thisActionTime = DateTime.Now;
                var duration = thisActionTime - previousAction;
                actions.Add(new WaitAction((int)duration.TotalMilliseconds));
                previousAction = thisActionTime;
            }
            actions.Add(action);
        }

        /// <summary>
        /// Starts the recording of a macro, will add all actions to the macro supplied.
        /// </summary>
        /// <param name="inputMacro">The macro that all actions should be writen to.</param>
        public void StartRecording(Macro inputMacro)
        {
            if (macro != null)
            {
                throw new Exception("Previous macro is not null. Can only record a single macro at a time!");
            }
            macro = inputMacro;
            proc = MouseHookCallback;
            mouseHookId = SetMouseHook(proc);
        }

        /// <summary>
        /// Stops recording the current macro (if any)
        /// </summary>
        public void StopRecording()
        {
            if (macro != null)
            {
                UnhookWindowsHookEx(mouseHookId);
                foreach (var action in actions.Take(actions.Count - 4))
                {
                    macro.AddUserAction(action);
                }
                actions.Clear();
                macro = null;
            }
        }

        /// <summary>
        /// Replays a macro
        /// </summary>
        public void ReplayMacro(Macro macro)
        {
            foreach (var action in macro.GetUserActions())
            {
                if (action is ClickAction)
                {
                    uint mouseEvent;
                    var tempAction = action as ClickAction;
                    if (action is RightClickAction)
                    {
                        mouseEvent = (uint)MouseEvents.MOUSEEVENTF_RIGHTDOWN | (uint)MouseEvents.MOUSEEVENTF_RIGHTUP;
                    }
                    else if (action is LeftClickAction)
                    {
                        mouseEvent = (uint)MouseEvents.MOUSEEVENTF_LEFTDOWN | (uint)MouseEvents.MOUSEEVENTF_LEFTUP;
                    }
                    else
                    {
                        throw new Exception("Unknown mouse action");
                    }
                    SetCursorPos(tempAction.X, tempAction.Y);
                    mouse_event(mouseEvent, (uint)tempAction.X, (uint)tempAction.Y, 0, 0);
                }
                else if (action is WaitAction)
                {
                    Thread.Sleep((action as WaitAction).Duration);
                } 
            }
        }

        #region Hook-related code
        /* 
        ** Most of this code is taken from these two sources:  
        **      http://blogs.msdn.com/b/toub/archive/2006/05/03/589468.aspx
        **      http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx
        ** I have added comments and tried to make the code a bit more concise
        **/

        #region Constants, structs and enumerations
        /// <summary>
        /// Low level mouse hook identifier
        /// </summary>
        private const int WH_MOUSE_LL = 14;
        /// <summary>
        /// Low level keyboard hook identifier
        /// </summary>
        private const int WH_KEYBOARD_LL = 13;

        /// <summary>
        /// Enumeration of keyboard messages
        /// </summary>
        private enum KeyboardMessages
        {
            WM_KEYDOWN = 0x0100
        }
        /// <summary>
        /// Enumeration of mouse messages, used when intercepting messages.
        /// </summary>
        private enum MouseMessages
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
        private enum MouseEvents
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

        #endregion

        /// <summary>
        /// Defines the signature for the low level hook mouse callback
        /// </summary>
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The mouse hook call back procedure
        /// </summary>
        private static LowLevelMouseProc proc;

        /// <summary>
        /// Mouse hook id is used to identify the current mouse hook
        /// </summary>
        private static IntPtr mouseHookId = IntPtr.Zero;

        /// <summary>
        /// Sets a low level mouse hook
        /// </summary>
        private static IntPtr SetMouseHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        #region Un-managed code import

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);  

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #endregion
    }
}
