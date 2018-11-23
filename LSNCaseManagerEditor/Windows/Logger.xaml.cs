using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Logger.xaml
    /// </summary>
    public partial class Logger : Window
    {
        public Logger()
        {
            InitializeComponent();

            if (File.Exists(@"log.txt")) File.Delete(@"log.txt");
        }

        public void AddLog(string log)
        {
            string data = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString() + " : " + log;
            LogBox.ContentEnd.InsertLineBreak();
            LogBox.ContentEnd.InsertTextInRun(data);

            using (StreamWriter writer = new StreamWriter(@"log.txt"))
            {
                writer.Write(data);
            }

            ScrollView.ScrollToEnd();
        }
    }
}
