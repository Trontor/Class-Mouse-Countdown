using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Class_Countdown
{
    class BaseClass
    {
        Timer t = new Timer();
        List<DateTime> times = new List<DateTime>();
        public BaseClass()
        {
            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 20, 0);
            for (int i = 0; i < 8; i++)
            {
                if (i == 4)
                    times.Add(time.AddMinutes((i - 1) * 40 + 20));
                else
                    times.Add(time.AddMinutes(40 * i));
            }


            t.Elapsed += t_Elapsed;
            t.Interval = 100;
            t.Start();
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {

        }
    }
}
