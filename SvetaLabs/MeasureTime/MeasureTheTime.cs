using System;
using System.Diagnostics;

namespace SvetaLabs.MeasureTime
{
    public class MeasureTheTime
    {
        public long GiveTimeOfWorking(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();

            return sw.ElapsedMilliseconds;
        }
    }
}
