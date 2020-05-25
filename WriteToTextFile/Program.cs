using System;
using System.IO;
namespace WriteTotextFile
{
    class Program
    {
        static void Main(string[] args)
        {

            ////////////////////////////////////////////////////////////////
            string[] lines = {"first line 250", "second line 242","third line 240" };
            File.WriteAllLines(@"C:\Users\sipic\Source\Repos\ReadFromTextFile\textFile2.txt", lines);
            
            ////////////////////////////////////////////////////////////////
            User user1 = new User("Sipi", 99);
            User user2 = new User("Birbi", 99);
            User user3 = new User("Naomi", 89);

            string[] usersTextData = {user1.ToString(), user2.ToString(), user3.ToString()};

            ////////////////////////////////////////////////////////////////
            File.WriteAllLines(@"C:\Users\sipic\Source\Repos\ReadFromTextFile\textFileUsersTextData.txt", usersTextData);
            Console.WriteLine("Pls give a file a name:");
            string fileName = Console.ReadLine();
            Console.WriteLine("Pls enter text for the file:");
            string input = Console.ReadLine();
            File.WriteAllText($@"C:\Users\sipic\Source\Repos\ReadFromTextFile\{fileName}.txt", input);

            ////////////////////////////////////////////////////////////////
            ///StreamWriter
            using (StreamWriter file = new StreamWriter(@"C:\Users\sipic\Source\Repos\ReadFromTextFile\textFileStreamWriter.txt"))
            {
                foreach (string ln in lines)
                {
                    if (ln.Contains("third".ToLower())) {
                        file.WriteLine(ln);
                    
                    }
                }
            }

            using (StreamWriter file = new StreamWriter(@"C:\Users\sipic\Source\Repos\ReadFromTextFile\textFileStreamWriter.txt", true)) //add line to an existing file 
            {
                file.WriteLine("Additional line");
            }

        }
    }
}
