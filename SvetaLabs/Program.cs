using SvetaLabs.Laba2;
using System;
using System.Diagnostics;

namespace SvetaLabs
{
    public class Program
    {
        static void Main(string[] args)
        {
            var laba2 = new Laba2Parallel();

            var sw = new Stopwatch();
            sw.Start();
            laba2.StartWithoutMultiTreading();
            sw.Stop();

            Console.WriteLine($"StartWithoutMultiTreading was ended in {sw.ElapsedMilliseconds}");

            sw.Reset();
            sw.Start();
            laba2.StartWithMultiTreading();

            sw.Stop();

            Console.WriteLine($"StartWithMultiTreading was ended in {sw.ElapsedMilliseconds}");

            Console.ReadKey();
        }
    }
}
