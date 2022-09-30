using SvetaLabs.Laba3;
using System;

namespace SvetaLabs
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var laba2 = new Laba2Parallel();
            //laba2.Start();

            var laba3 = new Laba3QuasiMinimalMethod();
            laba3.Start();

            Console.ReadKey();
        }
    }
}
