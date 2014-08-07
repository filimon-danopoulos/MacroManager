namespace MacroManager.WinForms
{
    partial class Recording
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.macroDetailsGroup = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.editDescriptionLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.editNameLabel = new System.Windows.Forms.Label();
            this.startRecordingButton = new System.Windows.Forms.Button();
            this.stopRecordingButton = new System.Windows.Forms.Button();
            this.actionGroup = new System.Windows.Forms.GroupBox();
            this.removeActionButton = new System.Windows.Forms.Button();
            this.actionsListView = new System.Windows.Forms.ListView();
            this.actionTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.actionDescriptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.actionApplicationColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.discardMacroButton = new System.Windows.Forms.Button();
            this.macroDetailsGroup.SuspendLayout();
            this.actionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // macroDetailsGroup
            // 
            this.macroDetailsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroDetailsGroup.Controls.Add(this.discardMacroButton);
            this.macroDetailsGroup.Controls.Add(this.saveButton);
            this.macroDetailsGroup.Controls.Add(this.descriptionTextBox);
            this.macroDetailsGroup.Controls.Add(this.editDescriptionLabel);
            this.macroDetailsGroup.Controls.Add(this.nameTextBox);
            this.macroDetailsGroup.Controls.Add(this.editNameLabel);
            this.macroDetailsGroup.Location = new System.Drawing.Point(3, 3);
            this.macroDetailsGroup.Name = "macroDetailsGroup";
            this.macroDetailsGroup.Size = new System.Drawing.Size(882, 196);
            this.macroDetailsGroup.TabIndex = 3;
            this.macroDetailsGroup.TabStop = false;
            this.macroDetailsGroup.Text = "Macro Details";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(754, 167);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save Macro";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(82, 44);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(792, 117);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // editDescriptionLabel
            // 
            this.editDescriptionLabel.AutoSize = true;
            this.editDescriptionLabel.Location = new System.Drawing.Point(6, 47);
            this.editDescriptionLabel.Name = "editDescriptionLabel";
            this.editDescriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.editDescriptionLabel.TabIndex = 2;
            this.editDescriptionLabel.Text = "Description";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(82, 17);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(792, 20);
            this.nameTextBox.TabIndex = 1;
            // 
            // editNameLabel
            // 
            this.editNameLabel.AutoSize = true;
            this.editNameLabel.Location = new System.Drawing.Point(7, 20);
            this.editNameLabel.Name = "editNameLabel";
            this.editNameLabel.Size = new System.Drawing.Size(35, 13);
            this.editNameLabel.TabIndex = 0;
            this.editNameLabel.Text = "Name";
            // 
            // startRecordingButton
            // 
            this.startRecordingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startRecordingButton.Location = new System.Drawing.Point(754, 370);
            this.startRecordingButton.Name = "startRecordingButton";
            this.startRecordingButton.Size = new System.Drawing.Size(120, 23);
            this.startRecordingButton.TabIndex = 4;
            this.startRecordingButton.Text = "Record Actions";
            this.startRecordingButton.UseVisualStyleBackColor = true;
            this.startRecordingButton.Click += new System.EventHandler(this.startRecordingButton_Click);
            // 
            // stopRecordingButton
            // 
            this.stopRecordingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopRecordingButton.Enabled = false;
            this.stopRecordingButton.Location = new System.Drawing.Point(628, 370);
            this.stopRecordingButton.Name = "stopRecordingButton";
            this.stopRecordingButton.Size = new System.Drawing.Size(120, 23);
            this.stopRecordingButton.TabIndex = 5;
            this.stopRecordingButton.Text = "Stop Recording";
            this.stopRecordingButton.UseVisualStyleBackColor = true;
            this.stopRecordingButton.Click += new System.EventHandler(this.stopRecordingButton_Click);
            // 
            // actionGroup
            // 
            this.actionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionGroup.Controls.Add(this.removeActionButton);
            this.actionGroup.Controls.Add(this.stopRecordingButton);
            this.actionGroup.Controls.Add(this.startRecordingButton);
            this.actionGroup.Controls.Add(this.actionsListView);
            this.actionGroup.Location = new System.Drawing.Point(3, 205);
            this.actionGroup.Name = "actionGroup";
            this.actionGroup.Size = new System.Drawing.Size(882, 399);
            this.actionGroup.TabIndex = 6;
            this.actionGroup.TabStop = false;
            this.actionGroup.Text = "Actions";
            // 
            // removeActionButton
            // 
            this.removeActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeActionButton.Enabled = false;
            this.removeActionButton.Location = new System.Drawing.Point(6, 370);
            this.removeActionButton.Name = "removeActionButton";
            this.removeActionButton.Size = new System.Drawing.Size(120, 23);
            this.removeActionButton.TabIndex = 1;
            this.removeActionButton.Text = "Remove Action";
            this.removeActionButton.UseVisualStyleBackColor = true;
            // 
            // actionsListView
            // 
            this.actionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionsListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.actionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.actionTypeColumnHeader,
            this.actionApplicationColumnHeader,
            this.actionDescriptionColumnHeader});
            this.actionsListView.FullRowSelect = true;
            this.actionsListView.Location = new System.Drawing.Point(6, 19);
            this.actionsListView.MultiSelect = false;
            this.actionsListView.Name = "actionsListView";
            this.actionsListView.Size = new System.Drawing.Size(868, 343);
            this.actionsListView.TabIndex = 0;
            this.actionsListView.UseCompatibleStateImageBehavior = false;
            this.actionsListView.View = System.Windows.Forms.View.Details;
            // 
            // actionTypeColumnHeader
            // 
            this.actionTypeColumnHeader.Text = "Action Type";
            this.actionTypeColumnHeader.Width = 100;
            // 
            // actionDescriptionColumnHeader
            // 
            this.actionDescriptionColumnHeader.Text = "Description";
            this.actionDescriptionColumnHeader.Width = 552;
            // 
            // actionApplicationColumnHeader
            // 
            this.actionApplicationColumnHeader.Text = "Application";
            this.actionApplicationColumnHeader.Width = 117;
            // 
            // discardMacroButton
            // 
            this.discardMacroButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.discardMacroButton.Location = new System.Drawing.Point(628, 167);
            this.discardMacroButton.Name = "discardMacroButton";
            this.discardMacroButton.Size = new System.Drawing.Size(120, 23);
            this.discardMacroButton.TabIndex = 5;
            this.discardMacroButton.Text = "Discard Macro";
            this.discardMacroButton.UseVisualStyleBackColor = true;
            this.discardMacroButton.Click += new System.EventHandler(this.discardMacroButton_Click);
            // 
            // Recording
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.actionGroup);
            this.Controls.Add(this.macroDetailsGroup);
            this.Name = "Recording";
            this.Size = new System.Drawing.Size(888, 607);
            this.Resize += new System.EventHandler(this.Recording_Resize);
            this.macroDetailsGroup.ResumeLayout(false);
            this.macroDetailsGroup.PerformLayout();
            this.actionGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox macroDetailsGroup;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label editDescriptionLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label editNameLabel;
        private System.Windows.Forms.Button stopRecordingButton;
        private System.Windows.Forms.Button startRecordingButton;
        private System.Windows.Forms.GroupBox actionGroup;
        private System.Windows.Forms.Button removeActionButton;
        private System.Windows.Forms.ListView actionsListView;
        private System.Windows.Forms.ColumnHeader actionTypeColumnHeader;
        private System.Windows.Forms.ColumnHeader actionDescriptionColumnHeader;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ColumnHeader actionApplicationColumnHeader;
        private System.Windows.Forms.Button discardMacroButton;
    }
}
