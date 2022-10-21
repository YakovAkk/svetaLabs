using SvetaLabs.Laba2;
using SvetaLabs.Laba3;
using SvetaLabs.Laba7;
using SvetaLabs.Laba7.WorkWithFiles;
using SvetaLabs.Laba8;
using System;

namespace SvetaLabs
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var laba2 = new Laba2Parallel();
            //laba2.Start();

            //var laba3 = new Laba3QuasiMinimalMethod();
            //laba3.Start();

            //var laba6 = new Laba6Graf();
            //laba6.Start();

            //var laba7 = new Laba7CartesianProduct();
            //laba7.Start();

            var laba8 = new Laba8Synchronization();
            laba8.Start();

            Console.ReadKey();
        }
    }
}
