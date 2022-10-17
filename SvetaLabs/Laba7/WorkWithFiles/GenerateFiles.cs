using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SvetaLabs.Laba7
{
    public class GenerateFiles
    {
        List<string> pathToFiles = new List<string>()
            {
                "../../Laba7/FirstArray.txt",
                "../../Laba7/SecondArray.txt",
            };

        private int _lenght = 500;
        private static Random random = new Random();
        private string GenerateData()
        {
            var list = new List<int>(_lenght);

            for (int i = 0; i < _lenght; i++)
            {
                list.Add(random.Next(1,1000));
            }

            var stringBuilder = new StringBuilder();

            foreach (var item in list)
            {
                stringBuilder.Append(item.ToString() + " ");
            }

            return stringBuilder.ToString();
        }
        private void WriteToFile(string path, string text)
        {
            using (var file = new StreamWriter(path, false))
            {
                file.Write(text);
            }
        }

        public void CreateFiles()
        {
            foreach (var item in pathToFiles) 
            {
                WriteToFile(item, GenerateData());
            }
        }
    }
}
