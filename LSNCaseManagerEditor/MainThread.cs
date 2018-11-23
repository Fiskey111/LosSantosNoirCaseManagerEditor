using LSNCaseManagerEditor.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSNCaseManagerEditor
{
    internal class MainThread
    {
        internal static string RootPath = string.Empty;
        internal static DateTime SavedTime = DateTime.MinValue;
        internal static CaseManager.CaseLoader.Case CurrentCase = null;
        internal static List<string> Files = null;

        internal static Logger Logger;

        private static Thread _t;

        internal static void Start()
        {
            Logger = new Logger();
            Logger.Show();

            _t = new Thread(Process);
            _t.Start();
        }

        public static void AddLog(string text)
        {
            Logger.AddLog(text);
        }

        public static void End()
        {
            Logger.Close();
            _t.Abort();
        }

        private static void Process()
        {
            while (true)
            {
                Thread.Yield();
            }
        }
    }
}
