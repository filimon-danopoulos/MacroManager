using MacroManager.Core.Data.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroManager.WinForms
{
    public partial class EditAction : Form
    {
        #region Fields

        private IDictionary<PropertyInfo, object> newValues;

        #endregion

        #region Constructors

        [Obsolete("Default constructor is only needed for designer puposes.", true)]
        public EditAction()
        {
            InitializeComponent();
        }

        public EditAction(UserAction action)
        {
            InitializeComponent();

            this.Action = action;
            this.BuildEditForm();
            this.newValues = new Dictionary<PropertyInfo, object>();
        }

        #endregion

        #region Properties

        public UserAction Action
        {
            get;
            private set;
        }

        #endregion

        #region Event Handlers

        private void saveButton_Click(object sender, EventArgs e)
        {
            foreach (var record in this.newValues)
            {
                record.Key.SetValue(this.Action, record.Value);
            }
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region Private Methods

        private void BuildEditForm()
        {
            var type = this.Action.GetType();
            this.typeTextBox.Text = type.Name;
            var counter = 1;
            foreach(var prop in type.GetProperties()) {
                if (prop.CustomAttributes.Any(x => x.AttributeType == typeof(UserAction.NonEditableAttribute))) {
                    continue;
                }

                var label = new Label();
                label.Size = this.typeLabel.Size;
                label.Location = new Point(this.typeLabel.Location.X,  this.typeLabel.Location.Y + 26 * counter);
                label.Text = prop.Name;

                Control inputField;
                if (prop.PropertyType == typeof(int))
                {
                    var numericInput = new NumericUpDown();
                    numericInput.Minimum = 0;
                    numericInput.Maximum = Int32.MaxValue; // TODO make this something smarter.
                    numericInput.Value = (int)prop.GetValue(this.Action);
                    numericInput.ValueChanged += (sender, args) =>
                    {
                        this.newValues[prop] = (int)numericInput.Value;
                    };
                    inputField = numericInput;
                }
                else if (prop.PropertyType == typeof(MouseAction.MouseButton))
                {
                    var comboBox = new ComboBox();
                    comboBox.BindingContext = new BindingContext();
                    comboBox.DataSource = Enum.GetValues(typeof(MouseAction.MouseButton));
                    comboBox.Refresh();
                    var value = Convert.ChangeType(prop.GetValue(this.Action), typeof(MouseAction.MouseButton));
                    comboBox.SelectedItem = value;
                    comboBox.SelectedIndexChanged += (sender, args) =>
                    {
                        this.newValues[prop] = Enum.Parse(typeof(MouseAction.MouseButton), comboBox.SelectedValue.ToString());
                    };
                    inputField = comboBox;
                }
                else if (prop.PropertyType == typeof(String))
                {
                    var value = Convert.ChangeType(prop.GetValue(this.Action), prop.PropertyType).ToString();
                    var textField = new TextBox();
                    textField.Text = value;
                    textField.TextChanged += (sender, args) =>
                    {
                        this.newValues[prop] = textField.Text;
                    };
                    inputField = textField;
                }
                else
                {
                    // Ignore fields we haven't implemented yet.
                    Debug.WriteLine(String.Format("Property of type {0} ignored. Property name: {1}", prop.PropertyType.Name, prop.Name));
                    continue;
                }

                
                inputField.Size = this.typeTextBox.Size;
                inputField.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                inputField.Location = new Point(this.typeTextBox.Location.X, this.typeTextBox.Location.Y + 26 * counter);

                this.actionPropertyPanel.Controls.Add(label);
                this.actionPropertyPanel.Controls.Add(inputField);

                ++counter;
            }
        }

        #endregion

    }
}