using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvetaLabs.Laba7
{
    public class GenerateFiles
    {
        List<string> pathToFiles = new List<string>() // шляхи до файлів у яких будуть лежати масиви
            {
                "../../Laba7/FirstArray.txt",
                "../../Laba7/SecondArray.txt",
            };

        private int _lenght = 500; // довжина массивів
        private static Random random = new Random(); // для того щоб зарандомити масиви
        private string GenerateData() 
        {
            var list = new List<int>(_lenght); // створення стиску

            for (int i = 0; i < _lenght; i++) // рандомимо список
            {
                list.Add(random.Next(1, 1000));
            }

            var stringBuilder = new StringBuilder(); // створюємо StringBuilder
                                                     // для того щоб збільшити швидкість виконання конкатенації строки

            foreach (var item in list) // створюємо строку для запису у файл
            {
                stringBuilder.Append(item.ToString() + " ");
            }

            return stringBuilder.ToString(); // вертаємо масив у строці
        }
        private void WriteToFile(string path, string text)
        {
            using (var file = new StreamWriter(path, false)) // записуємо строку у файл
            {
                file.Write(text);
            }
        }
        public void CreateFiles()
        {
            foreach (var item in pathToFiles) // записуємо масив у файл
            {
                WriteToFile(item, GenerateData());
            }
        }
    }
}
