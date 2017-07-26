using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileFlowWriting
{
    class Program
    {
        static AutoResetEvent waitHandler = new AutoResetEvent(true);
        static string fullFolderFileName;

        static void Main(string[] args)
        {
            //******************************************Подготовительная работа с файлом*********************************************
            string folderName = @"c:\Temp"; // путь к файлу
            string fileName = "Temp.txt";//  имя файла
            

            // Создаем дирректорию если ее нет
            DirectoryInfo dirInfo = new DirectoryInfo(folderName);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            fullFolderFileName = Path.Combine(folderName, fileName);

            // Создаем файл если его нет
            FileInfo fi = new FileInfo(fullFolderFileName);
            if (!fi.Exists)
            {
                using (StreamWriter sw = new StreamWriter(fullFolderFileName, false, Encoding.Default))
                {
                    sw.WriteLine("Этот текст создан при создании файла в ГЛАВНОМ потоке");
                    sw.WriteLine("Этот текст создан при создании файла в ГЛАВНОМ потоке");
                    sw.WriteLine("Этот текст создан при создании файла в ГЛАВНОМ потоке");
                    sw.WriteLine("Этот текст создан при создании файла в ГЛАВНОМ потоке");
                    sw.WriteLine("");
                }
            }

            // создаем новый поток
            Thread myThread = new Thread(Count);
            myThread.Start(); // запускаем поток

            
            for (int i = 1; i < 20; i++)
            {
                waitHandler.WaitOne();
                Console.WriteLine("Пишет в файл ГЛАВНЫЙ поток:");

                using (StreamWriter sw = new StreamWriter(fullFolderFileName, true, Encoding.Default))
                {
                    sw.WriteLine("Этот текст создан в ГЛАВНОМ потоке");
                }
                Thread.Sleep(200);
                waitHandler.Set();
            }
            

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public static void Count()
        {
            
                for (int i = 1; i < 20; i++)
                {
                waitHandler.WaitOne();
                Console.ForegroundColor = ConsoleColor.Red;     // Устанавливаем красный цвет символов    
                    Console.WriteLine("Пишет в файл ВТОРОЙ поток:");
                    using (StreamWriter sw = new StreamWriter((string)fullFolderFileName, true, Encoding.Default))
                    {
                        sw.WriteLine("Этот текст создан во ВТОРОМ потоке");
                    }
                    Console.ResetColor(); // Устанавливаем белый цвет символов
                    Thread.Sleep(300);
                waitHandler.Set();
            }
                
            
        }



        /*
            try
            {
                Console.WriteLine("******считываем весь файл********");
                using (StreamReader sr = new StreamReader(fullFolderFileName))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }

                Console.WriteLine();
                Console.WriteLine("******считываем построчно********");
                using (StreamReader sr = new StreamReader(fullFolderFileName, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("******считываем блоками********");
                using (StreamReader sr = new StreamReader(fullFolderFileName, Encoding.Default))
                {
                    char[] array = new char[4];
                    // считываем 4 символа
                    sr.Read(array, 0, 4);
                    Console.WriteLine(array);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/

        //******************************************работа с потоком*********************************************

        /*  Thread t = Thread.CurrentThread;
            t.Name = "Метод Main";
            Console.WriteLine("Имя потока: {0}", t.Name);
            Console.WriteLine("Запущен ли поток: {0}", t.IsAlive);
            Console.WriteLine("Приоритет потока: {0}", t.Priority);
            Console.WriteLine("Статус потока: {0}", t.ThreadState);
            // получаем домен приложения
            Console.WriteLine("Домен приложения: {0}", Thread.GetDomain().FriendlyName);

    }    

       /* class Program
        {
            static int x = 0;
            static object locker = new object();
            static void Main(string[] args)
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread myThread = new Thread(Count);
                    myThread.Name = "Поток " + i.ToString();
                    myThread.Start();
                }

                Console.ReadLine();
            }
            public static void Count()
            {
                lock (locker)
                {
                    x = 1;
                    for (int i = 1; i < 9; i++)
                    {
                        Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                        x++;
                        Thread.Sleep(100);
                    }
                }
            }
        }
        */

    }
}