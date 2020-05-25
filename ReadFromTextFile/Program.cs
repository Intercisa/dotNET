using System;

namespace ReadFromTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //example 1, for reading text
            string text = System.IO.File.ReadAllText(@"C:\Users\sipic\Source\Repos\ReadFromTextFile\textFile.txt");

            Console.WriteLine($"textfile contains following text: {text}");

            //example 2, for reading text all lines
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\sipic\Source\Repos\ReadFromTextFile\textFile.txt");

            foreach (string line in lines)
            {
                Console.WriteLine($"lines from textFile: {line}");

            }


            Console.ReadKey();

        }
    }
}
