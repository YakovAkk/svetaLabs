using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SvetaLabs.Laba2
{
    public class Laba2Parallel
    {
        static Random random = new Random();
        private int height;
        private int width;
        private int[,] _matrix;

        public Laba2Parallel()
        {
            height = 1000;
            width = 1000;

            _matrix = new int[height, width];

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    _matrix[i, j] = random.Next(height);
                }
            }
        }

        public void Start()
        {
            var sw = new Stopwatch();
            sw.Start();
            StartWithoutMultiTreading();
            sw.Stop();

            Console.WriteLine($"StartWithoutMultiTreading was ended in {sw.ElapsedMilliseconds}");

            sw.Reset();
            sw.Start();
            StartWithMultiTreading();

            sw.Stop();

            Console.WriteLine($"StartWithMultiTreading was ended in {sw.ElapsedMilliseconds}");
        }
        private void StartWithoutMultiTreading()
        {
            var dict = new Dictionary<int, int>();

            foreach (var item in _matrix)
            {
                if (dict.Keys.Contains(item))
                {
                    continue;
                }
                else
                {
                    var result = FindCountOfNumber(_matrix, item);
                    dict.Add(item, result);
                }
            }


            Console.WriteLine("Done");


        }
        private int FindCountOfNumber(int[,] matrix, int num)
        {
            int count = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (num == matrix[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        private Tuple<int, int> FindCountOfNumberAsync(int[,] matrix, int num)
        {
            int count = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (num == matrix[i, j])
                    {
                        count++;
                    }
                }
            }

            return new Tuple<int, int>(num, count);
        }
        private void StartWithMultiTreading()
        {

            var asyncResultList = new List<IAsyncResult>();
            var funcList = new List<Func<int[,], int, Tuple<int, int>>>();

            var dict = new Dictionary<int, int>();

            foreach (var item in _matrix)
            {
                if (dict.Keys.Contains(item))
                {
                    continue;
                }
                else
                {
                    Func<int[,], int, Tuple<int, int>> func = 
                        new Func<int[,], int, Tuple<int, int>>(FindCountOfNumberAsync);
                    IAsyncResult asyncResult = func.BeginInvoke(_matrix, item, null, null); 
                    dict.Add(item, 0);
                    asyncResultList.Add(asyncResult);
                    funcList.Add(func);
                }
            }

            for (int i = 0; i < asyncResultList.Count; i++)
            {
                var res = funcList[i].EndInvoke(asyncResultList[i]);

                dict[res.Item1] = res.Item2;
            }

            Console.WriteLine("Done");
        }
    }
}
