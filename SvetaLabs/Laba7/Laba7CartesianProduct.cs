using SvetaLabs.Laba7.WorkWithFiles;
using SvetaLabs.MeasureTime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SvetaLabs.Laba7
{
    public class Laba7CartesianProduct
    {
        private GenerateFiles _generateFiles;
        private ReadFromFile _readFromFile;
        public Laba7CartesianProduct()
        {
            _generateFiles = new GenerateFiles();
            _readFromFile = new ReadFromFile();   
        }

        public void Start()
        {
            _generateFiles.CreateFiles();

            var measureTheTime = new MeasureTheTime(); // створюємо екземпляр классу який вимірює час 

            Console.WriteLine($"StartWithoutMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithoutMultiTreading)}"); // вимірюємо час роботи функції StartWithoutMultiTreading

            Console.WriteLine($"StartWithMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithMultiTreading)}"); // вимірюємо час роботи функції StartWithMultiTreading
        }


        private void StartWithoutMultiTreading()
        {
            var data = _readFromFile.getAllData();

            var list1 = data[0];
            var list2 = data[1];

            var result = new List<int>(list1.Count * list2.Count);

            foreach (var item1 in list1)
            {
                foreach (var item2 in list2)
                {
                    result.Add(item1 * item2);
                }
            }

            Console.WriteLine();
        }

        private void StartWithMultiTreading()
        {
            var data = _readFromFile.getAllData();

            var list1 = data[0];
            var list2 = data[1];

            var listThread = new List<Thread>(list1.Count * list2.Count);

            var result = new List<int>(list1.Count * list2.Count);

            foreach (var item1 in list1)
            {
                listThread.Add(new Thread(() => MultiplyMetod(list2, result, item1)));
            }

            foreach (var item in listThread)
            {
                item.Start();
                item.Join();
            }

            Console.WriteLine();
        }

        private static void MultiplyMetod(List<int> list2, List<int> result, int item1)
        {
            foreach (var item2 in list2)
            {
                result.Add(item1 * item2);
            }
        }
    }
}
