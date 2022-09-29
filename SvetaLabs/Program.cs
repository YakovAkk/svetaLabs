using SvetaLabs.Laba2;
using SvetaLabs.Laba3;
using System;
using System.Diagnostics;

namespace SvetaLabs
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var laba2 = new Laba2Parallel();
            //laba2.Start();

            var laba3 = new Laba3Parallel();
            laba3.Start();

            Console.ReadKey();
        }
    }
}
