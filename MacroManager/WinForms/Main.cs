using MacroManager.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroManager
{
    public partial class Main : Form
    {
        private IMacroService macroService;
        private Macro currentMacro;

        public Main()
        {
            InitializeComponent();
        }

        public Main(IMacroService macroService)
            : this()
        {
            this.macroService = macroService;
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (this.currentMacro != null)
            {
                throw new Exception("User should not be able to click on start when a macro is recording.");
            }
            this.currentMacro = this.macroService.CreateNewMacro();
            this.macroService.StartRecording(this.currentMacro);

            this.recordButton.Enabled = false;
            this.stopButton.Enabled = true;

            this.WindowState = FormWindowState.Minimized;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (this.currentMacro == null)
            {
                throw new Exception("User should not be able to click on stop button if a macro is not recording.");
            }

            this.macroService.StopRecording(this.currentMacro);

            this.currentMacro = null;
            this.recordButton.Enabled = true;
            this.stopButton.Enabled = false;
        }

        private void playbackButton_Click(object sender, EventArgs e)
        {
            var firstMacro = this.macroService.GetAllMacros().FirstOrDefault();
            if (firstMacro == null)
            {
                throw new Exception("No macros to run");
            }
            this.macroService.ReplayMacro(firstMacro);
        }


    }
}
