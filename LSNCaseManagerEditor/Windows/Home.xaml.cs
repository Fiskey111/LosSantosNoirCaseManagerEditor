using CaseManager.CaseData;
using LSNCaseManagerEditor.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Label = System.Windows.Controls.Label;

namespace LSNCaseManagerEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private About _about;
        private NewItem _newItem;

        public MainWindow()
        {
            InitializeComponent();
            
            MainThread.Start();
            
            AddLog("Starting main thread");

            Thread thread = new Thread(TestMethod);
            thread.IsBackground = true;
            thread.Start();
        }

        private void TestMethod()
        {
            int fileCount = 0;
            while (true)
            {
                string mem = MainThread.GetTotalMemoryString();
                Dispatcher.Invoke(new Action<string>(value => MemUsed.Content = value), mem);

                if (MainThread.CurrentCase != null && fileCount != MainThread.CurrentCase.TotalCurrentFiles())
                {
                    Dispatcher.Invoke(new Action<string>(value => LastSavedLbl.Content = value), "Not Saved");
                    Dispatcher.Invoke(new Action<Brush>(value => LastSavedLbl.Foreground = value), Brushes.Red);
                    fileCount = MainThread.CurrentCase.TotalCurrentFiles();
                }

                Thread.Sleep(0500);
            }
        }

        public void AddLog(string text)
        {
            MainThread.AddLog(text);
        }

        private void OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender == ToolBarOpenCase)
            {
                AddLog("Opening case");
                OpenCase();
            }

            if (sender == ToolBarNewCase)
            {
                AddLog("Opening case");
                NewCase();
            }

            if (sender == ToolBarSaveCase)
            {
                AddLog("Saving case");
                if (MainThread.CurrentCase == null)
                {
                    MessageBox.Show("You MUST load or create a new case first before saving it!", "No Case Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SaveCase();
            }
            
            if (sender == ToolBarHelp)
            {
                AddLog("Opening about");
                _about = new About();
                _about.ShowDialog();
                RefreshData();
            }

            if (sender == ToolBarAddItem)
            {
                AddLog("Adding item");
                if (MainThread.CurrentCase == null)
                {
                    MessageBox.Show("You MUST load or create a new case first before adding a new item!", "No Case Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _newItem = new NewItem();
                _newItem.ShowDialog();
                RefreshData();
            }
        }

        private void OpenCase()
        {
            MessageBox.Show("Please select the ROOT folder for your case"
                + Environment.NewLine + "Typically this is the name of the case"
                + Environment.NewLine + Environment.NewLine
                + "Example: Grand Theft Auto V > Plugins > LSPDFR > LSNoir > Cases > Downtown Delivery   <-- You will select \"Downtown Delivery\", not any subfolders"
                , "Important Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult folder = folderBrowserDialog.ShowDialog();

            if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                AddLog("No folder selected");
                MessageBox.Show("You must select a folder");
                return;
            }

            MainThread.RootPath = folderBrowserDialog.SelectedPath;
            CurrentCaseLbl.Content = Path.GetFileName(MainThread.RootPath);

            AddLog($"Loading case from: {MainThread.RootPath}");
            MainThread.CurrentCase = new CaseManager.CaseLoader.Case(MainThread.RootPath);
            AddLog($"Case loaded with {MainThread.CurrentCase.FileCount}");

            LastSavedLbl.Content = "Not Saved";
            LastSavedLbl.Foreground = Brushes.Red;

            MainWindowDisplay.Items.Clear();
            RefreshData();
        }

        private void NewCase()
        {
            AddLog("Requesting case name");
            MessageBox.Show("Please select the ROOT folder for your case"
                + Environment.NewLine + "If you don't have a folder created, use the \"Make new folder\" button to create it"
                + Environment.NewLine + "Name this folder the name of your case"
                + Environment.NewLine + Environment.NewLine
                + "Example: Grand Theft Auto V > Plugins > LSPDFR > LSNoir > Cases   <-- You could create a new folder \"Downtown Delivery\" in the \"Cases\" folder and select the \"Downtown Delivery\" folder"
                , "Important Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult folder = folderBrowserDialog.ShowDialog();

            if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                AddLog("No folder selected");
                MessageBox.Show("You must select a folder", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.*", SearchOption.AllDirectories).Length > 0 || Directory.GetDirectories(folderBrowserDialog.SelectedPath).Length > 0)
            {
                AddLog("Not an empty folder");
                MessageBox.Show("The folder you select *MUST* be an empty folder", "Contents Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddLog($"Path: {folderBrowserDialog.SelectedPath}");

            MainThread.RootPath = folderBrowserDialog.SelectedPath;
            CurrentCaseLbl.Content = Path.GetFileName(MainThread.RootPath);

            LastSavedLbl.Content = "Not Saved";
            LastSavedLbl.Foreground = Brushes.Red;

            MainThread.CurrentCase = new CaseManager.CaseLoader.Case();
        }

        private void SaveCase()
        {
            if (MainThread.CurrentCase == null)
            {
                AddLog("No case loaded");
                MessageBox.Show("No case loaded to save...", "UNABLE TO SAVE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MainThread.CurrentCase.SaveData(MainThread.RootPath);

            LastSavedLbl.Content = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString();
            LastSavedLbl.Foreground = Brushes.Green;
        }

        private TreeViewItem AddItem(string name, bool isFolder)
        {
            TreeViewItem item = new TreeViewItem();

            StackPanel p = new StackPanel();
            p.Orientation = System.Windows.Controls.Orientation.Horizontal;
            
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,/gfx/main/item_doc.png"));

            Label l = new Label();
            l.Content = name;

            p.Children.Add(image);
            p.Children.Add(l);

            item.Header = p;
            return item;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainThread.Logger.Close();
            if (_about != null && _about.IsVisible) _about.Close();
        }

        private void RefreshData()
        {
            MainThread.AddLog("Refreshing data");

            this.MainWindowDisplay.Items.Clear();
            List<CaseManager.CaseData.IData> _dataList = new List<CaseManager.CaseData.IData>();

            _dataList.AddRange(MainThread.CurrentCase.CSIData.Values);
            _dataList.AddRange(MainThread.CurrentCase.EntityData.Values);
            _dataList.AddRange(MainThread.CurrentCase.EvidenceData.Values);
            _dataList.AddRange(MainThread.CurrentCase.InterrogationData.Values);
            _dataList.AddRange(MainThread.CurrentCase.SceneData.Values);
            _dataList.AddRange(MainThread.CurrentCase.StageData.Values);
            _dataList.AddRange(MainThread.CurrentCase.WrittenData.Values);

            AddItemsToData(_dataList.ToArray());
            MainThread.AddLog($"Total items: {MainWindowDisplay.Items.Count}");
        }

        private void AddItemsToData(IData[] data)
        {
            foreach (IData d in data)
            {
                this.MainWindowDisplay.Items.Add(d);
            }
        }
    }
}