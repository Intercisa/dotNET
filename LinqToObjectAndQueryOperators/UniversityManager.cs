using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqToObjectAndQueryOperators
{
    class UniversityManager
    {

        public List<University> universities;
        public List<Student> students;

        public UniversityManager(List<University> universities, List<Student> students)
        {
            this.universities = universities;
            this.students = students;
        }


       public void StudentsAndUniversitiesNameCollection()
        {
            var newCollection = from student in students
                                join university in universities on student.UniversityId equals university.Id
                                orderby student.Name
                                select new { StudentName = student.Name, UniversityName = university.Name };

            Console.WriteLine("Students - University");
            foreach (var elem in newCollection) Console.WriteLine($"Student {elem.StudentName} from University {elem.UniversityName}");

            Console.WriteLine("---------------------------------");

        }



        public void AllStudentsFromBejingTech() {

            var bjtStudents = from student in students
                              join university in universities on student.UniversityId equals university.Id
                              where university.Name == "Bejing Tech"
                              select student;
            Console.WriteLine("Bejing Tech - Students");
            foreach (Student student in bjtStudents) student.Print();

            Console.WriteLine("---------------------------------");

        }   
        
        public void AllStudentsFromSpecificuniversity(int id) {

            var bjtStudents = from student in students
                              join university in universities on student.UniversityId equals university.Id
                              where university.Id == id
                              select student;
            Console.WriteLine($"University with id: {id} - Students");
            foreach (Student student in bjtStudents) student.Print();

            Console.WriteLine("---------------------------------");

        }

        public void SortStudentsByAge()
        {

            var sortedStudents = from student in students orderby student.Age select student;
            
            Console.WriteLine("Sorted - Students by Age:");
            foreach (Student student in sortedStudents) student.Print();

            Console.WriteLine("---------------------------------");

        }



        public void MaleStudents()
        {
            IEnumerable<Student> maleStudents = from student in students where student.Gender == "male" select student;
            Console.WriteLine("Male - Students:");

            foreach (Student student in maleStudents) student.Print();

            Console.WriteLine("---------------------------------");

        } 
        
        public void FemaleStudents()
        {
            IEnumerable<Student> femaleStudents = from student in students where student.Gender == "female" select student;
            Console.WriteLine("Female - Students:");

            foreach (Student student in femaleStudents) student.Print();

            Console.WriteLine("---------------------------------");

        }
         public void LmbtqStudents()
        {
            IEnumerable<Student> lmbtqStudents = from student in students where student.Gender != "female" && student.Gender != "male" select student;
            Console.WriteLine("Lmbtq - Students:");

            foreach (Student student in lmbtqStudents) student.Print();

            Console.WriteLine("---------------------------------");

        }
              
        public void PrintStudentsBasedOnGender(string gender)
        {
            IEnumerable<Student> studentList = from student in students where student.Gender == gender select student;
            Console.WriteLine($"{gender} - Students:");

            foreach (Student student in studentList) student.Print();

            Console.WriteLine("---------------------------------");

        }

        
    }
}
