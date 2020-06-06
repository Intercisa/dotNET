using System;
using System.Collections.Generic;
using System.Text;

namespace LinqToObjectAndQueryOperators
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        // Foreign Key
        public int UniversityId { get; set; }

        public void Print() {
            Console.WriteLine($"Student {this.Name} with id {this.Id}, Gender {this.Gender} and Age {this.Age} from University with the Id {this.UniversityId}");
        }
    }
}
