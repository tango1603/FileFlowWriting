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
            // путь к файлу
            string folderName = @"c:\Temp";
            Directory.CreateDirectory(folderName);            
            string fileName = "Temp.txt";
            string fileName2 = "Temp2.txt";
            string fileName3 = "Temp3.txt";
            string folderName1= Path.Combine(folderName, fileName);
            string folderName2 = Path.Combine(folderName, fileName2);
            string folderName3 = Path.Combine(folderName, fileName3);



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


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
