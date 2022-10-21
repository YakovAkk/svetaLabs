namespace Laba8Sveta
{
    public partial class Form1 : Form
    {
        private object lock_obj;
        private readonly WriteToLogFile writeToLogFile;
        static int r = 1;
        static int g = 1;
        static int b = 1;

        public Form1()
        {
            lock_obj = new object();
            writeToLogFile = new WriteToLogFile();
            InitializeComponent();
        }
        

        private void Start_Click(object sender, EventArgs e)
        {
            Thread threadR = new Thread(() => increaseColor(ConsoleColor.Red)); // Стрворюємо потік який змінить червоний колір
            Thread threadG = new Thread(() => increaseColor(ConsoleColor.Green)); // Стрворюємо потік який змінить червоний зелений
            Thread threadB = new Thread(() => increaseColor(ConsoleColor.Blue)); // Стрворюємо потік який змінить червоний блакитний
           
            threadR.Start(); // Запускаємо потік для зміни червоного кольору
            threadR.Join(); // Джоіниио потік для зміни червоного кольору
            threadG.Start();// Запускаємо потік для зміни зеленого кольору
            threadG.Join();// Джоіниио потік для зміни зеленого кольору
            threadB.Start();// Запускаємо потік для зміни блакитного кольору
            threadB.Join();// Джоіниио потік для зміни блакитного кольору

        }
        private void increaseColor(ConsoleColor color) // функція для зміни кольору
        {
            lock (lock_obj) // блокуємо потоки 
            {
                var numOfThread = Thread.CurrentThread.ManagedThreadId; // отримуємо номер поточного потоку
                var strLog = $"-> {numOfThread} Thread" +
                    $"run changing of {color.ToString()} color";
                writeToLogFile.WriteToFile(strLog); // записуємо в логи що поток запустився
                switch (color) // змінюємо колір відповідньо до переданого кольору
                {
                    case ConsoleColor.Red: 
                        for (int i = 1; i < 255; i++)
                        {
                            if (r >= 255) // перевірка щоб не вийта за кордор допустимого значення
                            {
                                continue;
                            }
                            r++;
                            
                            this.BackColor = Color.FromArgb(r, g, b); // Змінюємо колір
                            Thread.Sleep(10); // затримка для того щоб побачити зміну кольорів
                        }
                        break;
                    case ConsoleColor.Green:
                        for (int i = 1; i < 255; i++)
                        {
                            if (g >= 255) // перевірка щоб не вийта за кордор допустимого значення
                            {
                                continue;
                            }
                            g++;
                            
                            this.BackColor = Color.FromArgb(r, g, b); // Змінюємо колір
                            Thread.Sleep(10); // затримка для того щоб побачити зміну кольорів
                        }
                        break;
                    case ConsoleColor.Blue:
                        for (int i = 0; i < 255; i++)
                        {
                            if (b >= 255) // перевірка щоб не вийта за кордор допустимого значення
                            {
                                continue;
                            }
                            b++;
                            
                            this.BackColor = Color.FromArgb(r, g, b); // Змінюємо колір
                            Thread.Sleep(10); // затримка для того щоб побачити зміну кольорів
                        }
                        break;
                    default:
                        break;
                }

                strLog = $"-> {Thread.CurrentThread.ManagedThreadId} Thread" +
                    $"ended";
                writeToLogFile.WriteToFile(strLog); // записуємо в логи що поток завершився
            } 
        }

    }
}