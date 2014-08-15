using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback.Strategies
{
    [Executes(typeof(ClickAction))]
    public class Click : PlaybackStrategy
    {
        public override async Task ExecuteAsync(UserAction userAction)
        {
            await Task.Run(() =>
            {
                var action = userAction as ClickAction;

                VirtualMouse.Event mouseEvent;
                switch (action.PressedButton)
                {
                    case ClickAction.MouseButton.Right:
                        mouseEvent = VirtualMouse.Event.RightClick;
                        break;
                    case ClickAction.MouseButton.Left:
                        mouseEvent = VirtualMouse.Event.LeftClick;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                this.virtualMouse.MoveCursor(action.X, action.Y);
                this.virtualMouse.ExecuteMouseEvent(mouseEvent, action.X, action.Y);
            });
        }
    }
}
