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
        private static object locked = new object();

        static void Main(string[] args)
        {
            //***Подготовительная работа с файлом***
            // путь к файлу
            string folderName = @"c:\Temp";
            //  имя файла 
            string fileName = "Temp.txt";        
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

            Thread myThread = new Thread(Count);
            myThread.Start();                         
            for (int i = 1; i < 20; i++)
            {
                lock (locked)
                {
                    Console.WriteLine("Пишет в файл ГЛАВНЫЙ поток:");
                    using (StreamWriter sw = new StreamWriter(fullFolderFileName, true, Encoding.Default))
                    {
                        sw.WriteLine("Этот текст создан в ГЛАВНОМ потоке");
                    }
                    Thread.Sleep(200);
                }
            }            
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public static void Count()
        {            
            for (int i = 1; i < 20; i++)
            {
                lock (locked)
                {
                    // Устанавливаем красный цвет символов  
                    Console.ForegroundColor = ConsoleColor.Red;       
                    Console.WriteLine("Пишет в файл ВТОРОЙ поток:");
                    using (StreamWriter sw = new StreamWriter((string)fullFolderFileName, true, Encoding.Default))
                    {
                        sw.WriteLine("Этот текст создан во ВТОРОМ потоке");
                    }
                    // Устанавливаем белый цвет символов
                    Console.ResetColor(); 
                    Thread.Sleep(300);
                }
            }                 
        }      
    }
}