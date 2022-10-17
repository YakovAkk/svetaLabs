using SvetaLabs.MeasureTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SvetaLabs.Laba2
{
    public class Laba2Parallel
    {
        static Random random = new Random();
        private int _height;
        private int _width;
        private int[,] _matrix;

        public Laba2Parallel()
        {
            _height = 1000;
            _width = 1000;

            _matrix = new int[_height, _width];

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    _matrix[i, j] = random.Next(_height);
                }
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
        private void StartWithoutMultiTreading()
        {
            var dict = new Dictionary<int, int>(); // словник де ключ наш елемент, а значення кількіть у матриці

            foreach (var item in _matrix)
            {
                if (dict.Keys.Contains(item)) // перевіряємо чи такий елемент був уже перевірений
                {
                    continue;
                }
                else // якщо ні тоді перевіряємой кількіть таких елементій у матриці
                {
                    var result = FindCountOfNumber(_matrix, item);
                    // функція FindCountOfNumber вертає кількіть елементів item у матриці _matrix

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

            var asyncResultList = new List<IAsyncResult>(); // створюємо стисок який містить IAsyncResult
            var funcList = new List<Func<int[,], int, Tuple<int, int>>>();
            // створюємо стисок який містить делегати для функції FindCountOfNumberAsync

            var dict = new Dictionary<int, int>(); // словник де ключ наш елемент, а значення кількіть у матриці

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
                    // створбємо делегат для функції FindCountOfNumberAsync 
                    IAsyncResult asyncResult = func.BeginInvoke(_matrix, item, null, null); // асинхронно запускаємо делегат
                    dict.Add(item, 0); // добавляємо значення у словник для того щоб знати що воно нам уже зустрічалося
                    asyncResultList.Add(asyncResult); // добавляемо asyncResult до список для того щоб потів титягнути з нього значення
                    funcList.Add(func); //добавляемо делегат до список для того щоб потів титягнути з нього значення
                }
            }

            for (int i = 0; i < asyncResultList.Count; i++)
            {
                var res = funcList[i].EndInvoke(asyncResultList[i]); // витягуєм означення за асинхнонної операції

                dict[res.Item1] = res.Item2; // сетим значення до масиву
            }

            Console.WriteLine("Done");
        }
    }
}
