using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LSNCaseManagerEditor.Windows
{
    /// <summary>
    /// Interaction logic for NewItem.xaml
    /// </summary>
    public partial class NewItem : Window
    {
        public NewItem()
        {
            InitializeComponent();
        }

        private List<Control> _controls = new List<Control>();
        
        private void DataTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteItems();
            CheckItem();
        }

        private void DeleteItems()
        {
            foreach (Control c in _controls)
            {
                CheckAndDelete(c);
            }
        }

        private void CheckAndDelete(Control c)
        {
            if (c == null) return;
            if (Column1.Children.Contains(c)) Column1.Children.Remove(c);
            if (Column2.Children.Contains(c)) Column2.Children.Remove(c);
            if (Column3.Children.Contains(c)) Column3.Children.Remove(c);
            if (Column4.Children.Contains(c)) Column4.Children.Remove(c);
        }

        private void CheckItem()
        {
            int selectedIndex = DataTypeBox.SelectedIndex;
            MainThread.AddLog($"DataTypeBox_SelectionChanged : {selectedIndex}");

            /*                
                <ComboBoxItem Content="InterrogationData"/>
                <ComboBoxItem Content="SceneData"/>
                <ComboBoxItem Content="StageData"/>
                <ComboBoxItem Content="WrittenData"/>
                <ComboBoxItem Content="EntityData"/>
                <ComboBoxItem Content="StageData"/>
            */

            switch (selectedIndex)
            {
                case 0: // InterrogationData
                    IntLines();
                    break;
                case 1: // SceneData

                    break;
                case 2: // StageData

                    break;
                case 3: // WrittenData

                    break;
                case 4: // EntityData

                    break;
                case 5: // StageData

                    break;
            }
        }

        private void IDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IDBox.FontStyle == FontStyles.Italic) IDBox.FontStyle = FontStyles.Normal;
            if (IDBox.FontWeight == FontWeights.DemiBold) IDBox.FontWeight = FontWeights.Normal;
        }

        #region SceneData

        private void SceneData()
        {
            MainThread.AddLog("Scene data selected");
            /* Need:
                List<SceneItem> Items
            */


        }

        #endregion

        #region IntData

        private ListBox _iData;
        private Button _addButton, _removeButton, _editButton, _saveButton;
        private NewInterrogationLines _newLines;
        private List<CaseManager.CaseData.InterrogationLine> _interrogationLines = new List<CaseManager.CaseData.InterrogationLine>();
        
        private void IntLines()
        {
            MainThread.AddLog("Interrogation data selected");
            /* Need:
                Interrogation Line List
            */
            _iData = new ListBox();
            _iData.MinHeight = 50;
            _iData.ClipToBounds = true;
            Column2.Children.Add(_iData);

            _addButton = new Button
            {
                Content = "Add Question",
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 110
            };
            _addButton.Click += ButtonClick;
            _editButton = new Button
            {
                Content = "Edit Question",
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 110
            };
            _editButton.Click += ButtonClick;
            _removeButton = new Button
            {
                Content = "Remove Question",
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 110
            };
            _removeButton.Click += ButtonClick;
            _saveButton = new Button
            {
                Content = "Save Interrogation to Memory",
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 200
            };
            _saveButton.Click += ButtonClick;

            Column1.Children.Add(_addButton);
            Column1.Children.Add(_editButton);
            Column1.Children.Add(_removeButton);

            Column4.Children.Add(_saveButton);

            _controls.Add(_iData);
            _controls.Add(_addButton);
            _controls.Add(_editButton);
            _controls.Add(_removeButton);
            _controls.Add(_saveButton);
        }

        #endregion

        private void ButtonClick (object sender, RoutedEventArgs e)
        {
            if (sender == _addButton)
            {
                MainThread.AddLog("Adding new interrogation line");
                _newLines = new NewInterrogationLines();
                _newLines.Owner = this;
                
                if (_newLines.ShowDialog() == false)
                {
                    if (_newLines.LineValue == null)
                    {
                        MainThread.AddLog("Interrogation line not saved");
                        return;
                    }
                    _iData.Items.Add(_newLines.LineValue.Question.FirstOrDefault());
                    _interrogationLines.Add(_newLines.LineValue);
                    MainThread.AddLog("Interrogation line saved");
                }
            }

            if (sender == _editButton)
            {
                if (_iData.SelectedItem == null) return;

                MainThread.AddLog("Editing interrogation line");
                _newLines = new NewInterrogationLines(_interrogationLines.ElementAt(_iData.SelectedIndex), _iData.SelectedIndex);
                _newLines.Owner = this;

                if (_newLines.ShowDialog() == true) return;

                if (_newLines.LineValue == null)
                {
                    MainThread.AddLog("Interrogation line not saved");
                    return;
                }

                _iData.Items.RemoveAt(_newLines.InsertIndex);
                _interrogationLines.RemoveAt(_newLines.InsertIndex);

                _iData.Items.Insert(_newLines.InsertIndex, _newLines.LineValue.Question.FirstOrDefault());
                _interrogationLines.Insert(_newLines.InsertIndex, _newLines.LineValue);
                MainThread.AddLog("Interrogation line saved");
            }

            if (sender == _removeButton)
            {
                if (_iData.SelectedItem == null) return;

                MainThread.AddLog($"Removing line at index {_iData.SelectedIndex}");
                _interrogationLines.RemoveAt(_iData.SelectedIndex);
                _iData.Items.RemoveAt(_iData.SelectedIndex);
            }

            if (sender == _saveButton)
            {
                MainThread.AddLog("Saving all data to memory...");

                CaseManager.CaseData.InterrogationData data = new CaseManager.CaseData.InterrogationData()
                {
                    ID = IDBox.Text,
                    DataType = "InterrogationData",
                    InterrogationLines = this._interrogationLines,
                    Name = ""
                };

                MainThread.CurrentCase.InterrogationData.Add(IDBox.Text + ".json", data);
                this.DeleteItems();
            }
        }

        private void SetVisiblity(Control control, Visibility visible = Visibility.Visible)
        {
            control.Visibility = visible;
        }
    }
}
