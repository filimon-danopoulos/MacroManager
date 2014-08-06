
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Data
{
    public interface IMacroRepository
    {
        IEnumerable<Macro> Read();
        void Add(Macro macro);
        void Remove(Macro macro);
        void SaveChanges();
        bool HasChanges();
    }
}
