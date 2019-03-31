using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        /// <summary>
        /// Reset items when type is changed
        /// </summary>
        private void DataTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteItems();
            CheckItem();
        }

        /// <summary>
        /// Delete all the current controls that have been added to the master control list
        /// </summary>
        private void DeleteItems()
        {
            foreach (Control c in _controls)
            {
                CheckAndDelete(c);
            }
        }

        /// <summary>
        /// Delete and reset all columns
        /// </summary>
        private void CheckAndDelete(Control c)
        {
            if (c == null) return;
            if (Column1.Children.Contains(c)) Column1.Children.Remove(c);
            if (Column2.Children.Contains(c)) Column2.Children.Remove(c);
            if (MainGrid.Children.Contains(c)) MainGrid.Children.Remove(c);
        }
        
        /// <summary>
        /// Objects for Data Types
        /// </summary>
        
        // SceneData
        private CaseManager.CaseData.SceneData _sceneData;
        private NewSceneItem _sceneItem;

        // InterrogationData
        private NewInterrogationLines _newLines;
        private List<CaseManager.CaseData.InterrogationLine> _interrogationLines = new List<CaseManager.CaseData.InterrogationLine>();

        // StageData
        //private NewStageData _newStageData;
        private CaseManager.CaseData.StageData _stageData = new CaseManager.CaseData.StageData();
        private TextBox _stageNameBox, _stageDelayMin, _stageDelayMax, _stageBlipName,
            _stageBlipRadius, _stageAreaRadius, _stageBlipX, _stageBlipY, _stageBlipZ, _stageBlipColor, _stageBlipSprite,
            _stageNotDict, _stageNotName, _stageNotTitle, _stageNotSub, _stageNotText;


        /// <summary>
        /// Checking items and starting the proper methods
        /// </summary>
        private void CheckItem()
        {
            int selectedIndex = DataTypeBox.SelectedIndex;
            MainThread.AddLog($"DataTypeBox_SelectionChanged : {selectedIndex}");
            
            switch (selectedIndex)
            {
                case 0: // InterrogationData
                    InitializeItems(typeof(CaseManager.CaseData.InterrogationData), "Add Question", "Edit Question", "Remove Question", "Save to Memory");
                    break;
                case 1: // SceneData
                    InitializeItems(typeof(CaseManager.CaseData.SceneData), "Add Scene Item", "Edit Scene Item", "Remove Scene Item", "Save to Memory");
                    break;
                case 2: // StageData
                    InitializeItems(typeof(CaseManager.CaseData.StageData), "Add Data ID", "Edit Data ID", "Remove Data ID", "Save to Memory");
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

            if (IsEqualToCurrentCaseID(IDBox.Text))
            {
                IDBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Cannot re-use ID values. Please rename to a different ID value.", "ID Value in Use", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        #region ButtonClicks
        
        private void ItemButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender == _addButton)
            {
                OnAddButton(_currentType);
            }

            if (sender == _editButton && _iData.SelectedItem != null)
            {
                OnEditButton(_currentType);
            }

            if (sender == _removeButton && _iData != null && _iData.SelectedItem != null)
            {
                OnRemoveButton(_currentType);
            }

            if (sender == _saveButton)
            {
                OnSaveButton(_currentType);
            }
        }

        private void OnAddButton(Type type)
        {
            MainThread.AddLog($"Adding new item for {type}");

            if (type == typeof(CaseManager.CaseData.SceneData))
            {
                _sceneItem = new NewSceneItem
                {
                    Owner = this
                };

                if (_sceneItem.ShowDialog() == false)
                {
                    if (_sceneItem.Item == null)
                    {
                        MainThread.AddLog("Scene item not saved");
                        return;
                    }

                    _iData.Items.Add(_sceneItem.Item.Model);
                    _sceneData.Items.Add(_sceneItem.Item);
                }
            }

            if (type == typeof(CaseManager.CaseData.InterrogationData))
            {
                _newLines = new NewInterrogationLines
                {
                    Owner = this
                };

                if (_newLines.ShowDialog() == false)
                {
                    if (_newLines.LineValue == null)
                    {
                        MainThread.AddLog("Interrogation line not saved");
                        return;
                    }

                    _iData.Items.Add(_newLines.LineValue.Question.FirstOrDefault());
                    _interrogationLines.Add(_newLines.LineValue);
                }
            }

            if (type == typeof(CaseManager.CaseData.StageData))
            {
                _newLines = new NewInterrogationLines
                {
                    Owner = this
                };

                if (_newLines.ShowDialog() == false)
                {
                    if (_newLines.LineValue == null)
                    {
                        MainThread.AddLog("Interrogation line not saved");
                        return;
                    }

                    _iData.Items.Add(_newLines.LineValue.Question.FirstOrDefault());
                    _interrogationLines.Add(_newLines.LineValue);
                }
            }


            MainThread.AddLog($"Item added");
        }

        private void OnEditButton(Type type)
        {
            MainThread.AddLog($"Editing item {type}");

            if (type == typeof(CaseManager.CaseData.SceneData))
            {
                _sceneItem = new NewSceneItem(_sceneData.Items.ElementAt(_iData.SelectedIndex), _iData.SelectedIndex)
                {
                    Owner = this
                };

                if (_sceneItem.ShowDialog() == false)
                {
                    if (_sceneItem.Item == null)
                    {
                        MainThread.AddLog("Scene item not saved");
                        return;
                    }
                }

                _iData.Items.RemoveAt(_sceneItem.Index);
                _sceneData.Items.RemoveAt(_sceneItem.Index);

                _iData.Items.Insert(_sceneItem.Index, _sceneItem.Item.Model);
                _sceneData.Items.Insert(_sceneItem.Index, _sceneItem.Item);
            }

            if (type == typeof(CaseManager.CaseData.InterrogationData))
            {
                _newLines = new NewInterrogationLines(_interrogationLines.ElementAt(_iData.SelectedIndex), _iData.SelectedIndex)
                {
                    Owner = this
                };

                if (_newLines.ShowDialog() == true || _newLines.LineValue == null)
                {
                    MainThread.AddLog("Interrogation line not saved");
                    return;
                }

                _iData.Items.RemoveAt(_newLines.InsertIndex);
                _interrogationLines.RemoveAt(_newLines.InsertIndex);

                _iData.Items.Insert(_newLines.InsertIndex, _newLines.LineValue.Question.FirstOrDefault());
                _interrogationLines.Insert(_newLines.InsertIndex, _newLines.LineValue);
            }

            MainThread.AddLog("Item edited");
        }

        private void OnRemoveButton(Type type)
        {
            MainThread.AddLog($"Removing line at index {_iData.SelectedIndex} for type {type}");

            if (type == typeof(CaseManager.CaseData.SceneData))
            {
                _sceneData.Items.RemoveAt(_iData.SelectedIndex);
            }

            if (type == typeof(CaseManager.CaseData.InterrogationData))
            {
                _interrogationLines.RemoveAt(_iData.SelectedIndex);
            }

            _iData.Items.RemoveAt(_iData.SelectedIndex);

            MainThread.AddLog($"Item removed");
        }

        private void OnSaveButton(Type type)
        {
            MainThread.AddLog($"Saving data of type {type} to memory...");

            if (IsEqualToCurrentCaseID(IDBox.Text))
            {
                IDBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Cannot re-use ID values. Please rename to a different ID value.", "ID Value in Use", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (type == typeof(CaseManager.CaseData.SceneData))
            {
                CaseManager.CaseData.SceneData data = new CaseManager.CaseData.SceneData()
                {
                    ID = IDBox.Text,
                    DataType = "SceneData",
                    Items = _sceneData.Items,
                    Name = ""
                };
                MainThread.CurrentCase.SceneData.Add(IDBox.Text + ".data", data);
            }

            if (type == typeof(CaseManager.CaseData.InterrogationData))
            {
                CaseManager.CaseData.InterrogationData data = new CaseManager.CaseData.InterrogationData()
                {
                    ID = IDBox.Text,
                    DataType = "InterrogationData",
                    InterrogationLines = this._interrogationLines,
                    Name = ""
                };
                MainThread.CurrentCase.InterrogationData.Add(IDBox.Text + ".json", data);
            }

            this.DeleteItems();

            MainThread.AddLog($"Item saved");
        }

        #endregion

        private Type _currentType;
        private ListBox _iData;
        private Button _addButton, _removeButton, _editButton, _saveButton;

        private void InitializeItems(Type type, string addText, string editText, string removeText, string saveItem)
        {
            _currentType = type;

            _iData = new ListBox
            {
                MinHeight = this.ActualHeight - 75,
                ClipToBounds = true,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            Column2.Children.Add(_iData);
            
            _addButton = new Button
            {
                Content = addText,
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            _editButton = new Button
            {
                Content = editText,
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            _removeButton = new Button
            {
                Content = removeText,
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            _saveButton = new Button
            {
                Content = saveItem,
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            _addButton.Click += ItemButtonClick;
            _editButton.Click += ItemButtonClick;
            _removeButton.Click += ItemButtonClick;
            _saveButton.Click += ItemButtonClick;

            Column1.Children.Add(_addButton);
            Column1.Children.Add(_editButton);
            Column1.Children.Add(_removeButton);
            Column1.Children.Add(_saveButton);

            _controls.Add(_iData);
            _controls.Add(_addButton);
            _controls.Add(_editButton);
            _controls.Add(_removeButton);
            _controls.Add(_saveButton);



            if (type == typeof(CaseManager.CaseData.SceneData))
            {
                _sceneData = new CaseManager.CaseData.SceneData();
            }
            if (type == typeof(CaseManager.CaseData.InterrogationData))
            {
                // Nothing needed
            }
            if (type == typeof(CaseManager.CaseData.StageData))
            {
                StageDataCreateDataBoxes();
            }
        }

        private void StageDataCreateDataBoxes()
        {
            _stageNameBox = NewTextBox(1);
            NewLabel(1, "Name", "The name of your stage");
            _stageDelayMin = NewTextBox(2, false, true);
            NewLabel(2, "Delay Time Minimum", "(Integer) The minimum time before your stage can occur [after the last completed stage]");
            _stageDelayMax = NewTextBox(3, false, true);
            NewLabel(3, "Delay Time Maximum", "(Integer) The maximum time before your stage can occur [after the last completed stage]");

            _stageBlipName = NewTextBox(4);
            NewLabel(4, "Blip Name", "The name for the blip that will appear on the minimap [blip = callout circle]");
            _stageBlipRadius = NewTextBox(5, true);
            NewLabel(5, "Blip Radius", "(Float) The radius of the blip");
            _stageAreaRadius = NewTextBox(6, true);
            NewLabel(6, "Activation Radius", "(Float) The radius when the stage becomes active [read: the player arrives on scene]");

            _stageBlipSprite = NewTextBox(7, true);
            NewLabel(7, "Blip Sprite", "Can be blank if not needed. Check out a list here: https://wiki.gtanet.work/index.php?title=Blips&mobileaction=toggle_view_desktop");

            _stageBlipX = NewTextBox(8, true);
            NewLabel(8, "Blip X Position", "(Float) The X position of the blip");
            _stageBlipY = NewTextBox(9, true);
            NewLabel(9, "Blip Y Position", "(Float) The Y position of the blip");
            _stageBlipZ = NewTextBox(10, true);
            NewLabel(10, "Blip Z Position", "(Float) The Z position of the blip");

            _stageNotDict = NewTextBox(11);
            NewLabel(11, "Notification Texture Dictionary", "The texture dictionary for the picture [typical = 3dtextures OR char_911]; can be found in OpenIV");
            _stageNotName = NewTextBox(12);
            NewLabel(12, "Notification Texture Name", "The texture name for the picture [typical = mpgroundlogo_cops]; can be found in OpenIV");
            _stageNotTitle = NewTextBox(13);
            NewLabel(13, "Notification Title", "The large text for your notification [what appears above the minimap in the box]");
            _stageNotSub = NewTextBox(14);
            NewLabel(14, "Notification Subtitle", "The smaller subtitle for your notification [what appears above the minimap in the box]");
            _stageNotText = NewTextBox(15);
            NewLabel(15, "Notification Text", "The bulk text for your notification [what appears above the minimap in the box]");
        }

        private void NewLabel(int row, string content, string tooltip)
        {
            Label l = new Label()
            {
                Content = content,
                Margin = new Thickness(2, 2, 2, 2),
                HorizontalAlignment = HorizontalAlignment.Right,
                ToolTip = new ToolTip { Content = tooltip },
            };

            MainGrid.Children.Add(l);
            l.SetValue(Grid.ColumnProperty, 2);
            l.SetValue(Grid.RowProperty, row);
            _controls.Add(l);
        }

        private TextBox NewTextBox(int row, bool isFloat = false, bool isInt = false)
        {
            TextBox t = new TextBox()
            {
                Margin = new Thickness(2, 2, 2, 2),
                HorizontalAlignment = HorizontalAlignment.Stretch                
            };

            MainGrid.Children.Add(t);
            t.SetValue(Grid.ColumnProperty, 3);
            t.SetValue(Grid.RowProperty, row);
            _controls.Add(t);

            if (isFloat)
            {
                t.PreviewTextInput += FloatValidationTextBox;
            }
            if (isInt)
            {
                t.PreviewTextInput += IntValidationTextBox;
            }

            return t;
        }

        private bool IsEqualToCurrentCaseID(string value) => MainThread.CurrentCase.GetAllIDs().Any(c => c == value);

        // https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private readonly Regex _floats = new Regex("[0-9.]+");
        private readonly Regex _ints = new Regex("[0-9]+");

        private void FloatValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_floats.IsMatch(e.Text);
        }
        private void IntValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_ints.IsMatch(e.Text);
        }
    }
}
