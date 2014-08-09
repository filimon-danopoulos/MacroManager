namespace MacroManager.WinForms
{
    partial class Playback
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
            this.macroListBox = new System.Windows.Forms.ListBox();
            this.macroDetails = new System.Windows.Forms.Panel();
            this.hideShortWaitActionCheckBox = new System.Windows.Forms.CheckBox();
            this.descriptionBox = new System.Windows.Forms.GroupBox();
            this.macroDetailsDescription = new System.Windows.Forms.Label();
            this.stopPlaybackButton = new System.Windows.Forms.Button();
            this.startPlaybackButton = new System.Windows.Forms.Button();
            this.macroActionsList = new System.Windows.Forms.ListView();
            this.actionTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.actionDescriptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.removeButton = new System.Windows.Forms.Button();
            this.macrosGroupBox = new System.Windows.Forms.GroupBox();
            this.actionProcessColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.macroDetails.SuspendLayout();
            this.descriptionBox.SuspendLayout();
            this.macrosGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // macroListBox
            // 
            this.macroListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.macroListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.macroListBox.FormattingEnabled = true;
            this.macroListBox.Location = new System.Drawing.Point(6, 16);
            this.macroListBox.Name = "macroListBox";
            this.macroListBox.Size = new System.Drawing.Size(228, 481);
            this.macroListBox.TabIndex = 1;
            this.macroListBox.SelectedIndexChanged += new System.EventHandler(this.macroListBox_SelectedIndexChanged);
            // 
            // macroDetails
            // 
            this.macroDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroDetails.Controls.Add(this.hideShortWaitActionCheckBox);
            this.macroDetails.Controls.Add(this.descriptionBox);
            this.macroDetails.Controls.Add(this.stopPlaybackButton);
            this.macroDetails.Controls.Add(this.startPlaybackButton);
            this.macroDetails.Controls.Add(this.macroActionsList);
            this.macroDetails.Location = new System.Drawing.Point(249, 3);
            this.macroDetails.Name = "macroDetails";
            this.macroDetails.Size = new System.Drawing.Size(543, 535);
            this.macroDetails.TabIndex = 2;
            // 
            // hideShortWaitActionCheckBox
            // 
            this.hideShortWaitActionCheckBox.AutoSize = true;
            this.hideShortWaitActionCheckBox.Checked = true;
            this.hideShortWaitActionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hideShortWaitActionCheckBox.Location = new System.Drawing.Point(0, 184);
            this.hideShortWaitActionCheckBox.Name = "hideShortWaitActionCheckBox";
            this.hideShortWaitActionCheckBox.Size = new System.Drawing.Size(134, 17);
            this.hideShortWaitActionCheckBox.TabIndex = 7;
            this.hideShortWaitActionCheckBox.Text = "Hide short WaitActions";
            this.hideShortWaitActionCheckBox.UseVisualStyleBackColor = true;
            this.hideShortWaitActionCheckBox.CheckedChanged += new System.EventHandler(this.hideShortWaitActionCheckBox_CheckedChanged);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Controls.Add(this.macroDetailsDescription);
            this.descriptionBox.Location = new System.Drawing.Point(0, 0);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(543, 174);
            this.descriptionBox.TabIndex = 6;
            this.descriptionBox.TabStop = false;
            this.descriptionBox.Text = "Description";
            // 
            // macroDetailsDescription
            // 
            this.macroDetailsDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroDetailsDescription.Location = new System.Drawing.Point(6, 16);
            this.macroDetailsDescription.Name = "macroDetailsDescription";
            this.macroDetailsDescription.Size = new System.Drawing.Size(531, 146);
            this.macroDetailsDescription.TabIndex = 0;
            // 
            // stopPlaybackButton
            // 
            this.stopPlaybackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopPlaybackButton.Enabled = false;
            this.stopPlaybackButton.Location = new System.Drawing.Point(297, 180);
            this.stopPlaybackButton.Name = "stopPlaybackButton";
            this.stopPlaybackButton.Size = new System.Drawing.Size(120, 23);
            this.stopPlaybackButton.TabIndex = 5;
            this.stopPlaybackButton.Text = "Stop";
            this.stopPlaybackButton.UseVisualStyleBackColor = true;
            this.stopPlaybackButton.Click += new System.EventHandler(this.stopPlaybackButton_Click);
            // 
            // startPlaybackButton
            // 
            this.startPlaybackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startPlaybackButton.Enabled = false;
            this.startPlaybackButton.Location = new System.Drawing.Point(423, 180);
            this.startPlaybackButton.Name = "startPlaybackButton";
            this.startPlaybackButton.Size = new System.Drawing.Size(120, 23);
            this.startPlaybackButton.TabIndex = 1;
            this.startPlaybackButton.Text = "Play Macro";
            this.startPlaybackButton.UseVisualStyleBackColor = true;
            this.startPlaybackButton.Click += new System.EventHandler(this.startPlaybackButton_Click);
            // 
            // macroActionsList
            // 
            this.macroActionsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroActionsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.actionTypeColumnHeader,
            this.actionProcessColumnHeader,
            this.actionDescriptionColumnHeader});
            this.macroActionsList.FullRowSelect = true;
            this.macroActionsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.macroActionsList.Location = new System.Drawing.Point(0, 209);
            this.macroActionsList.MultiSelect = false;
            this.macroActionsList.Name = "macroActionsList";
            this.macroActionsList.Size = new System.Drawing.Size(543, 326);
            this.macroActionsList.TabIndex = 0;
            this.macroActionsList.UseCompatibleStateImageBehavior = false;
            this.macroActionsList.View = System.Windows.Forms.View.Details;
            // 
            // actionTypeColumnHeader
            // 
            this.actionTypeColumnHeader.Text = "Action Type";
            this.actionTypeColumnHeader.Width = 100;
            // 
            // actionDescriptionColumnHeader
            // 
            this.actionDescriptionColumnHeader.Text = "Description";
            this.actionDescriptionColumnHeader.Width = 107;
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(3, 515);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(240, 23);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove Macro";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // macrosGroupBox
            // 
            this.macrosGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.macrosGroupBox.Controls.Add(this.macroListBox);
            this.macrosGroupBox.Location = new System.Drawing.Point(3, 3);
            this.macrosGroupBox.Name = "macrosGroupBox";
            this.macrosGroupBox.Size = new System.Drawing.Size(240, 506);
            this.macrosGroupBox.TabIndex = 4;
            this.macrosGroupBox.TabStop = false;
            this.macrosGroupBox.Text = "Macros";
            // 
            // actionProcessColumnHeader
            // 
            this.actionProcessColumnHeader.Text = "Process";
            // 
            // Playback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.macrosGroupBox);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.macroDetails);
            this.Name = "Playback";
            this.Size = new System.Drawing.Size(795, 541);
            this.Resize += new System.EventHandler(this.applicationTabs_Resize);
            this.macroDetails.ResumeLayout(false);
            this.macroDetails.PerformLayout();
            this.descriptionBox.ResumeLayout(false);
            this.macrosGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox macroListBox;
        private System.Windows.Forms.Panel macroDetails;
        private System.Windows.Forms.GroupBox descriptionBox;
        private System.Windows.Forms.Label macroDetailsDescription;
        private System.Windows.Forms.Button stopPlaybackButton;
        private System.Windows.Forms.Button startPlaybackButton;
        private System.Windows.Forms.ListView macroActionsList;
        private System.Windows.Forms.ColumnHeader actionTypeColumnHeader;
        private System.Windows.Forms.ColumnHeader actionDescriptionColumnHeader;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.GroupBox macrosGroupBox;
        private System.Windows.Forms.CheckBox hideShortWaitActionCheckBox;
        private System.Windows.Forms.ColumnHeader actionProcessColumnHeader;
    }
}
