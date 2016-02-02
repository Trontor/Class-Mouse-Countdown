using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Class_Countdown
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            System.Threading.ManualResetEvent close = new System.Threading.ManualResetEvent(false);
            SystemEvents.SessionEnding += (object sender, SessionEndingEventArgs e) =>
                close.Set();
            BaseClass();
            close.WaitOne();
        }

        static TimeSpan timespan = new TimeSpan();
        static Timer t = new Timer();
        static List<DateTime> times = new List<DateTime>();
        static bool FoundClass = false;

        private static void BaseClass()
        {
            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 20, 0);
            for (int i = 0; i < 11; i++)
            {
                if (i == 3)
                    time = time.AddMinutes(20);
                else
                    time = time.AddMinutes(40);
                times.Add(time);
            }


            t.Elapsed += t_Elapsed;
            t.Interval = 10;
            t.Start();
        }

        static Color txtColour()
        {
            if (timespan.TotalSeconds > 1600)
                return Color.FromArgb(0, (int)((255 - (2400 - timespan.TotalSeconds) / 5.49019608)), 0);
            else if (timespan.TotalSeconds > 800)
                return Color.FromArgb(255, (int)(255 - (1600 - timespan.TotalSeconds) / 5.49019608), 0);
            else
                return Color.FromArgb((int)(255 - ((800 - timespan.TotalSeconds) / 5.49019608)), 0, 0);
        }

        static double NthRoot(double A, double N)
        {
            return Math.Pow(A, 1.0 / N);
        }

        static FloatingOSDWindow window = new FloatingOSDWindow();
        static void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime nxtPer = getNextPeriod();
            if (!FoundClass)
                return;

            timespan = (nxtPer - DateTime.Now);
            float StartSize = 1f;
            float EndSize = 40;
            float Time = 60 * 40;
            float R = (float)(NthRoot(EndSize / StartSize, Time) - 1);
            float Size = (float)(StartSize * Math.Pow((1 + R), 2400 - timespan.TotalSeconds));
            Debug.WriteLine(Size);
            Debug.WriteLine(timespan.TotalSeconds);
            Debug.WriteLine(txtColour());
            Point pt_Mouse = new Point(System.Windows.Forms.Cursor.Position.X + 10, System.Windows.Forms.Cursor.Position.Y + 10);

            window.Show(pt_Mouse,
                    (byte)255,
                    txtColour(),
                    new System.Drawing.Font("Segoe UI", Size),
                    1,
                    FloatingWindow.AnimateMode.SlideTopToBottom,
                    0,
                    timespan.Minutes + ":" + (timespan.Seconds.ToString().Length == 1 ? "0" + timespan.Seconds.ToString() : timespan.Seconds.ToString()));

        }

        static DateTime getNextPeriod()
        {
            foreach (DateTime t in times)
            {
                if (DateTime.Now < t)
                {
                    FoundClass = true;
                    return t;
                }
            }
            FoundClass = false;
            return DateTime.Now; ;
        }
    }
}
