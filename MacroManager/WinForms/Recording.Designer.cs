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
            this.editMacroGroup = new System.Windows.Forms.GroupBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.editDescriptionLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.editNameLabel = new System.Windows.Forms.Label();
            this.stopRecordingButton = new System.Windows.Forms.Button();
            this.startRecordingButton = new System.Windows.Forms.Button();
            this.editMacroGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // editMacroGroup
            // 
            this.editMacroGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editMacroGroup.Controls.Add(this.descriptionTextBox);
            this.editMacroGroup.Controls.Add(this.editDescriptionLabel);
            this.editMacroGroup.Controls.Add(this.nameTextBox);
            this.editMacroGroup.Controls.Add(this.editNameLabel);
            this.editMacroGroup.Location = new System.Drawing.Point(3, 3);
            this.editMacroGroup.Name = "editMacroGroup";
            this.editMacroGroup.Size = new System.Drawing.Size(972, 684);
            this.editMacroGroup.TabIndex = 3;
            this.editMacroGroup.TabStop = false;
            this.editMacroGroup.Text = "Edit Macro";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(82, 44);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(882, 634);
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
            this.nameTextBox.Size = new System.Drawing.Size(882, 20);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
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
            // stopRecordingButton
            // 
            this.stopRecordingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopRecordingButton.Enabled = false;
            this.stopRecordingButton.Location = new System.Drawing.Point(729, 690);
            this.stopRecordingButton.Name = "stopRecordingButton";
            this.stopRecordingButton.Size = new System.Drawing.Size(120, 23);
            this.stopRecordingButton.TabIndex = 5;
            this.stopRecordingButton.Text = "Stop Recording";
            this.stopRecordingButton.UseVisualStyleBackColor = true;
            this.stopRecordingButton.Click += new System.EventHandler(this.stopRecordingButton_Click);
            // 
            // startRecordingButton
            // 
            this.startRecordingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startRecordingButton.Enabled = false;
            this.startRecordingButton.Location = new System.Drawing.Point(855, 690);
            this.startRecordingButton.Name = "startRecordingButton";
            this.startRecordingButton.Size = new System.Drawing.Size(120, 23);
            this.startRecordingButton.TabIndex = 4;
            this.startRecordingButton.Text = "Start Recording";
            this.startRecordingButton.UseVisualStyleBackColor = true;
            this.startRecordingButton.Click += new System.EventHandler(this.startRecordingButton_Click);
            // 
            // Recording
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stopRecordingButton);
            this.Controls.Add(this.startRecordingButton);
            this.Controls.Add(this.editMacroGroup);
            this.Name = "Recording";
            this.Size = new System.Drawing.Size(978, 716);
            this.editMacroGroup.ResumeLayout(false);
            this.editMacroGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox editMacroGroup;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label editDescriptionLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label editNameLabel;
        private System.Windows.Forms.Button stopRecordingButton;
        private System.Windows.Forms.Button startRecordingButton;
    }
}
