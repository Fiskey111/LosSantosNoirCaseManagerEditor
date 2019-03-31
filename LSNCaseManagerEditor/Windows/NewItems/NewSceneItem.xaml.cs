using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for NewSceneItem.xaml
    /// </summary>
    public partial class NewSceneItem : Window
    {
        public CaseManager.CaseData.SceneItem Item = null;
        public int Index = -1;

        public NewSceneItem()
        {
            Init();
        }

        public NewSceneItem(CaseManager.CaseData.SceneItem data, int index)
        {
            Init();

            MainThread.AddLog($"NewSceneItem.NewSceneItem({data.Model} {data.Scenario} {data.ItemType} {data.Spawn.Position.X} | {index}");
            Index = index;

            Item = data;

            TypeBox.SelectedItem = data.ItemType;

            X.Visibility = Visibility.Visible;
            Y.Visibility = Visibility.Visible;
            Z.Visibility = Visibility.Visible;
            XLbl.Visibility = Visibility.Visible;
            YLbl.Visibility = Visibility.Visible;
            ZLbl.Visibility = Visibility.Visible;

            switch (Item.ItemType)
            {
                case CaseManager.CaseData.SceneItem.Type.Object:
                    ObjectSelected();
                    break;
                case CaseManager.CaseData.SceneItem.Type.Ped:
                    PedSelected();
                    break;
                case CaseManager.CaseData.SceneItem.Type.Vehicle:
                    VehicleSelected();
                    break;
            }

            SetPositions();

            Model.Text = Item.Model;

            switch (Item.ItemType)
            {
                case CaseManager.CaseData.SceneItem.Type.Ped:
                    if (Item.Animation != null)
                    {
                        AnimationDictionary.Text = Item.Animation.AnimationDictionary;
                        AnimationName.Text = Item.Animation.AnimationName;
                        AnimationFlag.SelectedItem = Item.Animation.AnimationFlag;
                    }
                    if (!string.IsNullOrWhiteSpace(Item.Scenario))
                    {
                        var vals = Enum.GetValues(typeof(Scenarios.ScenarioList)).Cast<Scenarios.ScenarioList>().ToList();
                        Scenario.SelectedIndex = vals.IndexOf((Scenarios.ScenarioList)Enum.Parse(typeof(Scenarios.ScenarioList), Item.Scenario));
                    }
                    break;
                case CaseManager.CaseData.SceneItem.Type.Vehicle:
                    IsSirenOn.SelectedIndex = Convert.ToInt32(Item.IsSirenOn);
                    IsSirenSilent.SelectedIndex = Convert.ToInt32(Item.IsSirenSilent);
                    break;
            }
        }

        private void SetPositions()
        {
            MainThread.AddLog($"NewSceneItem.SetPositions");
            X.Text = Item.Spawn.Position.X.ToString();
            Y.Text = Item.Spawn.Position.Y.ToString();
            Z.Text = Item.Spawn.Position.Z.ToString();

            if (!Item.Spawn.Rotation.IsNull())
            {
                Pitch.Text = Item.Spawn.Rotation.Pitch.ToString();
                Roll.Text = Item.Spawn.Rotation.Roll.ToString();
                Yaw.Text = Item.Spawn.Rotation.Yaw.ToString();
            }
            else
            {
                Heading.Text = Item.Spawn.Heading.ToString();
            }
        }

        public void Init()
        {
            MainThread.AddLog($"NewSceneItem.Init");
            InitializeComponent();

            AnimationFlag.ItemsSource = Enum.GetValues(typeof(CaseManager.Resources.Animations.AnimationFlags));
            TypeBox.ItemsSource = Enum.GetValues(typeof(CaseManager.CaseData.SceneItem.Type));
            Scenario.ItemsSource = Enum.GetValues(typeof(Scenarios.ScenarioList));
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != TypeBox) return;
            MainThread.AddLog($"NewSceneItem.Type_SelectionChanged={TypeBox.SelectedIndex}");
            if (Scenario == null) return;

            switch (TypeBox.SelectedIndex)
            {
                case 0: // Object
                    ObjectSelected();
                    break;
                case 1: // Ped
                    PedSelected();
                    break;
                case 2: // Vehicle
                    VehicleSelected();
                    break;
            }
        }

        private void ObjectSelected()
        {
            MainThread.AddLog($"NewSceneItem.ObjectSelected");
            Scenario.Visibility = Visibility.Hidden;
            ScenarioLbl.Visibility = Visibility.Hidden;
            AnimDictLbl.Visibility = Visibility.Hidden;
            AnimNameLbl.Visibility = Visibility.Hidden;
            AnimFlagLbl.Visibility = Visibility.Hidden;
            AnimationDictionary.Visibility = Visibility.Hidden;
            AnimationName.Visibility = Visibility.Hidden;
            AnimationFlag.Visibility = Visibility.Hidden;

            ActivateLbl.Visibility = Visibility.Hidden;
            SirenLbl.Visibility = Visibility.Hidden;
            IsSirenOn.Visibility = Visibility.Hidden;
            IsSirenSilent.Visibility = Visibility.Hidden;
        }

        private void PedSelected()
        {
            MainThread.AddLog($"NewSceneItem.PedSelected");
            Scenario.Visibility = Visibility.Visible;
            ScenarioLbl.Visibility = Visibility.Visible;

            AnimDictLbl.Visibility = Visibility.Visible;
            AnimNameLbl.Visibility = Visibility.Visible;
            AnimFlagLbl.Visibility = Visibility.Visible;
            AnimationDictionary.Visibility = Visibility.Visible;
            AnimationName.Visibility = Visibility.Visible;
            AnimationFlag.Visibility = Visibility.Visible;

            Grid.SetRow(AnimDictLbl, 3);
            Grid.SetRow(AnimNameLbl, 4);
            Grid.SetRow(AnimFlagLbl, 5);
            Grid.SetRow(AnimationDictionary, 3);
            Grid.SetRow(AnimationName, 4);
            Grid.SetRow(AnimationFlag, 5);

            ActivateLbl.Visibility = Visibility.Hidden;
            SirenLbl.Visibility = Visibility.Hidden;
            IsSirenOn.Visibility = Visibility.Hidden;
            IsSirenSilent.Visibility = Visibility.Hidden;
        }

        private void VehicleSelected()
        {
            MainThread.AddLog($"NewSceneItem.VehicleSelected");
            ActivateLbl.Visibility = Visibility.Visible;
            SirenLbl.Visibility = Visibility.Visible;
            IsSirenOn.Visibility = Visibility.Visible;
            IsSirenSilent.Visibility = Visibility.Visible;

            Grid.SetRow(ActivateLbl, 2);
            Grid.SetRow(SirenLbl, 3);
            Grid.SetRow(IsSirenOn, 2);
            Grid.SetRow(IsSirenSilent, 3);

            Scenario.Visibility = Visibility.Hidden;
            ScenarioLbl.Visibility = Visibility.Hidden;
            AnimDictLbl.Visibility = Visibility.Hidden;
            AnimNameLbl.Visibility = Visibility.Hidden;
            AnimFlagLbl.Visibility = Visibility.Hidden;
            AnimationDictionary.Visibility = Visibility.Hidden;
            AnimationName.Visibility = Visibility.Hidden;
            AnimationFlag.Visibility = Visibility.Hidden;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Important information:" +
                "\n\nA scene item is the data for a particular object to be spawned in by a scene." +
                "\nFor example: I want a police car to be spawned for my scene.  I will want to select \"Vehicle\" and set the position and any important options for it" +
                "\n\nThere are 3 item types: Object, Ped (person), and Vehicle" +
                "\nSelect a type and the proper boxes will appear." +
                "\nFill in the boxes with your data." +
                "\n\nNote: If you have GTAV loaded while doing this you are able to get the spawnpoint from the game!"
                );
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainThread.AddLog($"NewSceneItem.Window_Closing");
            var result = MessageBox.Show("Would you like to save your data?", "Save?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                MainThread.AddLog(TypeBox.Items.CurrentItem.ToString());
                Item = new CaseManager.CaseData.SceneItem();
                Item.ActivateWhenNear = Convert.ToBoolean(IsSirenOn.SelectedIndex);
                Item.Animation = new CaseManager.Resources.Animations(AnimationDictionary.Text, AnimationName.Text, 3f, (CaseManager.Resources.Animations.AnimationFlags) Enum.Parse(typeof(CaseManager.Resources.Animations.AnimationFlags), AnimationFlag.SelectedItem.ToString(), true));
                Item.IsSirenOn = Convert.ToBoolean(IsSirenSilent.SelectedIndex);
                Item.ItemType = (CaseManager.CaseData.SceneItem.Type) Enum.Parse(typeof(CaseManager.CaseData.SceneItem.Type), TypeBox.SelectedItem.ToString(), true);
                Item.Model = Model.Text;
                Item.Scenario = Scenario.SelectedItem != null ? Scenario.SelectedItem.ToString() : string.Empty;
                Item.Spawn = new CaseManager.Resources.SpawnPoint();
                Item.Spawn.Position = new CaseManager.Resources.Vector3()
                {
                    X = GetSingleFromText(X.Text),
                    Y = GetSingleFromText(Y.Text),
                    Z = GetSingleFromText(Z.Text)
                };
                if (string.IsNullOrWhiteSpace(Heading.Text))
                {
                    Item.Spawn.Rotation = new CaseManager.Resources.Rotator()
                    {
                        Pitch = GetSingleFromText(Pitch.Text),
                        Roll = GetSingleFromText(Roll.Text),
                        Yaw = GetSingleFromText(Yaw.Text)
                    };
                }
                else
                {
                    Item.Spawn.Heading = GetSingleFromText(Heading.Text);
                    Item.Spawn.Rotation = new CaseManager.Resources.Rotator()
                    {
                        Pitch = null,
                        Roll = null,
                        Yaw = null
                    };
                }

                MainThread.AddLog($"NewSceneItem.Saving data: {Item.Model}|{Item.ItemType}|{Item.Spawn.Position.X}|{Item.Spawn.Heading}|{Item.Spawn.Rotation == null}");
            }
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private float GetSingleFromText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0f;
            }
            else return Convert.ToSingle(text);
        }
        
        private void RetrieveSpawnpoint_Click(object sender, RoutedEventArgs e)
        {
            MainThread.AddLog($"NewSceneItem.RetrieveSpawnpoint_Click");
            // todo -- get position from RPH
        }
    }
}
