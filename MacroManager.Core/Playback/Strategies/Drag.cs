using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Playback.Strategies
{
    [Executes(typeof(DragAction))]
    public class Drag : PlaybackStrategy
    {
        public override async Task ExecuteAsync(UserAction userAction)
        {
            var action = userAction as DragAction;
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
            var firstPoint = action.Path.First();
            this.virtualMouse.MoveCursor(firstPoint.X, firstPoint.Y);
            this.virtualMouse.ExecuteMouseEvent(mouseDownEvent, firstPoint.X, firstPoint.Y);
            foreach (var point in action.Path.Skip(1))
            {
                await Task.Delay(VirtualMouse.DRAG_INTERVAL).ConfigureAwait(false);
                this.virtualMouse.MoveCursor(point.X, point.Y);
            }
            var lastPoint = action.Path.Last();
            this.virtualMouse.ExecuteMouseEvent(mouseUpEvent, lastPoint.X, lastPoint.Y);
        }
    }
}
