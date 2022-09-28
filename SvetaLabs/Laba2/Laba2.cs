using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SvetaLabs.Laba2
{
    public class Laba2Parallel
    {
        static Random random = new Random();
        int height { get; set; }
        int width { get; set; }

        public Laba2Parallel()
        {
            height = 1000;
            width = 1000;
        }
        public void StartWithoutMultiTreading()
        {
            int[,] matrix = new int[height, width];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(height);
                }
            }

            var dict = new Dictionary<int, int>();

            foreach (var item in matrix)
            {
                if (dict.Keys.Contains(item))
                {
                    continue;
                }
                else
                {
                    var result = FindCountOfNumber(matrix, item);
                    dict.Add(item, result);

                    //Console.WriteLine("Current Tread {0}", Thread.CurrentThread.ManagedThreadId);
                }
            }

            Console.WriteLine("Done");

            //foreach (var item in dict)
            //{
            //    Console.WriteLine($"{item.Key} - {item.Value}");
            //}
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

        public void StartWithMultiTreading()
        {
            Func<int[,], int, int> func = new Func<int[,], int, int>(FindCountOfNumber);

            int[,] matrix = new int[height, width];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(height);
                }
            }


            for (int i = 0; i < matrix.GetLength(0); i++)
            {

            }

            var dict = new Dictionary<int, int>();

            foreach (var item in matrix)
            {
                if (dict.Keys.Contains(item))
                {
                    continue;
                }
                else
                {
                    IAsyncResult asyncResult = func.BeginInvoke(matrix, item, null, null); 
                    dict.Add(item, func.EndInvoke(asyncResult));
                }
            }

            Console.WriteLine("Done");

            //foreach (var item in dict)
            //{
            //    Console.WriteLine($"{item.Key} - {item.Value}");
            //}

            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        Console.Write(matrix[i,j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }

        private void AddComplete(IAsyncResult asyncResult)
        {
            //Console.WriteLine("Current Tread {0}" , Thread.CurrentThread.ManagedThreadId);
        }
    }
}
