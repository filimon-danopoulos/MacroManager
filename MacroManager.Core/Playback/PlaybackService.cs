
using MacroManager.Core.Data;
using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using MacroManager.Core.Util;
using System.Drawing;

namespace MacroManager.Core.Playback
{
    public class PlaybackService
    {
        #region Fields

        private PlaybackStrategyFactory strategyFactory;
        private bool stopPlayback;

        #endregion

        #region Constructors

        public PlaybackService()
        {
            this.stopPlayback = false;

            this.strategyFactory = new PlaybackStrategyFactory();
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

                if (action is ClickAction && action.Process != "" && !this.IsSameProcess((ClickAction)action))
                {
                    this.OnActionError(new ErrorEventArgs(action, "The process of the action and the process under the cursor do not match!"));
                    return;
                }

                var actionEventArgs = new ActionEventArgs(action);
                this.OnActionStarted(actionEventArgs);
                var strategy = this.strategyFactory.Create(action);
                try
                {
                    await strategy.ExecuteAsync(action);
                }
                catch (NotImplementedException ex)
                {
                    this.OnActionError(new ErrorEventArgs(action, ex.Message));
                    return;
                }
                this.OnActionCompleted(actionEventArgs);
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

        #region Nested Class ActionEventArgs

        public class ActionEventArgs : EventArgs
        {
            public ActionEventArgs(UserAction action)
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

        public event EventHandler<ActionEventArgs> ActionStarted;
        private void OnActionStarted(ActionEventArgs args)
        {
            var handler = this.ActionStarted;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public event EventHandler<ActionEventArgs> ActionCompleted;
        private void OnActionCompleted(ActionEventArgs args)
        {
            var handler = this.ActionCompleted;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #region Nested Class ErrorEventArgs

        public class ErrorEventArgs : ActionEventArgs
        {
            public ErrorEventArgs(UserAction action, string errorMessage)
                : base(action)
            {
                this.ErrorMessage = errorMessage;
            }
            public string ErrorMessage
            {
                get;
                private set;
            }
        }

        #endregion

        public event EventHandler<ErrorEventArgs> ActionError;
        private void OnActionError(ErrorEventArgs args)
        {
            var handler = this.ActionError;
            if (handler != null) {
                handler(this, args);
            }
        }

        #endregion
    }
}
