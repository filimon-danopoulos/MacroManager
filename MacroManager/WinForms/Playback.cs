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
    public partial class Playback : UserControl
    {
        #region Fields

        private IList<Macro> macros;
        private Macro playingMacro;

        #endregion

        #region Constructors

        public Playback()
        {
            InitializeComponent();
            this.macros = new List<Macro>();
            this.playingMacro = null;
        }

        #endregion

        #region Events

        #region Nested class PlaybackEventArgs

        public class PlaybackEventArgs : EventArgs
        {
            public PlaybackEventArgs(Macro macro)
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

        public event EventHandler<PlaybackEventArgs> StartPlayback;
        private void OnStartPlayback(PlaybackEventArgs args)
        {
            var handler = this.StartPlayback;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public event EventHandler<PlaybackEventArgs> StopPlayback;
        private void OnStopPlayback(PlaybackEventArgs args)
        {
            var handler = this.StopPlayback;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Event handlers

        private void startPlaybackButton_Click(object sender, EventArgs e)
        {
            this.removeButton.Enabled = false;
            this.stopPlaybackButton.Enabled = true;
            this.startPlaybackButton.Enabled = false;
            this.playingMacro = this.GetSelectedMacro();
            this.OnStartPlayback(new PlaybackEventArgs(this.playingMacro));
        }

        private void stopPlaybackButton_Click(object sender, EventArgs e)
        {
            if (this.playingMacro == null)
            {
                throw new NullReferenceException("playingMacro can't be null when the Stop button is clicked.");
            }
            this.OnStopPlayback(new PlaybackEventArgs(this.playingMacro));
        }

        private void macroListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayActions(this.GetSelectedMacro());
        }

        private void applicationTabs_Resize(object sender, EventArgs e)
        {
            this.ResizeActionColumns();
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Populates the controllers macro list.
        /// </summary>
        public void LoadMacros(IList<Macro> macros)
        {
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
            this.macroActionsList.Items.Clear();
            foreach (var action in macro.GetUserActions())
            {
                this.macroActionsList.Items.Add(new ListViewItem(new[] {
                    action.GetType().Name,
                    action.ToString()
                }));
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
            this.macroActionsList.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Return the macro that is selected in the macroListBox
        /// </summary>
        private Macro GetSelectedMacro()
        {
            return this.macros[this.macroListBox.SelectedIndex];
        }

        #endregion


    }
}
