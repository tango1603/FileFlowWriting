﻿using System;
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
            //***Подготовительная работа с файлом***
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

            Thread myThread = new Thread(Count);
            myThread.Start();                         
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
    }
}