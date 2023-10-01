using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_cs_linq
{
    internal class Student
    {
        //            Елементи колекції Студенти містять наступну інформацію про студентів:
        //            код,
        //            прізвище,
        //            група.

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }

        public override string ToString()
        {
            string result = $"Id {Id}: Student {Surname} belongs to the group of {Group}";
            return result;
        }
    }
}
