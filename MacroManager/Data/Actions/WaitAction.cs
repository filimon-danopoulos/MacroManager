using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Data.Actions
{
    /// <summary>
    /// An action that represents wainting. The user usualy moves the mouse or something similar during this type of action.
    /// </summary>
    public class WaitAction : UserAction
    {   
        /// <summary>
        /// The duration of the wait.
        /// </summary>
        public int Duration { get; private set; }
        
        /// <summary>
        /// Constructor that initializes the WaitAction with a duration.
        /// </summary>
        public WaitAction(int duration)
        {
            this.Duration = duration;
        }
    }
}
