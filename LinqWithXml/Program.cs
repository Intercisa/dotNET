using System;
using System.Xml.Linq;
using System.Linq;

namespace LinqWithXml
{
    class Program
    {
        static void Main(string[] args)
        {
            string studentsYML =
                @"<Students>
                    <Student>
                        <Id>1001</Id>
                        <Name>Toni</Name>
                        <Age>21</Age>
                        <University>Yale</University>
                        <Gender>Male</Gender>
                        <Country>Germany</Country> 
                    </Student>
                      <Student>
                        <Id>1002</Id>
                        <Name>Carla</Name>
                        <Age>17</Age>
                        <University>Yale</University>
                        <Gender>Female</Gender>
                        <Country>England</Country> 
                    </Student>
                      <Student>
                        <Id>1003</Id>
                        <Name>Leyla</Name>
                        <Age>19</Age>
                        <University>Bejing Tech</University>
                        <Gender>Female</Gender>
                        <Country>Austria</Country> 
                    </Student>
                     <Student>
                        <Id>1004</Id>
                        <Name>Mark</Name>
                        <Age>31</Age>
                        <University>Bejing Tech</University>
                        <Gender>Male</Gender>
                        <Country>Hungary</Country> 
                    </Student>  
                    <Student>
                        <Id>1005</Id>
                        <Name>Brian</Name>
                        <Age>28</Age>
                        <University>Yale</University>
                        <Gender>Male</Gender>
                        <Country>USA</Country> 
                    </Student>
                    <Student>
                        <Id>1006</Id>
                        <Name>Lilly</Name>
                        <Age>22</Age>
                        <University>Bejing Tech</University>
                        <Gender>Female</Gender>
                        <Country>Sweden</Country> 
                    </Student>
                    </Students>
                ";

  

            XDocument studentsXdoc = new XDocument();
            studentsXdoc = XDocument.Parse(studentsYML);

            var students = from student in studentsXdoc.Descendants("Student")
                           select new
                           {
                               Id = student.Element("Id").Value,
                               Name = student.Element("Name").Value,
                               Age = student.Element("Age").Value,
                               University = student.Element("University").Value,
                               Gender = student.Element("Gender").Value,
                               Country = student.Element("Country").Value

                           };

            foreach (var student in students) Console.WriteLine($"Student name: " +
                $"{student.Name} " +
                $"(${student.Gender}) " +
                $"with the age of {student.Age} " +
                $"from {student.Country} " +
                $"is a student of {student.University} University " +
                $"with the Id of {student.Id}");


            Console.WriteLine("SORTED:");
            var sortedStudents = from student in students orderby student.Age select student;
            foreach (var student in sortedStudents) Console.WriteLine($"Student name: " +
               $"{student.Name} " +
               $"(${student.Gender}) " +
               $"with the age of {student.Age} " +
               $"from {student.Country} " +
               $"is a student of {student.University} University " +
               $"with the Id of {student.Id}");



        }
    }
}
