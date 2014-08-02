using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Data.Actions
{
    public class DragAction : UserAction
    {
        public DragAction(ClickAction.MouseButton button, IEnumerable<Point> path) 
        {
            this.Path = path;
        }
        public IEnumerable<Point> Path
        {
            get;
            private set;
        }
        public ClickAction.MouseButton PressedButton
        {
            get;
            private set;
        }
    }
}
