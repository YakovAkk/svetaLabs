using System;
using System.Diagnostics;
using System.Threading.Tasks;

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

        public async Task<long> GiveTimeOfWorkingOfTask(Func<Task> action)
        {
            var sw = new Stopwatch();
            sw.Start();
            await action();
            sw.Stop();

            return sw.ElapsedMilliseconds;
        }
    }
}
