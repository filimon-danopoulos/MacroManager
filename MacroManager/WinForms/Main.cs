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

        public Main(IMacroService macroService) : this()
        {
            this.macroService = macroService;
            this.macroService.RecordingStoped += (sender, e) => this.LoadMacros();
            this.LoadMacros();
        }

        private void LoadMacros()
        {
            var macros = this.macroService.GetAllMacros();
            foreach (var macro in macros)
            {
                this.macroList.Items.Add(new ListViewItem(macro.MacroId.ToString()));
            }
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (this.nameTextBox.Text == "")
            {
                throw new Exception("Cannot create a macro whithout a name!");
            }
            if (this.currentMacro != null)
            {
                throw new Exception("User should not be able to click on start when a macro is recording.");
            }
            this.currentMacro = this.macroService.CreateMacro(this.nameTextBox.Text, this.descriptionTextBox.Text);
            this.macroService.StartRecording(this.currentMacro);

            this.nameTextBox.Enabled = false;
            this.descriptionTextBox.Enabled = false;

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

            this.nameTextBox.Enabled = true;
            this.nameTextBox.Text = "";

            this.descriptionTextBox.Enabled = true;
            this.descriptionTextBox.Text = "";

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

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogInput = MessageBox.Show(
                "Are you sure you want to quit?", 
                "Quit?", 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Question
            );
            if (dialogInput == DialogResult.OK) { 
                Application.Exit();
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.recordButton.Enabled = this.nameTextBox.Text != "";
        }

    }
}
