﻿namespace MacroManager
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.maniMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationTabs = new System.Windows.Forms.TabControl();
            this.playbackTab = new System.Windows.Forms.TabPage();
            this.macroList = new System.Windows.Forms.ListView();
            this.playbackButton = new System.Windows.Forms.Button();
            this.recordTab = new System.Windows.Forms.TabPage();
            this.editMacroGroup = new System.Windows.Forms.GroupBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.editNameLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.recordButton = new System.Windows.Forms.Button();
            this.editDescriptionLabel = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.maniMenuStrip.SuspendLayout();
            this.applicationTabs.SuspendLayout();
            this.playbackTab.SuspendLayout();
            this.recordTab.SuspendLayout();
            this.editMacroGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // maniMenuStrip
            // 
            this.maniMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.maniMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.maniMenuStrip.Name = "maniMenuStrip";
            this.maniMenuStrip.Size = new System.Drawing.Size(700, 24);
            this.maniMenuStrip.TabIndex = 0;
            this.maniMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // applicationTabs
            // 
            this.applicationTabs.Controls.Add(this.playbackTab);
            this.applicationTabs.Controls.Add(this.recordTab);
            this.applicationTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationTabs.Location = new System.Drawing.Point(0, 24);
            this.applicationTabs.Name = "applicationTabs";
            this.applicationTabs.SelectedIndex = 0;
            this.applicationTabs.Size = new System.Drawing.Size(700, 338);
            this.applicationTabs.TabIndex = 2;
            // 
            // playbackTab
            // 
            this.playbackTab.Controls.Add(this.macroList);
            this.playbackTab.Controls.Add(this.playbackButton);
            this.playbackTab.Location = new System.Drawing.Point(4, 22);
            this.playbackTab.Name = "playbackTab";
            this.playbackTab.Padding = new System.Windows.Forms.Padding(3);
            this.playbackTab.Size = new System.Drawing.Size(692, 312);
            this.playbackTab.TabIndex = 0;
            this.playbackTab.Text = "Playback Macro";
            this.playbackTab.UseVisualStyleBackColor = true;
            // 
            // macroList
            // 
            this.macroList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroList.GridLines = true;
            this.macroList.Location = new System.Drawing.Point(6, 6);
            this.macroList.MultiSelect = false;
            this.macroList.Name = "macroList";
            this.macroList.Size = new System.Drawing.Size(680, 274);
            this.macroList.TabIndex = 1;
            this.macroList.TileSize = new System.Drawing.Size(168, 50);
            this.macroList.UseCompatibleStateImageBehavior = false;
            this.macroList.View = System.Windows.Forms.View.Tile;
            // 
            // playbackButton
            // 
            this.playbackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.playbackButton.Location = new System.Drawing.Point(611, 286);
            this.playbackButton.Name = "playbackButton";
            this.playbackButton.Size = new System.Drawing.Size(75, 23);
            this.playbackButton.TabIndex = 0;
            this.playbackButton.Text = "Playback First Macro";
            this.playbackButton.UseVisualStyleBackColor = true;
            this.playbackButton.Click += new System.EventHandler(this.playbackButton_Click);
            // 
            // recordTab
            // 
            this.recordTab.Controls.Add(this.editMacroGroup);
            this.recordTab.Controls.Add(this.stopButton);
            this.recordTab.Controls.Add(this.recordButton);
            this.recordTab.Location = new System.Drawing.Point(4, 22);
            this.recordTab.Name = "recordTab";
            this.recordTab.Padding = new System.Windows.Forms.Padding(3);
            this.recordTab.Size = new System.Drawing.Size(692, 312);
            this.recordTab.TabIndex = 1;
            this.recordTab.Text = "Record Macro";
            this.recordTab.UseVisualStyleBackColor = true;
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
            this.editMacroGroup.Location = new System.Drawing.Point(6, 6);
            this.editMacroGroup.Name = "editMacroGroup";
            this.editMacroGroup.Size = new System.Drawing.Size(683, 274);
            this.editMacroGroup.TabIndex = 2;
            this.editMacroGroup.TabStop = false;
            this.editMacroGroup.Text = "Edit Macro";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(82, 17);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(595, 20);
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
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(564, 286);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(125, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop Recording";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // recordButton
            // 
            this.recordButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.recordButton.Enabled = false;
            this.recordButton.Location = new System.Drawing.Point(448, 286);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(110, 23);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Start Recording";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
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
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(82, 44);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(595, 154);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 362);
            this.Controls.Add(this.applicationTabs);
            this.Controls.Add(this.maniMenuStrip);
            this.MainMenuStrip = this.maniMenuStrip;
            this.Name = "Main";
            this.Text = "Macro Manager";
            this.maniMenuStrip.ResumeLayout(false);
            this.maniMenuStrip.PerformLayout();
            this.applicationTabs.ResumeLayout(false);
            this.playbackTab.ResumeLayout(false);
            this.recordTab.ResumeLayout(false);
            this.editMacroGroup.ResumeLayout(false);
            this.editMacroGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip maniMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.TabControl applicationTabs;
        private System.Windows.Forms.TabPage playbackTab;
        private System.Windows.Forms.ListView macroList;
        private System.Windows.Forms.Button playbackButton;
        private System.Windows.Forms.TabPage recordTab;
        private System.Windows.Forms.GroupBox editMacroGroup;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label editNameLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Label editDescriptionLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;


    }
}

