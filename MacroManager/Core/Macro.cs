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
        #region Fields

        private IList<UserAction> userActions;

        #endregion

        #region Properties

        public Guid MacroId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #endregion

        #region Constructors

        public Macro(IList<UserAction> userActions, Guid macroId, string name, string description)
        {
            this.userActions = userActions;
            this.MacroId = macroId;
            this.Name = name;
            this.Description = description;
        }
        public Macro(string name, string description) : this(new List<UserAction>(), Guid.NewGuid(), name, description) {}
        public Macro() : this("Untitled", String.Empty) {}

        #endregion

        #region Methods

        public void AddUserAction(UserAction userAction)
        {
            if (userAction is ClickAction)
            {
                var x = userAction as ClickAction;
                Debug.WriteLine(String.Format("x: {0} y: {1}", x.X, x.Y));
            } 
            this.userActions.Add(userAction);
        }

        public IEnumerable<UserAction> GetUserActions() {
            return this.userActions.ToArray();
        }

        #endregion
    }
}
