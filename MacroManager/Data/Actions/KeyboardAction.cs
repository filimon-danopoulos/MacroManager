using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroManager.Data.Actions
{
    public class KeyboardAction : UserAction
    {
        public KeyboardAction(int vkCode)
        {
            this.VirtualKey = vkCode;
        }

        public int VirtualKey { get; private set; }

        public override string ToString()
        {
            return String.Format(
                "'{0}' pressed.",
                (Keys)this.VirtualKey
            );
        }
    }
}
