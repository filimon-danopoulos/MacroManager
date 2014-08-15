using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroManager.Core.Data
{
    public interface IFileMacroRepository : IMacroRepository
    {
        void SaveChanges(string fileName);
        void LoadData(string fileName);
    }
}
