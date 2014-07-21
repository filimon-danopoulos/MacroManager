using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var macroRepository = new XmlMacroRepository();
            var hookService = new HookService();
            var macroService = new MacroService(macroRepository, hookService);
            Application.Run(new Main(macroService));
        }
    }
}
