using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback.Strategies
{
    [Executes(typeof(LongClickAction))]
    public class LongClick : PlaybackStrategy
    {
        public override async Task ExecuteAsync(UserAction userAction)
        {
            var action = userAction as LongClickAction;
            VirtualMouse.Event mouseDownEvent;
            VirtualMouse.Event mouseUpEvent;
            switch (action.PressedButton)
            {
                case ClickAction.MouseButton.Right:
                    mouseDownEvent = VirtualMouse.Event.RightDown;
                    mouseUpEvent = VirtualMouse.Event.RightUp;
                    break;
                case ClickAction.MouseButton.Left:
                    mouseDownEvent = VirtualMouse.Event.LeftDown;
                    mouseUpEvent = VirtualMouse.Event.LeftUp;
                    break;
                default:
                    throw new NotImplementedException();
            }
            this.virtualMouse.MoveCursor(action.X, action.Y);
            this.virtualMouse.ExecuteMouseEvent(mouseDownEvent, action.X, action.Y);
            await Task.Delay(action.Duration).ConfigureAwait(false);
            this.virtualMouse.ExecuteMouseEvent(mouseUpEvent, action.X, action.Y);
        }
    }
}
