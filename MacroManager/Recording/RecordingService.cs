using MacroManager.Data;
using MacroManager.Data.Actions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Recording
{
    public class RecordingService
    {
        #region Fields

        private readonly MouseRecorder mouseRecorder;
        private readonly KeyboardRecorder keyboardRecorder;

        /// <summary>
        /// Keeps track of all the recorded actions.
        /// </summary>
        private readonly IList<UserAction> actions;

        /// <summary>
        /// Keeps track of when the last user action was recorded. Used to add a WaitingAction before each action.
        /// </summary>
        private DateTime previousAction;

        #endregion

        #region Constructors

        public RecordingService()
        {
            this.actions = new List<UserAction>();
            this.previousAction = DateTime.MinValue;

            this.mouseRecorder = new MouseRecorder();
            this.mouseRecorder.MouseClicked += (sender, args) => this.AddActionToMacro(args.Action);

            this.keyboardRecorder = new KeyboardRecorder();
            this.keyboardRecorder.KeyPressed += (sender, args) => this.AddActionToMacro(args.Action);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the recording of a macro, will add all actions to the macro supplied.
        /// </summary>
        /// <param name="inputMacro">The macro that all actions should be writen to.</param>
        public void StartRecording()
        {
            this.actions.Clear();
            this.previousAction = DateTime.MinValue;
            this.mouseRecorder.StartRecording();
            this.keyboardRecorder.StartRecording();
        }

        /// <summary>
        /// Stops recording the current macro (if any)
        /// </summary>
        public void StopRecording()
        {
            this.mouseRecorder.StopRecording();
            this.keyboardRecorder.StopRecording();
        }

        public IEnumerable<UserAction> GetRecordedActions() 
        {
            return actions.Take(actions.Count - 4);
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