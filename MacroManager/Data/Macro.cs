using MacroManager.Data.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MacroManager.Data
{
    /// <summary>
    /// Represents a sequence of user actions.
    /// </summary>
    public class Macro
    {
        #region Fields

        /// <summary>
        /// All the user actios this macro consists of.
        /// </summary>
        private IList<UserAction> userActions;

        #endregion

        #region Properties

        /// <summary>
        /// The Macro identifier.
        /// </summary>
        public Guid MacroId { get; private set; }
        /// <summary>
        /// The name of this Macro, does not have to be unique.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the Macro, is not required.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor that initializes all the fields and properties with the supplied values.
        /// </summary>
        public Macro(IList<UserAction> userActions, Guid macroId, string name, string description)
        {
            this.userActions = userActions;
            this.MacroId = macroId;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Constructor that initializes a new Macro with a name, a description and a list of user actions.
        /// Creates a new Guid.
        /// </summary>
        public Macro(IList<UserAction> userActions, string name, string description) : this (userActions, Guid.NewGuid(), name, description)
        {
            this.userActions = userActions;
        }

        /// <summary>
        /// Constructor that initializes a new Macro with a name and a description. 
        /// Creates a new Guid and empty list of UserActions.
        /// </summary>
        public Macro(string name, string description) : this(new List<UserAction>(), Guid.NewGuid(), name, description) {}

        /// <summary>
        /// Default constructor that initializes a macro with the namem Untitled and no description.
        /// </summary>
        public Macro() : this("Untitled", String.Empty) {}

        #endregion

        #region Methods

        /// <summary>
        /// Adds a UserAction to the Macro.
        /// </summary>
        public void AddUserAction(UserAction userAction)
        {
            if (userAction is ClickAction)
            {
                var x = userAction as ClickAction;
                Debug.WriteLine(String.Format("x: {0} y: {1}", x.X, x.Y));
            } 
            this.userActions.Add(userAction);
        }

        /// <summary>
        /// Returns all the UserAction of this Macro.
        /// </summary>
        public IEnumerable<UserAction> GetUserActions() {
            return this.userActions;
        }

        #endregion
    }
}
