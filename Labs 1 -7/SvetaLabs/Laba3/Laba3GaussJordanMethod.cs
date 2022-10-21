using SvetaLabs.MeasureTime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SvetaLabs.Laba3
{
    public class Laba3GaussJordanMethod
    {
        private static Random _random = new Random();
        private int _size = 1000;

        private double[,] _matrixCoef;
        private double[] _freeCoef;
        private double[] _result;
        public Laba3GaussJordanMethod()
        {

            _matrixCoef = new double[_size, _size];

            for (int i = 0; i < _matrixCoef.GetLength(0); i++)
            {
                for (int j = 0; j < _matrixCoef.GetLength(1); j++)
                {
                    _matrixCoef[i, j] = _random.NextDouble();
                }
            }

            _freeCoef = new double[_size];

            for (int i = 0; i < _freeCoef.Length; i++)
            {
                _freeCoef[i] = _random.Next(_size);
            }

            _result = new double[_size];

            for (int i = 0; i < _result.Length; i++)
            {
                _result[i] = _random.NextDouble();
            }

        }
        public void Start()
        {
            var measureTheTime = new MeasureTheTime(); // створюємо екземпляр классу який вимірює час 

            Console.WriteLine($"StartWithoutMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithoutMultiTreading)}"); // вимірюємо час роботи функції StartWithoutMultiTreading

            Console.WriteLine($"StartWithMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithMultiTreading)}"); // вимірюємо час роботи функції StartWithMultiTreading
        }
        public void StartWithoutMultiTreading()
        {
            double Multi1, Multi2;  // створбємо тимчасові змінні

            for (int k = 0; k < _size; k++)
            {
                for (int j = k + 1; j < _size; j++)
                {
                    Multi1 = _matrixCoef[j, k] / _matrixCoef[k, k]; // ділемо відповідні коефіцієтти відповідних рядків матриці
                    for (int i = k; i < _size; i++)
                    // цикл проходиться по всім коефіцієнтам рядка i та ділить їх на відповідні коеф рядка k
                    {
                        _matrixCoef[j, i] = _matrixCoef[j, i] - Multi1 * _matrixCoef[k, i];
                    }
                    _freeCoef[j] = _freeCoef[j] - Multi1 * _freeCoef[k]; // віднімаємо рядки щоб знищити коеф
                }
            }

            for (int k = _size - 1; k >= 0; k--) // проходимся по рядку k для того щиб знищити коефіціентки які домножили перед цим 
            {
                Multi1 = 0;
                for (int j = k; j < _size; j++)
                {
                    Multi2 = _matrixCoef[k, j] * _result[j];
                    Multi1 += Multi2;
                }
                _result[k] = (_freeCoef[k] - Multi1) / _matrixCoef[k, k]; // записуємо значення всіх коефіцієнтів у масив відповідей
            }

            Console.WriteLine("Done");
        }
        public void StartWithMultiTreading()
        {

            var thr = new List<Thread>(); // створюємо лист потоків

            for (int k = 0; k < _size; k++)
            {
                thr.Add(new Thread(() => CountingMatrix(_matrixCoef, _freeCoef, k)));
                // запускаємо в потік функцію яка домножає коефіцієнти відповідних рядків матриці
            }

            foreach (var item in thr) // запускаємо потоки та отримуємо від них результати
            {
                item.Start();
                item.Join();
            }

            var thr2 = new List<Thread>(); // створюємо лист потоків

            for (int k = _size - 1; k >= 0; k--)
            {
                thr.Add(new Thread(() => GetResult(_matrixCoef, _freeCoef, _result, k)));
                // запускаємо в потік функцію яка записує результати у масив _result
            }

            foreach (var item in thr2) // запускаємо потоки та отримуємо від них результати
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
            for (int j = k; j < _size; j++)
            {
                Multi2 = MatrixCoef[k, j] * Result[j];
                Multi1 += Multi2;
            }
            Result[k] = (FreeCoef[k] - Multi1) / MatrixCoef[k, k];
        }
        private void CountingMatrix(double[,] MatrixCoef, double[] FreeCoef, int k)
        {
            double Multi1 = 0;
            for (int j = k + 1; j < _size; j++)
            {
                Multi1 = MatrixCoef[j, k] / MatrixCoef[k, k];
                for (int i = k; i < _size; i++)
                {
                    MatrixCoef[j, i] = MatrixCoef[j, i] - Multi1 * MatrixCoef[k, i];
                }
                FreeCoef[j] = FreeCoef[j] - Multi1 * FreeCoef[k];
            }
        }
    }
}
