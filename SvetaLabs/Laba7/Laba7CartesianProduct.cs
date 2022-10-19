using SvetaLabs.Laba7.WorkWithFiles;
using SvetaLabs.MeasureTime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SvetaLabs.Laba7
{
    public class Laba7CartesianProduct
    {
        private GenerateFiles _generateFiles; // для того щоб згенерувати файли
        private ReadFromFile _readFromFile; // для того щоб отримати дані з файлів
        public Laba7CartesianProduct()
        {
            _generateFiles = new GenerateFiles();
            _readFromFile = new ReadFromFile();
        }

        public void Start()
        {
            _generateFiles.CreateFiles(); // створюємо файли з масивами

            var measureTheTime = new MeasureTheTime(); // створюємо екземпляр классу який вимірює час 

            Console.WriteLine($"StartWithoutMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithoutMultiTreading)}"); // вимірюємо час роботи функції StartWithoutMultiTreading

            Console.WriteLine($"StartWithMultiTreading was ended in " +
                $"{measureTheTime.GiveTimeOfWorking(StartWithMultiTreading)}"); // вимірюємо час роботи функції StartWithMultiTreading
        }


        private void StartWithoutMultiTreading() // функція без багатопочності
        {
            var data = _readFromFile.getAllData(); // отримуємо дані з масиву

            var list1 = data[0];
            var list2 = data[1];

            var result = new List<int>(list1.Count * list2.Count); // створюємо результуючий масив та виділяємо пам'ять

            foreach (var item1 in list1)  // рахуїмо декартовий добуток
            {
                foreach (var item2 in list2)
                {
                    result.Add(item1 * item2);
                }
            }
        }

        private void StartWithMultiTreading() // функція з багатопочності
        {
            var data = _readFromFile.getAllData(); // отримуємо дані з масиву

            var list1 = data[0];
            var list2 = data[1];

            var listThread = new List<Thread>(list1.Count * list2.Count); // створюємо список з потоками

            var result = new List<int>(list1.Count * list2.Count); // створюємо результуючий масив та виділяємо пам'ять

            foreach (var item1 in list1) // рахуїмо декартовий добуток
            {
                listThread.Add(new Thread(() => MultiplyMetod(list2, result, item1))); // заповнюємо список з потоками
            }

            foreach (var item in listThread) // запускаємо потоки для паралельного виконання
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
