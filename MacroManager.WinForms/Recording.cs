﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MacroManager.Core.Data;
using MacroManager.Core.Recording;
using MacroManager.Core.Data.Actions;

namespace MacroManager.WinForms
{
    public partial class Recording : UserControl
    {
        #region Fields 

        private RecordingService recordingService;

        #endregion

        #region Constructors

        public Recording()
        {
            InitializeComponent();
            this.recordingService = new RecordingService();
        }

        #endregion

        #region Events 

        #region Nested class RecordingEventArgs

        public class RecordingEventArgs : EventArgs
        {
            public RecordingEventArgs(Macro macro)
            {
                this.RecordedMacro = macro;
            }

            public Macro RecordedMacro
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
        /// Fires after the recording has stoped.
        /// </summary>
        public event EventHandler StopRecording;

        /// <summary>
        /// Wrapper for firing the StopRecording event. Check the handler for null before firing the event.
        /// </summary>
        private void OnStopRecording()
        {
            var handler = this.StopRecording;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Fires when the save button is clicked. 
        /// </summary>
        public event EventHandler<RecordingEventArgs> SaveRecording;

        /// <summary>
        /// 
        /// </summary>
        private void OnSaveRecording(RecordingEventArgs args) {
            var handler = this.SaveRecording;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Event handlers

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            this.startRecordingButton.Enabled = false;
            this.stopRecordingButton.Enabled = true;

            this.recordingService.StartRecording();
            this.OnStartRecording();
        }

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            this.stopRecordingButton.Enabled = false;
            this.removeActionButton.Enabled = true;
            this.editActionButton.Enabled = true;

            this.recordingService.StopRecording();
            this.LoadActions();
            this.ResizeActionColumns();
            this.OnStopRecording();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var name = this.nameTextBox.Text;

            if (name == "")
            {
                MessageBox.Show(
                    "Cannot create a Macro without a name!",
                    "Name is required!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return;
            }

            var description = this.descriptionTextBox.Text;
            var actions = this.recordingService.GetRecordedActions().ToList();
            this.ResetRecordForm();
            this.OnSaveRecording(new RecordingEventArgs(new Macro(actions, name, description)));
        }

        private void discardMacroButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "All changes made to the new macro will be discarded. Do you want to continue?",
                "Are you sure?", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                this.ResetRecordForm();
            }
        }

        private void editActionButton_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedAction())
            {
                return;
            }

            var selectedAction = this.GetSelectedAction();
            using (var form = new EditAction(selectedAction))
            {
                var dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
                this.LoadActions();
            }
        }

        private void Recording_Resize(object sender, EventArgs e)
        {
            this.ResizeActionColumns();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Resizes the columns in the actionListView.
        /// </summary>
        private void ResizeActionColumns()
        {
            this.actionsListView.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Resets the record input-form to it's original state.
        /// </summary>
        private void ResetRecordForm()
        {
            this.nameTextBox.Text = "";
            this.descriptionTextBox.Text = "";
            this.actionsListView.Items.Clear();
            this.stopRecordingButton.Enabled = false;
            this.startRecordingButton.Enabled = true;
            this.removeActionButton.Enabled = false;
            this.editActionButton.Enabled = false;
        }

        /// <summary>
        /// Returns the selected action
        /// </summary>
        private UserAction GetSelectedAction()
        {
            if (!this.HasSelectedAction())
            {
                return null;
            }
            if (this.actionsListView.MultiSelect)
            {
                throw new Exception("The actionsListView can not have MultiSelect set to true");
            }

            return this.recordingService.GetRecordedActions().ToArray()[this.actionsListView.SelectedIndices[0]];

        }

        /// <summary>
        /// Returns true if the use has selected any actions.
        /// </summary>
        private bool HasSelectedAction()
        {
            return this.actionsListView.SelectedIndices.Count > 0;
        }

        /// <summary>
        /// Loads the actions that have been recorded.
        /// </summary>
        private void LoadActions()
        {
            this.actionsListView.Items.Clear();
            foreach (var action in this.recordingService.GetRecordedActions())
            {
                this.actionsListView.Items.Add(new ListViewItem(new[] {
                    action.GetType().Name,
                    action.Process,
                    action.ToString()
                }));
            }
        }
        #endregion

    }
}
