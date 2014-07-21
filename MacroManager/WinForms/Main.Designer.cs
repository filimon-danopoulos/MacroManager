namespace MacroManager
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
            this.applicationTabs = new System.Windows.Forms.TabControl();
            this.playbackTab = new System.Windows.Forms.TabPage();
            this.playbackButton = new System.Windows.Forms.Button();
            this.recordTab = new System.Windows.Forms.TabPage();
            this.stopButton = new System.Windows.Forms.Button();
            this.recordButton = new System.Windows.Forms.Button();
            this.macroList = new System.Windows.Forms.ListView();
            this.applicationTabs.SuspendLayout();
            this.playbackTab.SuspendLayout();
            this.recordTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // applicationTabs
            // 
            this.applicationTabs.Controls.Add(this.playbackTab);
            this.applicationTabs.Controls.Add(this.recordTab);
            this.applicationTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationTabs.Location = new System.Drawing.Point(0, 0);
            this.applicationTabs.Name = "applicationTabs";
            this.applicationTabs.SelectedIndex = 0;
            this.applicationTabs.Size = new System.Drawing.Size(629, 468);
            this.applicationTabs.TabIndex = 0;
            // 
            // playbackTab
            // 
            this.playbackTab.Controls.Add(this.macroList);
            this.playbackTab.Controls.Add(this.playbackButton);
            this.playbackTab.Location = new System.Drawing.Point(4, 22);
            this.playbackTab.Name = "playbackTab";
            this.playbackTab.Padding = new System.Windows.Forms.Padding(3);
            this.playbackTab.Size = new System.Drawing.Size(621, 442);
            this.playbackTab.TabIndex = 0;
            this.playbackTab.Text = "Playback Macro";
            this.playbackTab.UseVisualStyleBackColor = true;
            // 
            // playbackButton
            // 
            this.playbackButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playbackButton.Location = new System.Drawing.Point(258, 411);
            this.playbackButton.Name = "playbackButton";
            this.playbackButton.Size = new System.Drawing.Size(75, 23);
            this.playbackButton.TabIndex = 0;
            this.playbackButton.Text = "Playback First Macro";
            this.playbackButton.UseVisualStyleBackColor = true;
            this.playbackButton.Click += new System.EventHandler(this.playbackButton_Click);
            // 
            // recordTab
            // 
            this.recordTab.Controls.Add(this.stopButton);
            this.recordTab.Controls.Add(this.recordButton);
            this.recordTab.Location = new System.Drawing.Point(4, 22);
            this.recordTab.Name = "recordTab";
            this.recordTab.Padding = new System.Windows.Forms.Padding(3);
            this.recordTab.Size = new System.Drawing.Size(621, 442);
            this.recordTab.TabIndex = 1;
            this.recordTab.Text = "Record Macro";
            this.recordTab.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(315, 198);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(125, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop Recording";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(166, 198);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(110, 23);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Start Recording";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // macroList
            // 
            this.macroList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroList.CheckBoxes = true;
            this.macroList.Location = new System.Drawing.Point(6, 6);
            this.macroList.MultiSelect = false;
            this.macroList.Name = "macroList";
            this.macroList.Size = new System.Drawing.Size(609, 399);
            this.macroList.TabIndex = 1;
            this.macroList.UseCompatibleStateImageBehavior = false;
            this.macroList.View = System.Windows.Forms.View.List;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 468);
            this.Controls.Add(this.applicationTabs);
            this.Name = "Main";
            this.Text = "Macro Manager";
            this.applicationTabs.ResumeLayout(false);
            this.playbackTab.ResumeLayout(false);
            this.recordTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl applicationTabs;
        private System.Windows.Forms.TabPage playbackTab;
        private System.Windows.Forms.TabPage recordTab;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button playbackButton;
        private System.Windows.Forms.ListView macroList;
    }
}

