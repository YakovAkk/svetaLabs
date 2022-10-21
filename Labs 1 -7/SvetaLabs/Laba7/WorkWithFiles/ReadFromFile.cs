using System;
using System.Collections.Generic;
using System.IO;

namespace SvetaLabs.Laba7.WorkWithFiles
{
    public class ReadFromFile
    {
        private List<string> pathToFiles = new List<string>() // шляхи до файлів у яких будуть лежати масиви
            {
                "../../Laba7/FirstArray.txt",
                "../../Laba7/SecondArray.txt",
            };

        private List<int> getDataFromFile(string path) // отримуємо масив з файлу
        {
            string text;

            using (var reader = new StreamReader(path)) // читаємо з файлу
            {
                text = reader.ReadToEnd();
            }

            var list = new List<int>(); 

            var listOfCharacters = text.Split(' '); // ділимо строку на цифри

            foreach (var item in listOfCharacters) // заповнюємо масив з данними
            {
                try
                {
                    list.Add(Convert.ToInt32(item));
                }
                catch (Exception)
                {

                }
            }

            return list; // вертаємо масив
        }

        public List<List<int>> getAllData() // отлимуємо дані з двох файлів
        {
            var result = new List<List<int>>();

            foreach (var item in pathToFiles) // записуємо дані у масив 
            {
                result.Add(getDataFromFile(item));
            }

            return result; // вертаємо 2 масиви
        }
    }
}
