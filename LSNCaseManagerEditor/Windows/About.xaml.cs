using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace LSNCaseManagerEditor.Windows
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        private Timer _t;
        private int counter = 12;

        public About()
        {
            InitializeComponent();
                       
            if (counter >= 8) CloseLbl.Foreground = Brushes.Green;
            CloseLbl.Content = counter.ToString() + " seconds";

            StartTimer();
        }

        private void StartTimer()
        {
            _t = new Timer();
            _t.Interval = 1000;
            _t.Tick += timer_Tick;
            _t.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            counter--;

            SolidColorBrush b = Brushes.Green;

            if (counter < 7 && counter >= 4) b = Brushes.Yellow;
            else if (counter < 4) b = Brushes.Red;

            CloseLbl.Foreground = b;

            string label = counter == 1 ? " second" : " seconds";
            CloseLbl.Content = counter.ToString() + label;

            if (counter == 0)
            {
                _t.Stop();
                this.Close();
            }
        }
    }
}
