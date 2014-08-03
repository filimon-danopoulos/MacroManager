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

namespace MacroManager.Hooks
{
    /// <summary>
    /// A service that allows you to record and emulate user actions via low level hooks.
    /// </summary>
    public class HookService : IHookService
    {
        #region Fields

        private readonly VirtualMouse virtualMouse;
        private readonly VirtualKeyboard virtualKeyboard;

        /// <summary>
        /// Keeps track of all the recorded actions.
        /// </summary>
        private readonly IList<UserAction> actions;

        /// <summary>
        /// The macro that is used as a container for all user actions
        /// </summary>
        private Macro macro;

        /// <summary>
        /// Keeps track of when the last user action was recorded. Used to add a WaitingAction before each action.
        /// </summary>
        private DateTime previousAction;

        #endregion

        #region Constructors

        public HookService()
        {
            this.actions = new List<UserAction>();
            this.previousAction = DateTime.MinValue;

            this.virtualMouse = new VirtualMouse();
            this.virtualMouse.MouseClicked += (sender, args) => this.AddActionToMacro(args.Action);

            this.virtualKeyboard = new VirtualKeyboard();
            this.virtualKeyboard.KeyPressed += (sender, args) => this.AddActionToMacro(args.Action);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the recording of a macro, will add all actions to the macro supplied.
        /// </summary>
        /// <param name="inputMacro">The macro that all actions should be writen to.</param>
        public void StartRecording(Macro inputMacro)
        {
            if (this.macro != null)
            {
                throw new Exception("Previous macro is not null. Can only record a single macro at a time!");
            }
            this.macro = inputMacro;
            this.previousAction = DateTime.MinValue;
            this.virtualMouse.StartRecording();
            this.virtualKeyboard.StartRecording();
        }

        /// <summary>
        /// Stops recording the current macro (if any)
        /// </summary>
        public void StopRecording()
        {
            if (this.macro != null)
            {
                this.virtualMouse.StopRecording();
                this.virtualKeyboard.StopRecording();
                foreach (var action in actions.Take(actions.Count - 4))
                {
                    this.macro.AddUserAction(action);
                }
                actions.Clear();
                this.macro = null;
            }
        }

        /// <summary>
        /// Replays a macro asynchronously
        /// </summary>
        public async Task ReplayMacroAsync(Macro macro)
        {
            foreach (var action in macro.GetUserActions())
            {
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
                else if (action is KeyPressAction)
                {
                    this.virtualKeyboard.KeyPress(action as KeyPressAction);
                }
                else if (action is WaitAction)
                {
                    await Task.Delay((action as WaitAction).Duration);
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add an action to the macro, also adds a WaitAction before the supplied action. 
        /// The WaitAction is added so that the macro replays in the same time the user entered it.
        /// </summary>
        private void AddActionToMacro(UserAction action)
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

        #endregion
    }
}
