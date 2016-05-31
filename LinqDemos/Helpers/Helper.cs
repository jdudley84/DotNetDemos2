using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemos
{
    class Helper
    {
        public TimeSpan Time(Action action)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            action?.Invoke();
            timer.Stop();
            return timer.Elapsed;
        }
    }
}
