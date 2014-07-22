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
        #region Constants

        private const string NO_FILE_SELECTED_MESSAGE = "Please open saved macros or create new...";
        private const string RECORD_TAB_STATUS_EMPTY_NAME_MESSAGE = "Name is required!";
        private const string RECORD_TAB_STATUS_MESSAGE = "Waiting for recording to start...";
        private const string PLAYBACK_TAB_STATUS_MESSAGE = "Select a Macro...";

        #endregion

        #region Fields

        private MacroService macroService;
        private Macro currentMacro;

        #endregion

        #region Constructors

        public Main()
        {
            InitializeComponent();
        }

        public Main(MacroService macroService)
            : this()
        {
            this.macroService = macroService;
            this.macroService.RecordingStopped += (sender, e) => this.LoadMacros();
            this.statusMessage.Text = NO_FILE_SELECTED_MESSAGE;
        }

        #endregion

        private void LoadMacros()
        {
            var macros = this.macroService.GetAllMacros();

            this.playbackButton.Enabled = macros.Count() > 0;

            this.macroList.Items.Clear();

            foreach (var macro in macros)
            {
                this.macroList.Items.Add(
                    new ListViewItem(new[] { macro.Name, macro.Description })
                    {
                        Tag = macro.MacroId
                    }
                );
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
            var selected = this.macroList.SelectedItems;
            if (selected.Count != 1)
            {
                throw new Exception("No macro selected from list");
            }
            var selectedGuid = (Guid)selected[0].Tag;
            var macro = this.macroService
                .GetAllMacros()
                .FirstOrDefault(x => x.MacroId == selectedGuid);
            if (macro == null)
            {
                throw new Exception("No macros to run");
            }
            this.macroService.ReplayMacro(macro);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogInput = MessageBox.Show(
                "Are you sure you want to quit?",
                "Quit?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );
            if (dialogInput == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.recordButton.Enabled = this.nameTextBox.Text != "";
            this.UpdateStatusText();
        }

        private void applicationTabs_Click(object sender, EventArgs e)
        {
            this.UpdateStatusText();
        }

        private void UpdateStatusText()
        {
            var message = "";
            if (this.applicationTabs.SelectedIndex == 1)
            {
                if (this.nameTextBox.Text == "")
                {
                    message = RECORD_TAB_STATUS_EMPTY_NAME_MESSAGE;
                }
                else
                {
                    message = RECORD_TAB_STATUS_MESSAGE;
                }
            }
            else
            {
                message = PLAYBACK_TAB_STATUS_MESSAGE;
            }
            this.statusMessage.Text = message;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog() { 
                Title = "Open Macro file...",
                Multiselect = false,
                CheckFileExists = true
            };
            dialog.ShowDialog();
            var fileName = dialog.FileName;
            var macroRepository = new XmlMacroRepository(fileName);
            this.macroService.IntitializeRepository(macroRepository);
            this.LoadMacros();
            this.applicationTabs.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.macroService.SaveChanges();
        }


    }
}
