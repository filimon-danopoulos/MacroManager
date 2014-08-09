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
using MacroManager.Data.Actions;
using MacroManager.Playback;

namespace MacroManager.WinForms
{
    public partial class Playback : UserControl
    {
        #region Fields

        private IList<Macro> macros;
        private bool hideShortWaitActions;
        private PlaybackService playbackService;
        private ImageList actionIcons;
        private int currectActionIndex;

        #endregion

        #region Constructors

        public Playback()
        {
            InitializeComponent();
            this.playbackService = new PlaybackService();
            this.playbackService.ActionStarted += playbackService_ActionStarted;
            this.playbackService.ActionCompleted += playbackService_ActionCompleted;
            this.playbackService.MacroCompleted += playbackService_MacroCompleted;
            this.playbackService.MacroCanceled += playbackService_MacroCanceled;
            this.macros = new List<Macro>();
            this.hideShortWaitActions = this.hideShortWaitActionCheckBox.Checked;
        }

        #endregion

        #region Events

        #region Nested class RemoveMacroEventArgs

        public class RemoveMacroEventArgs : EventArgs
        {
            public RemoveMacroEventArgs(Macro macro)
            {
                this.SelectedMacro = macro;
            }
            public Macro SelectedMacro
            {
                get;
                private set;
            }
        }

        #endregion

        /// <summary>
        /// Fires when the start playback button is clicked.
        /// </summary>
        public event EventHandler StartPlayback;

        /// <summary>
        /// Wrapper for the StartPlayback event, null checks handler.
        /// </summary>
        private void OnStartPlayback()
        {
            var handler = this.StartPlayback;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Fires when the stop playback button is clicked.
        /// </summary>
        public event EventHandler StopPlayback;

        /// <summary>
        /// Wrapper for the StopPlayback event, null checks the handler.
        /// </summary>
        private void OnStopPlayback()
        {
            var handler = this.StopPlayback;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Fires when the remove macro button is clicked.
        /// </summary>
        public event EventHandler<RemoveMacroEventArgs> RemoveMacro;

        /// <summary>
        /// Wrapper for the RemoveMacro event, nulls checks the handler.
        /// </summary>
        private void OnRemoveMacro(RemoveMacroEventArgs args)
        {
            var handler = this.RemoveMacro;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Event handlers

        private void playbackService_ActionStarted(object sender, EventArgs e)
        {
            if (this.currectActionIndex < this.macroActionsList.Items.Count)
            {
                this.macroActionsList.Items[this.currectActionIndex].ImageKey = "executing";
            }
        }

        private void playbackService_ActionCompleted(object sender, EventArgs e)
        {
            if (this.currectActionIndex < this.macroActionsList.Items.Count)
            {
                this.macroActionsList.Items[this.currectActionIndex++].ImageKey = "done";
            }
        }

        private void playbackService_MacroCanceled(object sender, EventArgs e)
        {
            this.currectActionIndex = 0;
            this.stopPlaybackButton.Enabled = false;
            this.startPlaybackButton.Enabled = true;
        }

        private async void playbackService_MacroCompleted(object sender, EventArgs e)
        {
            this.currectActionIndex = 0;
            this.stopPlaybackButton.Enabled = false;
            await Task.Delay(5000);
            foreach (ListViewItem action in this.macroActionsList.Items)
            {
                action.ImageKey = "waiting";
            }
            this.startPlaybackButton.Enabled = true;
        }

        private async void startPlaybackButton_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedMacro())
            {
                return;
            }
            this.currectActionIndex = 0;
            this.removeButton.Enabled = false;
            this.stopPlaybackButton.Enabled = true;
            this.startPlaybackButton.Enabled = false;
            this.OnStartPlayback();
            await this.playbackService.StartMacroPlaybackAsync(this.GetSelectedMacro());
        }

        private void stopPlaybackButton_Click(object sender, EventArgs e)
        {
            this.playbackService.StopMacroPlayback();
            this.OnStopPlayback();
        }

        private void macroListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.HasSelectedMacro())
            {
                this.DisplayActions(this.GetSelectedMacro());
            }
        }

