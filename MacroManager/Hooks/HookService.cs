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
using System.Windows.Forms;
using System.Threading.Tasks;

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
            var message = (VirtualMouse.Messages)wParam;
            if (nCode >= 0 && message != VirtualMouse.Messages.WM_MOUSEMOVE)
            {
                VirtualMouse.MSLLHOOKSTRUCT hookStruct = (VirtualMouse.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(VirtualMouse.MSLLHOOKSTRUCT));
                if (message == VirtualMouse.Messages.WM_LBUTTONDOWN || message == VirtualMouse.Messages.WM_RBUTTONDOWN)
                {
                    clikDown = DateTime.Now;
                }
                else if (message == VirtualMouse.Messages.WM_LBUTTONUP || message == VirtualMouse.Messages.WM_RBUTTONUP)
                {
                    var ellapsedTime = (int)Math.Floor((DateTime.Now - clikDown).TotalMilliseconds);
                    var pressedButton = message == VirtualMouse.Messages.WM_LBUTTONUP ? ClickAction.MouseButton.Left : ClickAction.MouseButton.Right;
                    if (ellapsedTime > 200)
                    {
                        AddActionToMacro(new LongClickAction(hookStruct.pt.x, hookStruct.pt.y, pressedButton, ellapsedTime));
                    }
                    else
                    {
                        AddActionToMacro(new ClickAction(hookStruct.pt.x, hookStruct.pt.y, pressedButton));
                    }
                }
            }
            return CallNextHookEx(mouseHookId, nCode, wParam, lParam);
        }


        /// <summary>
        /// The keyboard callback function
        /// </summary>
        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (VirtualKeyboard.Messages)wParam;
            if (nCode >= 0 && message == VirtualKeyboard.Messages.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                AddActionToMacro(new KeyPressAction(vkCode));
            }
            return CallNextHookEx(keyboardHookId, nCode, wParam, lParam);
        }


        /// <summary>
        /// The macro that is used as a container for all user actions
        /// </summary>
        private static Macro macro = null;

        /// <summary>
        /// Keeps track of when the last user action was recorded. Used to add a WaitingAction before each action.
        /// </summary>
        private static DateTime previousAction = DateTime.MinValue;

        private static DateTime clikDown = DateTime.MaxValue;

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
            mouseProc = MouseHookCallback;
            keyboardProc = KeyboardHookCallback;
            mouseHookId = SetMouseHook(mouseProc);
            keyboardHookId = SetKeyboardHook(keyboardProc);
        }

        /// <summary>
        /// Stops recording the current macro (if any)
        /// </summary>
        public void StopRecording()
        {
            if (macro != null)
            {
                UnhookWindowsHookEx(mouseHookId);
                UnhookWindowsHookEx(keyboardHookId);
                foreach (var action in actions.Take(actions.Count - 4))
                {
                    macro.AddUserAction(action);
                }
                actions.Clear();
                macro = null;
            }
        }

        /// <summary>
        /// Replays a macro asynchronously
        /// </summary>
        public async Task ReplayMacroAsync(Macro macro)
        {
            foreach (var action in macro.GetUserActions())
            { 
                if (action is ClickAction)
                {
                    uint mouseDownEvent;
                    uint mouseUpEvent;
                    var tempAction = action as ClickAction;
                    if (tempAction.PressedButton == ClickAction.MouseButton.Right)
                    {
                        mouseDownEvent = (uint)VirtualMouse.Events.MOUSEEVENTF_RIGHTDOWN;
                        mouseUpEvent = (uint)VirtualMouse.Events.MOUSEEVENTF_RIGHTUP;
                    }
                    else if (tempAction.PressedButton == ClickAction.MouseButton.Left)
                    {
                        mouseDownEvent = (uint)VirtualMouse.Events.MOUSEEVENTF_LEFTDOWN;
                        mouseUpEvent = (uint)VirtualMouse.Events.MOUSEEVENTF_LEFTUP;
                    }
                    else
                    {
                        throw new Exception("Unknown mouse action");
                    }
                    SetCursorPos(tempAction.X, tempAction.Y);
                    var longClickAction = (action as LongClickAction);
                    if (longClickAction != null)
                    {
                        mouse_event(mouseDownEvent, (uint)tempAction.X, (uint)tempAction.Y, 0, 0);
                        await Task.Delay(longClickAction.Duration);
                        mouse_event(mouseUpEvent, (uint)tempAction.X, (uint)tempAction.Y, 0, 0);
                    }
                    else
                    {
                        mouse_event(mouseDownEvent|mouseUpEvent, (uint)tempAction.X, (uint)tempAction.Y, 0, 0);
                    }
                }
                else if (action is KeyPressAction)
                {
                    var key = (byte)(action as KeyPressAction).VirtualKey;
                    keybd_event(key, 0, (int)VirtualKeyboard.Events.KEYBOARDEVENTF_KEYDOWN, 0);
                    keybd_event(key, 0, (int)VirtualKeyboard.Events.KEYBOARDEVENTF_KEYUP, 0);
                }
                else if (action is WaitAction)
                {
                    await Task.Delay((action as WaitAction).Duration);
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

        #region Mouse related code

        /// <summary>
        /// Defines the signature for the low level hook mouse callback
        /// </summary>
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The mouse hook callback procedure
        /// </summary>
        private static LowLevelMouseProc mouseProc;

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
                    return SetWindowsHookEx(VirtualMouse.WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        #endregion

        #region Keyboard related code

        /// <summary>
        /// Defines the signature for the low level keyboard callback
        /// </summary>
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The keyboard hook callback procedure.
        /// </summary>
        private static LowLevelKeyboardProc keyboardProc;

        /// <summary>
        /// Keyboard hook id is used to identify the current keyboard hook
        /// </summary>
        private static IntPtr keyboardHookId = IntPtr.Zero;

        /// <summary>
        /// Sets a low level keyboard hook
        /// </summary>
        private static IntPtr SetKeyboardHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(VirtualKeyboard.WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        #endregion

        #region Un-managed code import

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #endregion
    }
}
