using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToObjectAndQueryOperators
{
    class Program
    {
        private static List<University> universities = new List<University>();
        private static List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            UniversityDataFillHelper();
            UniversityManager universityManager = new UniversityManager(universities, students);
            universityManager.MaleStudents();
            universityManager.FemaleStudents();
            universityManager.LmbtqStudents();
            universityManager.PrintStudentsBasedOnGender("female");
 
            universityManager.SortStudentsByAge();

            universityManager.AllStudentsFromBejingTech();
            universityManager.AllStudentsFromSpecificuniversity(1);

            /*
            Console.WriteLine("(INPUT) University Id: ");
            var input = Console.ReadLine();
            int inputNum;

            if (int.TryParse(input, out inputNum) && (inputNum == 1 || inputNum == 2))
                universityManager.AllStudentsFromSpecificuniversity(inputNum);
            else 
                Console.WriteLine("Wrong input. Try again!");

            */

            int[] someInts = { 30, 12, 4, 3, 12 };
            IEnumerable<int> sortedInts = from i in someInts orderby i select i;
            IEnumerable<int> reversedInt = sortedInts.Reverse();

            foreach (int i in reversedInt) Console.WriteLine(i);

            IEnumerable<int> reversedSortedInts = from i in someInts
                                                  orderby i descending
                                                  select i;

            foreach (int i in reversedSortedInts) Console.WriteLine(i);
            Console.WriteLine("----------------------------------------");

            universityManager.StudentsAndUniversitiesNameCollection();


        }

        


        private static void UniversityDataFillHelper() {
            universities.Add(new University { Id = 1, Name = "Yale" });
            universities.Add(new University { Id = 2, Name = "Bejing Tech" });

            students.Add(new Student { Id = 1, Name = "Carla", Gender = "female", Age = 17, UniversityId = 1 });
            students.Add(new Student { Id = 2, Name = "Toni", Gender = "male", Age = 21, UniversityId = 1 });
            students.Add(new Student { Id = 3, Name = "Frank", Gender = "male", Age = 23, UniversityId = 2 });
            students.Add(new Student { Id = 4, Name = "Leyle", Gender = "female", Age = 19, UniversityId = 2 });
            students.Add(new Student { Id = 5, Name = "James", Gender = "trans-gender", Age = 25, UniversityId = 2 });
            students.Add(new Student { Id = 6, Name = "Linda", Gender = "female", Age = 22, UniversityId = 2 });
        }
    }
}
