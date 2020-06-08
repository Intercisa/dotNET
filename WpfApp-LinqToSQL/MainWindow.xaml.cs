using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Configuration;

namespace WpfApp_LinqToSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        LonqToSqlDataClassesDataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();
            //ExampleDB1ConnectionString
            // - is going to be _
            string connectionString = ConfigurationManager.ConnectionStrings["WpfApp_LinqToSQL.Properties.Settings.ExampleDB1ConnectionString"].ConnectionString;
            dataContext = new LonqToSqlDataClassesDataContext(connectionString);

            //InsertUniversities();
            //InsertStudents();
            //InsertLectures();
            //InsertStudentLectureAssociations();
            //GetUniversityOfMark();
            //GetLecturesOfMark();
            //GetAllStudentsFromYale();
            //GetAllUniversitiesWithFeMales();
            //UpdateMark();
            DeleteKarla();

        }

        public void InsertUniversities()
        {

            dataContext.ExecuteCommand("delete from University");

            University yale = new University();
            yale.Name = "Yale";
            dataContext.Universities.InsertOnSubmit(yale);

            University harvard = new University();
            harvard.Name = "Harvard";
            dataContext.Universities.InsertOnSubmit(harvard);

            University mit = new University();
            mit.Name = "MIT";
            dataContext.Universities.InsertOnSubmit(mit);

            University oxford = new University();
            oxford.Name = "Oxford";
            dataContext.Universities.InsertOnSubmit(oxford);

            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Universities;

        }

        public void InsertStudents()
        {

            dataContext.ExecuteCommand("delete from Student");

            University yale = dataContext
                .Universities
                .First(uni => uni.Name.Equals("Yale"));

            University harvard = dataContext
                .Universities
                .First(uni => uni.Name.Equals("Harvard"));

           University mit = dataContext
                .Universities
                .First(uni => uni.Name.Equals("MIT"));

            University oxford = dataContext
                .Universities
                .First(uni => uni.Name.Equals("Oxford"));

            List<Student> students = new List<Student>();

            students.Add(new Student { Name = "Karla", Gender = "female", UniversityId = yale.Id });
            students.Add(new Student { Name = "Mark", Gender = "male", University = harvard });
            students.Add(new Student { Name = "Jon", Gender = "male", University = mit });
            students.Add(new Student { Name = "Peter", Gender = "male", University = oxford });
            students.Add(new Student { Name = "David", Gender = "male", University = yale });
            students.Add(new Student { Name = "Jane", Gender = "female", University = yale });
            students.Add(new Student { Name = "Steven", Gender = "male", University = harvard });
            students.Add(new Student { Name = "Brie", Gender = "female", University = mit });


            dataContext.Students.InsertAllOnSubmit(students);
            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Students;
        }

        public void InsertLectures() {
            
            dataContext.ExecuteCommand("delete from Lecture");

            dataContext.Lectures.InsertOnSubmit(new Lecture { Name = "Math" });
            dataContext.Lectures.InsertOnSubmit(new Lecture { Name = "History" });
            dataContext.Lectures.InsertOnSubmit(new Lecture { Name = "English" });

            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Lectures;
        }

        public void InsertStudentLectureAssociations() {

            dataContext.ExecuteCommand("delete from StudentLecture");

            Student karla = dataContext.Students.First(s => s.Name.Equals("Karla"));
            Student mark = dataContext.Students.First(s => s.Name.Equals("Mark"));
            Student jon = dataContext.Students.First(s => s.Name.Equals("Jon"));
            Student peter = dataContext.Students.First(s => s.Name.Equals("Peter"));
            Student david = dataContext.Students.First(s => s.Name.Equals("David"));
            Student jane = dataContext.Students.First(s => s.Name.Equals("Jane"));
            Student steven = dataContext.Students.First(s => s.Name.Equals("Steven"));
            Student brie = dataContext.Students.First(s => s.Name.Equals("Brie"));

            Lecture math = dataContext.Lectures.First(l => l.Name.Equals("Math"));
            Lecture history = dataContext.Lectures.First(l => l.Name.Equals("History"));
            Lecture english = dataContext.Lectures.First(l => l.Name.Equals("English"));


            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture {Student = karla, Lecture = math});
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = mark, Lecture = math });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = jon, Lecture = math });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = karla, Lecture = history });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = jane, Lecture = history });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = brie, Lecture = math });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = peter, Lecture = english });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = david, Lecture = math });

            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.StudentLectures;

        }

        public void GetUniversityOfMark() {
            Student mark = dataContext.Students.First(t => t.Name.Equals("Mark"));
            University marksUni = mark.University;

            List<University> universities = new List<University>();
            universities.Add(marksUni);
            MainDataGrid.ItemsSource = universities;
        }

        public void GetLecturesOfMark()
        {
            Student mark = dataContext.Students.First(t => t.Name.Equals("Mark"));
            var marLects = from lect in dataContext.StudentLectures where lect.StudentId == mark.Id select lect;
            
            var marLectsTwo = from sl in mark.StudentLectures select sl.Lecture;

            MainDataGrid.ItemsSource = marLectsTwo;
        }


        public void GetAllStudentsFromYale()
        {
            var studentsFromYale = from student in dataContext.Students
                                   where student.University.Name == "Yale"
                                   select student;

            MainDataGrid.ItemsSource = studentsFromYale;

        }

        public void GetAllUniversitiesWithFeMales() {

            var unisWithFemales = from student in dataContext.Students
                                  join uni in dataContext.Universities
                                  on student.University equals uni
                                  where student.Gender == "female"
                                  select uni;
            
            MainDataGrid.ItemsSource = unisWithFemales;

        }

        public void GetAllLecturesFromMIT() {
            var lecturesFromMIT = from sl in dataContext.StudentLectures
                                  join student in dataContext.Students on sl.Id equals student.Id
                                  where student.University.Name == "MIT"
                                  select sl.Lecture;

            MainDataGrid.ItemsSource = lecturesFromMIT;
        }

        public void UpdateMark() {
            Student mark = dataContext.Students.FirstOrDefault(s => s.Name.Equals("Mark"));
            mark.Name = "Marko";

            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Students;

        }

        public void DeleteKarla() {
            Student karla = dataContext.Students.FirstOrDefault(s => s.Name.Equals("Karla"));
            dataContext.Students.DeleteOnSubmit(karla);

            dataContext.SubmitChanges();

            //in case of error: 
            string connectionString = ConfigurationManager.ConnectionStrings["WpfApp_LinqToSQL.Properties.Settings.ExampleDB1ConnectionString"].ConnectionString;
            LonqToSqlDataClassesDataContext db = new LonqToSqlDataClassesDataContext(connectionString);


            MainDataGrid.ItemsSource = dataContext.Students;
        }

    }
}
