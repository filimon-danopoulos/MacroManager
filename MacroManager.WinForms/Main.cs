using MacroManager.Core.Data;
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

        private IFileMacroRepository macroRepository;

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


            this.macroRepository = new XmlMacroRepository(path, true);

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
                hasChanges = this.macroRepository.HasChanges();
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
                this.macroRepository.SaveChanges();
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
            this.macroRepository.LoadData(fileName);
            this.playbackControll.LoadMacros(this.macroRepository.Read().ToList());
            this.SetApplicationTile(Path.GetFileNameWithoutExtension(fileName));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.macroRepository.SaveChanges();
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
            this.macroRepository.SaveChanges(fileName);
        }

        #endregion

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.CanCloseOpenFile();
        }

        #region Playback Controll

        private void playbackControll_StartPlayback(object sender, EventArgs e)
        {
            this.statusMessage.Text = "Playing Macro...";
        }

        private void playbackControll_StopPlayback(object sender, EventArgs e)
        {
            this.statusMessage.Text = "Playback Finished, macro will reset in five seconds.";
        }

        private void playbackControll_CancelPlayback(object sender, EventArgs e)
        {
            this.statusMessage.Text = "Playback Canceled!";
        }

        private void playbackControll_RemoveMacro(object sender, MacroManager.WinForms.Playback.RemoveMacroEventArgs args)
        {
            this.macroRepository.Remove(args.SelectedMacro);
            this.playbackControll.LoadMacros(this.macroRepository.Read().ToList());
        }

        private void playbackControll_PlaybackError(object sender, EventArgs e)
        {
            this.statusMessage.Text = "Playback failed!";
        }

        #endregion

        #region Recording Controll

        private void recordingControll_StartRecording(object sender, EventArgs e)
        {
            this.statusMessage.Text = "Recording Macro...";
            this.WindowState = FormWindowState.Minimized;
        }

        private void recordingControll_StopRecording(object sender, EventArgs e)
        {
            this.statusMessage.Text = "Actions recorded!";
        }

        private void recordingControll_SaveRecording(object sender, WinForms.Recording.RecordingEventArgs args)
        {
            this.statusMessage.Text = "Macro saved.";
            this.macroRepository.Add(args.RecordedMacro);
            this.playbackControll.LoadMacros(this.macroRepository.Read().ToList());
        }

        #endregion

        #endregion

    }
}
