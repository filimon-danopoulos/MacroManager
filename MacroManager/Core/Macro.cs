using MacroManager.Core.Action;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MacroManager.Core
{
    public class Macro
    {
        public Macro(IList<UserAction> userActions, Guid macroId)
        {
            this.UserActions = userActions;
            this.macroId = macroId;
        }
        public Macro() : this(new List<UserAction>(), Guid.NewGuid()) {}
        
        private IList<UserAction> UserActions { get; set; }

        public readonly Guid macroId;

        public void AddUserAction(UserAction userAction)
        {
            if (userAction is ClickAction)
            {
                var x = userAction as ClickAction;
                Debug.WriteLine(String.Format("x: {0} y: {1}", x.X, x.Y));
            } 
            this.UserActions.Add(userAction);
        }

        public IEnumerable<UserAction> GetUserActions() {
            return this.UserActions.ToArray();
        }

        
    }
}
