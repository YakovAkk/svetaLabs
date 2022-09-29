using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace SvetaLabs.Laba3
{
    public class Laba3Parallel
    {
        public void Start()
        {
            var sw = new Stopwatch();
            sw.Start();
            StartWithoutMultiTreading();
            sw.Stop();

            Console.WriteLine($"StartWithoutMultiTreading was ended in {sw.ElapsedMilliseconds}");
        }

        public void StartWithoutMultiTreading()
        {

            int RowAm = 100;
            double[,] MatrixCoef = new double[RowAm, RowAm];
            double[] FreeCoef = new double[RowAm];
            double Multi1, Multi2;
            double[] Result = new double[RowAm];
            Console.WriteLine();
            Console.WriteLine("Введите коэффициенты и свободные члены матрицы:");
            Console.WriteLine();
            for (int i = 0; i < RowAm; i++)
            {
                for (int j = 0; j < RowAm; j++)
                {
                    Console.Write($"A[{i + 1}][{j + 1}] = ");
                    MatrixCoef[i, j] = double.Parse(Console.ReadLine());
                }
                Console.Write($"B[{i + 1}] = ");
                FreeCoef[i] = double.Parse(Console.ReadLine());
                Console.WriteLine();
            }
            Console.WriteLine("Начальный вид матрицы:");
            Console.WriteLine();
            int MatrixRow = MatrixCoef.GetLength(0);
            int MatrixCol = MatrixCoef.GetLength(1);
            for (int i = 0; i < MatrixRow; i++)
            {
                for (int j = 0; j < MatrixCol; j++)
                {
                    Console.Write(MatrixCoef[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Свободные члены матрицы:");
            Console.WriteLine();
            for (int i = 0; i < RowAm; i++)
            {
                Console.WriteLine($"B[{i + 1}] = {FreeCoef[i]}");
            }
            Console.WriteLine();
            for (int k = 0; k < RowAm; k++)
            {
                for (int j = k + 1; j < RowAm; j++)
                {
                    Multi1 = MatrixCoef[j, k] / MatrixCoef[k, k];
                    for (int i = k; i < RowAm; i++)
                    {
                        MatrixCoef[j, i] = MatrixCoef[j, i] - Multi1 * MatrixCoef[k, i];
                    }
                    FreeCoef[j] = FreeCoef[j] - Multi1 * FreeCoef[k];
                }
            }
            for (int k = RowAm - 1; k >= 0; k--)
            {
                Multi1 = 0;
                for (int j = k; j < RowAm; j++)
                {
                    Multi2 = MatrixCoef[k, j] * Result[j];
                    Multi1 += Multi2;
                }
                Result[k] = (FreeCoef[k] - Multi1) / MatrixCoef[k, k];
            }
            Console.WriteLine("Конечный вид матрицы:");
            Console.WriteLine();
            for (int i = 0; i < MatrixRow; i++)
            {
                for (int j = 0; j < MatrixCol; j++)
                {
                    Console.Write(MatrixCoef[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Корни матрицы:");
            Console.WriteLine();
            for (int i = 0; i < RowAm; i++)
            {
                Console.WriteLine($"X[{i + 1}] = {Result[i]}");
            }
        }
    }
}
