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
using System.IO;
using MacroManager.WinForms;

namespace MacroManager
{
    public partial class Main : Form
    {

        #region Fields

        private MacroService macroService;

        #endregion

        #region Constructors

        public Main()
        {
            InitializeComponent();

            var fileName = String.Format("macros_{0}.xml", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                fileName
            );

            this.macroService = new MacroService(new HookService(), new XmlMacroRepository(path, true));
            this.macroService.RecordingStopped += (sender, e) =>
            {
                var macros = this.macroService.GetAllMacros().ToList();
                this.playbackControll.LoadMacros(macros);
            };

            this.SetApplicationTile(fileName.Substring(0, fileName.Length - 4));

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks for unsaved changes and prompts the user for a save. Then returns a boolean representing what is to be done.
        /// Returns true if the application is to be terminated.
        /// </summary>
        private bool CanCloseOpenFile()
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

        /// <summary>
        /// Sets the application title with a file name.
        /// </summary>
        private void SetApplicationTile(string fileName)
        {
            this.Text = String.Format("Macro Manager - {0}", fileName);
        }
        
        #endregion

        #region Eventhandlers

        #region Toolstrip

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CanCloseOpenFile())
            {
                Application.Exit();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.CanCloseOpenFile())
            {
                return;
            }
            var dialog = new OpenFileDialog()
            {
                Title = "Open Macro file...",
                Multiselect = false,
                CheckFileExists = true,
                Filter = "XML (*.xml)|*.xml"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }
            var fileName = dialog.FileName;
            var macroRepository = new XmlMacroRepository(fileName);
            this.macroService.UpdateRepository(macroRepository);
            var macros = this.macroService.GetAllMacros().ToList();
            this.playbackControll.LoadMacros(macros);
            this.SetApplicationTile(Path.GetFileNameWithoutExtension(fileName));
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

        private void playbackControll_StartPlayback(object sender, Playback.PlaybackEventArgs eventArgs)
        {
            this.macroService.StartPlayback(eventArgs.SelectedMacro);
            this.statusMessage.Text = "Playing Macro...";
        }

        private void playbackControll_StopPlayback(object sender, Playback.PlaybackEventArgs e)
        {
            this.macroService.StopPlayback();
            this.statusMessage.Text = "Playback stopped!";
        }
        
        private void playbackControll_RemoveMacro(object sender, Playback.PlaybackEventArgs args)
        {
            this.macroService.RemoveMacro(args.SelectedMacro);
            this.playbackControll.LoadMacros(this.macroService.GetAllMacros().ToList());
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.CanCloseOpenFile();
        }

        private void recordingControll_StartRecording(object sender, EventArgs e)
        {
            this.statusMessage.Text = "Recording Macro...";
            this.macroService.StartRecording();
            this.WindowState = FormWindowState.Minimized;
        }

        private void recordingControll_StopRecording(object sender, Recording.RecordingEventArgs args)
        {
            this.macroService.StopRecording(args.MacroName, args.MacroDescription);
            this.statusMessage.Text = "Macro recorded!";
        }


        #endregion


    }
}
