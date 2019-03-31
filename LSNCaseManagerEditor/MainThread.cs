using LSNCaseManagerEditor.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

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

        private static float totalBytesOfMemoryUsed = 0;
        private static void Process()
        {
            while (true)
            {
                totalBytesOfMemoryUsed = (float) System.Diagnostics.Process.GetCurrentProcess().WorkingSet64;

                Thread.Yield();
            }
        }

        internal static string GetTotalMemoryString()
        {
            return $"{Math.Round(totalBytesOfMemoryUsed / (1024 * 1024), 1)} MB / {Math.Round(GetTotalMemoryInBytes() / (1024 * 1024 * 1024), 1)} GB RAM used";
        }

        private static float GetTotalMemoryInBytes() => (float) new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
    }
}
