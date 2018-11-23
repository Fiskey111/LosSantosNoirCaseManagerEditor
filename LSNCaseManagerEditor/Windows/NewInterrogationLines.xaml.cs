using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CaseManager.CaseData;

namespace LSNCaseManagerEditor.Windows
{
    /// <summary>
    /// Interaction logic for NewInterrogationLines.xaml
    /// </summary>
    public partial class NewInterrogationLines : Window
    {
        public InterrogationLine LineValue;
        public int InsertIndex = 0;

        public NewInterrogationLines()
        {
            InitializeComponent();
        }

        public NewInterrogationLines(InterrogationLine data, int index)
        {
            InitializeComponent();

            AddData(data.Question, Question);
            AddData(data.Answer, Answer);
            CorrectAnswer.SelectedIndex = (int) data.CorrectAnswer;
            AddData(data.PlayerResponseTruth, PlayerTruth);
            AddData(data.InterrogeeReactionTruth, InterrogeeTruth);
            AddData(data.PlayerResponseDoubt, PlayerDoubt);
            AddData(data.InterrogeeReactionDoubt, InterrogeeDoubt);
            AddData(data.PlayerResponseLie, PlayerLie);
            AddData(data.InterrogeeReactionLie, InterrogeeLie);

            InsertIndex = index;
        }

        private void AddData(string[] data, TextBox box)
        {
            foreach (string line in data)
            {
                box.AppendText(line);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Question.Text)) return;
            if (string.IsNullOrWhiteSpace(Answer.Text)) return;
            if (string.IsNullOrWhiteSpace(PlayerTruth.Text)) return;
            if (string.IsNullOrWhiteSpace(InterrogeeTruth.Text)) return;
            if (string.IsNullOrWhiteSpace(PlayerDoubt.Text)) return;
            if (string.IsNullOrWhiteSpace(InterrogeeDoubt.Text)) return;
            if (string.IsNullOrWhiteSpace(PlayerLie.Text)) return;
            if (string.IsNullOrWhiteSpace(InterrogeeLie.Text)) return;

            LineValue = new InterrogationLine
            {
                Question = ReadLines(Question.Text).ToArray(),
                Answer = ReadLines(Answer.Text).ToArray(),
                CorrectAnswer = (InterrogationLine.ResponseType)Enum.Parse(typeof(InterrogationLine.ResponseType), CorrectAnswer.Text),
                PlayerResponseTruth = ReadLines(PlayerTruth.Text).ToArray(),
                InterrogeeReactionTruth = ReadLines(InterrogeeTruth.Text).ToArray(),
                PlayerResponseDoubt = ReadLines(PlayerDoubt.Text).ToArray(),
                InterrogeeReactionDoubt = ReadLines(InterrogeeDoubt.Text).ToArray(),
                PlayerResponseLie = ReadLines(PlayerLie.Text).ToArray(),
                InterrogeeReactionLie = ReadLines(InterrogeeLie.Text).ToArray()
            };
        }

        private IEnumerable<string> ReadLines(string s)
        {
            string line;
            using (var sr = new StringReader(s))
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) break;
                    yield return line;
                }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Important information:" +
                "\n\nWhen entering lines, be aware that you can only fit so many characters on the screen." +
                " L.S. Noir has a dialogue class that requires a keypress for the player to continue to the next line." +
                "\n\nWhen entering data in a text box here press \"Enter\" to create a new line." +
                " This new line will be the subsequent entry in the dialogue class." +
                "\n\nIn other words, every separate line you have in the texbox will be the subtitle that shows on the screen" +
                "\n\nNOTE: The data will automatically save when you exit. " +
                " If you leave any box blank the data will not save." +
                "\n\nThese questions will display in the order you place them." +
                " There is no sorting mechanism in this program, so it is best to enter your questions in the order you would like them displayed." +
                "\n\n\nYou can find more information in the youtube video for case creation." +
                " Check the description for timestamps.", 
                "Some Important Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
