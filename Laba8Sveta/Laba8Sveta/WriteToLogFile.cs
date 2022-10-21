namespace Laba8Sveta
{
    public class WriteToLogFile
    {
        string path = "../../../log.txt"; // Шлях до файлу з логами

        public WriteToLogFile() 
        {
            ClearTheFile();// Очищуємой файл
        }

        private void ClearTheFile()
        {
            using (var file = new StreamWriter(path, false)) 
            {
                file.WriteLine("");
            }
        }
        public void WriteToFile(string text)
        {
            using (var file = new StreamWriter(path, true)) // записуємо строку у файл
            {
                file.WriteLine(text);
            }
        }
    }
}
