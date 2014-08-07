using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroManager.Data.Actions
{
    /// <summary>
    /// Base class for all user actions.
    /// </summary>
    public abstract class UserAction
    {
        public string Aplication
        {
            get;
            protected set;
        }
    }
}
