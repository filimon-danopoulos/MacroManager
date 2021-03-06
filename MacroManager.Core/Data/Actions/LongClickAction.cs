﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Data.Actions
{
    public class LongClickAction : ClickAction
    {
        public LongClickAction(int x, int y, MouseButton pressedButton, int duration) : this(x, y, pressedButton, duration, "")
        {
        }
        public LongClickAction(int x, int y, MouseButton pressedButton, int duration, string process)
            : base(x, y, pressedButton, process)
        {
            this.Duration = duration;
        }


        public int Duration
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format(
                "Long {0} click at ({1}, {2}) for {3} seconds",
                this.PressedButton == MouseButton.Left ? "left" : "right",
                this.X,
                this.Y,
                this.Duration / 1000f
            );
        }
    }
}
