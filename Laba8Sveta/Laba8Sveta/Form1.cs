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
            Thread threadR = new Thread(() => increaseColor(ConsoleColor.Red)); // ���������� ���� ���� ������ �������� ����
            Thread threadG = new Thread(() => increaseColor(ConsoleColor.Green)); // ���������� ���� ���� ������ �������� �������
            Thread threadB = new Thread(() => increaseColor(ConsoleColor.Blue)); // ���������� ���� ���� ������ �������� ���������
           
            threadR.Start(); // ��������� ���� ��� ���� ��������� �������
            threadR.Join(); // ������� ���� ��� ���� ��������� �������
            threadG.Start();// ��������� ���� ��� ���� �������� �������
            threadG.Join();// ������� ���� ��� ���� �������� �������
            threadB.Start();// ��������� ���� ��� ���� ���������� �������
            threadB.Join();// ������� ���� ��� ���� ���������� �������

        }
        private void increaseColor(ConsoleColor color) // ������� ��� ���� �������
        {
            lock (lock_obj) // ������� ������ 
            {
                var numOfThread = Thread.CurrentThread.ManagedThreadId; // �������� ����� ��������� ������
                var strLog = $"-> {numOfThread} Thread" +
                    $"run changing of {color.ToString()} color";
                writeToLogFile.WriteToFile(strLog); // �������� � ���� �� ����� ����������
                switch (color) // ������� ���� ��������� �� ���������� �������
                {
                    case ConsoleColor.Red: 
                        for (int i = 1; i < 255; i++)
                        {
                            if (r >= 255) // �������� ��� �� ����� �� ������ ����������� ��������
                            {
                                continue;
                            }
                            r++;
                            
                            this.BackColor = Color.FromArgb(r, g, b); // ������� ����
                            Thread.Sleep(10); // �������� ��� ���� ��� �������� ���� �������
                        }
                        break;
                    case ConsoleColor.Green:
                        for (int i = 1; i < 255; i++)
                        {
                            if (g >= 255) // �������� ��� �� ����� �� ������ ����������� ��������
                            {
                                continue;
                            }
                            g++;
                            
                            this.BackColor = Color.FromArgb(r, g, b); // ������� ����
                            Thread.Sleep(10); // �������� ��� ���� ��� �������� ���� �������
                        }
                        break;
                    case ConsoleColor.Blue:
                        for (int i = 0; i < 255; i++)
                        {
                            if (b >= 255) // �������� ��� �� ����� �� ������ ����������� ��������
                            {
                                continue;
                            }
                            b++;
                            
                            this.BackColor = Color.FromArgb(r, g, b); // ������� ����
                            Thread.Sleep(10); // �������� ��� ���� ��� �������� ���� �������
                        }
                        break;
                    default:
                        break;
                }

                strLog = $"-> {Thread.CurrentThread.ManagedThreadId} Thread" +
                    $"ended";
                writeToLogFile.WriteToFile(strLog); // �������� � ���� �� ����� ����������
            } 
        }

    }
}