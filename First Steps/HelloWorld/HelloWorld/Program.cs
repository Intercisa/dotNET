using System;

namespace HelloWorld
{
    class Program
    {
        private const string WhatsYourName = "Hello, ?!";

        static void Main(string[] args){
            SetConsoleColors();
            Console.WriteLine(WhatsYourName);
            var name = Console.ReadLine();
            Console.WriteLine($"Hello {name}");
            Console.Read();
        }

        private static void SetConsoleColors(){
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
        }
    }
}
