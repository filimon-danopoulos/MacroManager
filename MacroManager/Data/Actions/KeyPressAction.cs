using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Data.Actions
{
    public class KeyPressAction : UserAction
    {
        public KeyPressAction(int vkCode)
        {
            this.VirtualKey = vkCode;
        }
        public int VirtualKey { get; private set; }
    }
}
