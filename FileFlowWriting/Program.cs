using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFlowWriting
{
    class Program
    {
        static void Main(string[] args)
        {           
            string folderName = @"c:\Temp"; // путь к файлу
            string fileName = "Temp.txt";//  имя файла

            // Создаем дирректорию если ее нет
            DirectoryInfo dirInfo = new DirectoryInfo(folderName);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();                
            }

            string fullFolderName = Path.Combine(folderName, fileName);

            // Создаем файл если его нет
            FileInfo fi = new FileInfo(fullFolderName);
            if (!fi.Exists)
            {
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine("Этот текст создан при создании файла в основном потоке");
                    sw.WriteLine("");
                }
            }

            //Читаем файл в консоль
            using (StreamReader sr = fi.OpenText())
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

            
            /*
            // These examples assume a "C:\Users\Public\TestFolder" folder on your machine.
            // You can modify the path if necessary.


            // Example #1: Write an array of strings to a file.
            // Create a string array that consists of three lines.
            string[] lines = { "First line", "Second line", "Third line" };
            File.WriteAllLines(folderName1, lines);


            // Example #2: Write one string to a text file.
            string text = "A class is the most powerful data type in C#. Like a structure, " +
                           "a class defines the data and behavior of the data type. ";
            // WriteAllText creates a file, writes the specified string to the file,
            // and then closes the file.    You do NOT need to call Flush() or Close().
            File.WriteAllText(folderName2, text);

            // Example #3: Write only some strings in an array to a file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            // NOTE: do not use FileStream for text files because it writes bytes, but StreamWriter
            // encodes the output as text.
            using (StreamWriter file = new StreamWriter(folderName3))
            {
                foreach (string line in lines)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }
                }
            }

            // Example #4: Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls 
            // IDisposable.Dispose on the stream object.
            using (StreamWriter file = new StreamWriter(folderName1, true))
            {
                file.WriteLine("Fourth line");
            }
            */

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}