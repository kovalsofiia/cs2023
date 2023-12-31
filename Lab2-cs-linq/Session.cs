﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_cs_linq
{
    internal class Session
    {
        //            Кожен елемент колекції Сесія містить:
        //            код студента,
        //            назву предмета,
        //            форму контролю(залік або іспит),
        //            підсумковий бал,
        //            дату складання.

        public int StudentId { get; set; }
        public List<double> SubjectsScores { get; set; }
        public string TypeOfControl { get; set; }
        public DateTime ControlDate { get; set; }
        public double average {  get; set; }
    }
}
