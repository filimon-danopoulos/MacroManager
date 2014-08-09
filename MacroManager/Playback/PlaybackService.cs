
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
using MacroManager.Util;
using System.Drawing;

namespace MacroManager.Playback
{
    public class PlaybackService
    {
        #region Fields

        private readonly VirtualMouse virtualMouse;
        private readonly VirtualKeyboard virtualKeyboard;
        private bool stopPlayback;

        #endregion

        #region Constructors

        public PlaybackService()
        {
            this.stopPlayback = false;

            this.virtualMouse = new VirtualMouse();
            this.virtualKeyboard = new VirtualKeyboard();
         }

        #endregion

        #region Public Methods

        /// <summary>
        /// Replays a macro asynchronously
        /// </summary>
        public async Task StartMacroPlaybackAsync(Macro macro)
        {
            this.stopPlayback = false;
            foreach (var action in macro.GetUserActions())
            {
                if (this.stopPlayback)
                {
                    this.OnMacroCanceled();
                    return;
                }

                if (action is MouseAction && action.Process != "" && !this.IsSameProcess((ClickAction)action))
                {
                    this.OnActionError();
                    return;
                }

                this.OnActionStarted();
                if (action is MacroManager.Data.Actions.DragAction)
                {
                    await this.virtualMouse.DragAsync(action as MacroManager.Data.Actions.DragAction);
                }
                else if (action is LongClickAction)
                {
                    await this.virtualMouse.LongClickAsync(action as LongClickAction);
                }
                else if (action is ClickAction)
                {
                    this.virtualMouse.Click(action as ClickAction);
                }
                else if (action is KeyboardAction)
                {
                    this.virtualKeyboard.KeyPress(action as KeyboardAction);
                }
                else if (action is WaitAction)
                {
                    await Task.Delay((action as WaitAction).Duration);
                }
                else
                {
                    throw new NotImplementedException(String.Format("Specified action {0} is not implemented.", action.GetType().Name));
                }

                this.OnActionCompleted();
            }
            this.OnMacroCompleted();
        }


        public void StopMacroPlayback()
        {
            this.stopPlayback = true;
        }

        #endregion

        #region Private methods

        private bool IsSameProcess(ClickAction action)
        {
            var windowHandle = Util.ProcessHelper.WindowFromPoint(new Point(action.X, action.Y));
            int processId;
            Util.ProcessHelper.GetWindowThreadProcessId(windowHandle, out processId);
            return Process.GetProcesses().Any(x => x.Id == processId && x.ProcessName == action.Process);
        }

        #endregion

        #region Events

        public event EventHandler MacroCanceled;
        private void OnMacroCanceled()
        {
            var handler = this.MacroCanceled;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler MacroCompleted;
        private void OnMacroCompleted()
        {
            var handler = this.MacroCompleted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler ActionStarted;
        private void OnActionStarted()
        {
            var handler = this.ActionStarted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    

        public event EventHandler ActionCompleted;
        private void OnActionCompleted()
        {
            var handler = this.ActionCompleted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler ActionError;
        private void OnActionError()
        {
            var handler = this.ActionError;
            if (handler != null) {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
