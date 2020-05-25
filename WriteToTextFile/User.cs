using System;
using System.Collections.Generic;
using System.Text;

namespace WriteTotextFile
{
    class User
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public User(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public override string ToString()
        {
            return $"The user: {this.Name} have {this.Score} points";
        }
    }
}
