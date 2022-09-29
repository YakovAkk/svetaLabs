using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SvetaLabs.Laba3
{
    public class Laba3Parallel
    {
        static Random random = new Random();
        public int Size = 1000;
        public void Start()
        {
            var sw = new Stopwatch();
            sw.Start();
            StartWithoutMultiTreading();
            sw.Stop();

            Console.WriteLine($"StartWithoutMultiTreading was ended in {sw.ElapsedMilliseconds}");

            sw.Restart();
            sw.Start();
            StartWithMultiTreading();
            sw.Stop();

            Console.WriteLine($"StartWithMultiTreading was ended in {sw.ElapsedMilliseconds}");
        }
        public void StartWithoutMultiTreading()
        {

            double[,] MatrixCoef = new double[Size, Size];

            for (int i = 0; i < MatrixCoef.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixCoef.GetLength(1); j++)
                {
                    MatrixCoef[i, j] = random.NextDouble();
                }
            }

            double[] FreeCoef = new double[Size];

            for (int i = 0; i < FreeCoef.Length; i++)
            {
                FreeCoef[i] = random.NextDouble();
            }

            double Multi1, Multi2;
            double[] Result = new double[Size];
            Console.WriteLine();
            
            for (int k = 0; k < Size; k++)
            {
                for (int j = k + 1; j < Size; j++)
                {
                    Multi1 = MatrixCoef[j, k] / MatrixCoef[k, k];
                    for (int i = k; i < Size; i++)
                    {
                        MatrixCoef[j, i] = MatrixCoef[j, i] - Multi1 * MatrixCoef[k, i];
                    }
                    FreeCoef[j] = FreeCoef[j] - Multi1 * FreeCoef[k];
                }
            }
            for (int k = Size - 1; k >= 0; k--)
            {
                Multi1 = 0;
                for (int j = k; j < Size; j++)
                {
                    Multi2 = MatrixCoef[k, j] * Result[j];
                    Multi1 += Multi2;
                }
                Result[k] = (FreeCoef[k] - Multi1) / MatrixCoef[k, k];
            }

            Console.WriteLine("Done");
        }
        public void StartWithMultiTreading()
        {
            double[,] MatrixCoef = new double[Size, Size];

            for (int i = 0; i < MatrixCoef.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixCoef.GetLength(1); j++)
                {
                    MatrixCoef[i, j] = random.NextDouble();
                }
            }

            double[] FreeCoef = new double[Size];

            for (int i = 0; i < FreeCoef.Length; i++)
            {
                FreeCoef[i] = random.NextDouble();
            }

            double Multi1, Multi2;
            double[] Result = new double[Size];

            var thr = new List<Thread>();

            for (int k = 0; k < Size; k++)
            {
                thr.Add( new Thread(() =>  CountingMatrix(MatrixCoef, FreeCoef, k)));
            }

            foreach (var item in thr)
            {
                item.Start();
                item.Join();
            }

            var thr2 = new List<Thread>();

            for (int k = Size - 1; k >= 0; k--)
            {
                thr.Add(new Thread(() => GetResult(MatrixCoef, FreeCoef, Result, k)));
            }

            foreach (var item in thr2)
            {
                item.Start();
                item.Join();
            }

            Console.WriteLine("Done");
        }
        private void GetResult(double[,] MatrixCoef, 
            double[] FreeCoef, double[] Result, int k)
        {
            double Multi1 = 0, Multi2 = 0;
            for (int j = k; j < Size; j++)
            {
                Multi2 = MatrixCoef[k, j] * Result[j];
                Multi1 += Multi2;
            }
            Result[k] = (FreeCoef[k] - Multi1) / MatrixCoef[k, k];
        }
        private double CountingMatrix(double[,] MatrixCoef, double[] FreeCoef, int k)
        {
            double Multi1 = 0;
            for (int j = k + 1; j < Size; j++)
            {
                Multi1 = MatrixCoef[j, k] / MatrixCoef[k, k];
                for (int i = k; i < Size; i++)
                {
                    MatrixCoef[j, i] = MatrixCoef[j, i] - Multi1 * MatrixCoef[k, i];
                }
                FreeCoef[j] = FreeCoef[j] - Multi1 * FreeCoef[k];
            }

            return Multi1;
        }
    }
}
