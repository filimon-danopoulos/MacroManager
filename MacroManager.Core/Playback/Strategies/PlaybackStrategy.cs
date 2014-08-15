using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback.Strategies
{
    public abstract class PlaybackStrategy
    {
        #region Fields
        
        protected readonly VirtualMouse virtualMouse;
        protected readonly VirtualKeyboard virtualKeyboard;

        #endregion

        #region Constructors

        public PlaybackStrategy()
        {
            this.virtualMouse = new VirtualMouse();
            this.virtualKeyboard = new VirtualKeyboard();
        }

        #endregion

        #region Public Methods

        public abstract Task ExecuteAsync(UserAction action);

        #endregion

        #region Attributes

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public sealed class ExecutesAttribute : Attribute
        {
            public ExecutesAttribute(Type actionType)
            {
                this.ActionType = actionType;
            }

            public Type ActionType
            {
                get;
                private set;
            }
        }

        #endregion

    }
}