        private void applicationTabs_Resize(object sender, EventArgs e)
        {
            this.ResizeActionColumns();
        }

        private void hideShortWaitActionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.hideShortWaitActions = this.hideShortWaitActionCheckBox.Checked;

            if (this.HasSelectedMacro())
            {
                // Reload actions so that any visible wait actions are hiden. 
                this.DisplayActions(this.GetSelectedMacro());
                // Resize the columns so that any potential scroll bar width is taken into account.
                this.ResizeActionColumns();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (this.HasSelectedMacro())
            {
                this.OnRemoveMacro(new RemoveMacroEventArgs(this.GetSelectedMacro()));
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Populates the controllers macro list.
        /// </summary>
        public void LoadMacros(IList<Macro> macros)
        {
            this.ResetForm();
            this.macros = macros;
            this.DisplayMacros();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads all the macros from the service into the macroListBox on the playback Tab.
        /// </summary>
        private void DisplayMacros()
        {
            this.startPlaybackButton.Enabled = this.macros.Count() > 0;
            this.macroListBox.DataSource = this.macros.Select(x => x.Name).ToList();
        }

        /// <summary>
        /// Displays all the actions related to the suplied action
        /// </summary>
        private void DisplayActions(Macro macro)
        {
            this.InitializeActionIcons();
            this.macroActionsList.Items.Clear();
            var actions = macro.GetUserActions();
            if (this.hideShortWaitActions)
            {
                actions = actions.Where(x =>
                {
                    var waitAction = x as WaitAction;
                    return waitAction == null || waitAction.Duration > 500;
                });
            }
            foreach (var action in actions)
            {
                this.macroActionsList.Items.Add(new ListViewItem(new[] {
                    action.GetType().Name,
                    action.Process,
                    action.ToString()
                },
                "waiting"));
            }
            if (macro.Description == "")
            {
                this.macroDetailsDescription.Text = "No description available...";
                this.macroDetailsDescription.Font = new Font(this.macroDetailsDescription.Font, FontStyle.Italic);
            }
            else
            {
                this.macroDetailsDescription.Text = macro.Description;
                this.macroDetailsDescription.Font = new Font(this.macroDetailsDescription.Font, FontStyle.Regular);
            }
            this.startPlaybackButton.Enabled = true;
            this.removeButton.Enabled = true;

        }

        /// <summary>
        /// Resizes the columns in the macroActionList.
        /// </summary>
        private void ResizeActionColumns()
        {
            this.macroActionsList.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Return the macro that is selected in the macroListBox
        /// </summary>
        private Macro GetSelectedMacro()
        {
            return this.macros[this.macroListBox.SelectedIndex];
        }

        /// <summary>
        /// Checks that a macro is selected in the macro list box.
        /// </summary>
        private bool HasSelectedMacro()
        {
            return this.macroListBox.SelectedIndex != -1;
        }

        /// <summary>
        /// Resets all the controls in the form to their original state.
        /// </summary>
        private void ResetForm()
        {
            this.macroListBox.DataSource = null;
            this.macroActionsList.Items.Clear();
            this.macroDetailsDescription.Text = "";
            this.stopPlaybackButton.Enabled = false;
            this.startPlaybackButton.Enabled = false;
            this.removeButton.Enabled = false;
        }

        private void InitializeActionIcons()
        {
            if (this.actionIcons == null)
            {
                this.actionIcons = new ImageList();
                this.actionIcons.Images.Add("waiting", Image.FromFile("Icons\\clock.png"));
                this.actionIcons.Images.Add("executing", Image.FromFile("Icons\\cog.png"));
                this.actionIcons.Images.Add("done", Image.FromFile("Icons\\accept.png"));
                this.actionIcons.ImageSize = new Size(16, 16);
                this.macroActionsList.SmallImageList = this.actionIcons;
            }
        }
        #endregion

    }
}
