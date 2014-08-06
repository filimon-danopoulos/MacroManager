using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Data.Actions
{
    public class DragAction : MouseAction
    {
        public DragAction(MouseButton button, IEnumerable<Point> path) 
        {
            this.PressedButton = button;
            this.Path = path;
        }
        public IEnumerable<Point> Path
        {
            get;
            private set;
        }

        public override string ToString()
        {
            var first = this.Path.First();
            var last = this.Path.Last();
            return String.Format(
                "{0} click drag from ({1}, {2}) to ({3}, {4}).",
                this.PressedButton == MouseButton.Left ? "Left" : "Right",
                first.X,
                first.Y,
                last.X,
                last.Y
            );
        }
    }
}
