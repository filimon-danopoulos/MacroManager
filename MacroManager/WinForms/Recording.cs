using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MacroManager.Data;

namespace MacroManager.WinForms
{
    public partial class Recording : UserControl
    {
        #region Constructors

        public Recording()
        {
            InitializeComponent();
        }

        #endregion

        #region Events 

        #region Nested class RecordingEventArgs

        public class RecordingEventArgs : EventArgs
        {
            public RecordingEventArgs(string macroName, string macroDescription)
            {
                this.MacroName = macroName;
                this.MacroDescription = macroDescription;
            }

            public string MacroName
            {
                get;
                private set;
            }

            public string MacroDescription
            {
                get;
                private set;
            }
        }

        #endregion

        /// <summary>
        /// Fires when the recording of macro starts.
        /// </summary>
        public event EventHandler StartRecording;

        /// <summary>
        /// Wrapper for firing the StartRecording event, includes handler null check.
        /// </summary>
        private void OnStartRecording()
        {
            var handler = this.StartRecording;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Fires after the recording has stoped. Carries the name and description of the macro.
        /// </summary>
        public event EventHandler<RecordingEventArgs> StopRecording;

        /// <summary>
        /// Wrapper for firing the StopRecording event. Check the handler for null before firing the event.
        /// </summary>
        private void OnStopRecording(RecordingEventArgs args)
        {
            var handler = this.StopRecording;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Event handlers

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            this.nameTextBox.Enabled = false;
            this.descriptionTextBox.Enabled = false;

            this.startRecordingButton.Enabled = false;
            this.stopRecordingButton.Enabled = true;
            this.OnStartRecording();
        }

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            
            var args = new RecordingEventArgs(this.nameTextBox.Text, this.descriptionTextBox.Name);
            this.OnStopRecording(args);

            this.nameTextBox.Enabled = true;
            this.nameTextBox.Text = "";

            this.descriptionTextBox.Enabled = true;
            this.descriptionTextBox.Text = "";

            this.stopRecordingButton.Enabled = false;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.startRecordingButton.Enabled = this.nameTextBox.Text != "";
        }

        #endregion
    }
}
