using MacroManager.Hooks;
using MacroManager.Data;
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

        #region Private Methods

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

        /// <summary>
        /// Checks for unsaved changes and prompts the user for a save. Then returns a boolean representing what is to be done.
        /// Returns true if the application is to be terminated.
        /// </summary>
        private bool Quit()
        {
            bool hasChanges;
            try
            {
                hasChanges = this.macroService.HasChanges();
            }
            // This will happen if the user exits before a macro file has been opened. 
            catch (NullReferenceException)
            {
                hasChanges = false;
            }
            if (!hasChanges)
            {
                return true;
            }
            var dialogInput = MessageBox.Show(
                "There are unsaved changes. Do you want to save them before exit?",
                "Unsaved changes...",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );
            if (dialogInput == DialogResult.Cancel)
            {
                return false;
            }
            if (dialogInput == DialogResult.Yes)
            {
                this.macroService.SaveChanges();
            }
            return true;
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

        #endregion

        #region Eventhandlers

        #region Record Macro Tab

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

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.recordButton.Enabled = this.nameTextBox.Text != "";
            this.UpdateStatusText();
        }
        #endregion

        #region Playback Macro Tab

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


        private void removeButton_Click(object sender, EventArgs e)
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
            this.macroService.RemoveMacro(macro);
            this.LoadMacros();
        }

        #endregion

        #region Toolstrip

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Quit())
            {
                Application.Exit();
            }
        }


        private void applicationTabs_Click(object sender, EventArgs e)
        {
            this.UpdateStatusText();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Open Macro file...",
                Multiselect = false,
                CheckFileExists = true
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            var fileName = dialog.FileName;
            var macroRepository = new XmlMacroRepository(fileName);
            this.macroService.IntitializeRepository(macroRepository);
            this.LoadMacros();
            this.applicationTabs.Enabled = true;
            this.saveAsToolStripMenuItem.Enabled = true;
            this.saveToolStripMenuItem.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.macroService.SaveChanges();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog()
            {
                Title = "Save changes...",
                OverwritePrompt = true,
                Filter = "XML (*.xml)|*.xml"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            var fileName = dialog.FileName;
            this.macroService.SaveChanges(fileName);
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.Quit();
        }

        #endregion

    }
}
