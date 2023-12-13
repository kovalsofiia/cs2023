using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Models
{
    internal class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public string GroupUni { get; set; }
        public string Faculty { get; set; }
        public double AverageScore { get; set; }
        public string WorkPlace { get; set; }
        public string City { get; set; }
    }
}
