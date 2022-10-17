using System;
using System.Collections.Generic;
using System.IO;

namespace SvetaLabs.Laba7.WorkWithFiles
{
    public class ReadFromFile
    {
        private List<string> pathToFiles = new List<string>()
            {
                "../../Laba7/FirstArray.txt",
                "../../Laba7/SecondArray.txt",
            };

        private List<int> getDataFromFile(string path)
        {
            string text;

            using (var reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }

            var list = new List<int>();

            var listOfCharacters = text.Split(' ');

            foreach (var item in listOfCharacters)
            {
                try
                {
                    list.Add(Convert.ToInt32(item));
                }
                catch (Exception)
                {

                }
            }

            return list;
        }

        public List<List<int>> getAllData()
        {
            var result = new List<List<int>>();

            foreach (var item in pathToFiles)
            {
                result.Add(getDataFromFile(item));
            }

            return result;
        }
    }
}
