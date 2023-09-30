using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lab2_cs_linq
{
    internal class Program
    {
        //            Вимоги до роботи

        //Завдання мають бути виконані з використанням методів LINQ.
        //Цикли можна використовувати лише при виведенні результатів запитів.
        //Допускається виконання завдань лабораторної роботи у проекті консольного типу.
        //Базові типи елементів колекцій та самі колекції(списки чи масиви) мають бути описані та ініціалізовані у окремому класі.
        //Розв’язок кожного завдання має міститися у окремому методі(TaskA, TaskB, TaskC).
        //Параметри методів мають визначатися, виходячи з умови завдань. Кожна з колекцій має містити не менше 20 елементів


        //a)+++вивести коди студентів, прізвища яких починаються на задану літеру.
        //b) вивести назви груп, не більше двох різних студентів яких отримали двійки на іспиті у поточному році.
        //c)+++ вивести прізвища студентів та їх підсумковий рейтинг(середній бал за всіма предметами), впорядкувавши запису по рейтингу у порядку спадання.




        static void Main(string[] args)
        {
            PrintStudents();
            //TaskA();
            TaskB();
            //TaskC();
        }

        public static void TaskA()
        {
            Console.Write("Write a first letter for selection of student`s surnames : ");
            var startingLetter = Console.ReadLine();
            var selectedStudents = student_list
                .Where(student => student.Surname.StartsWith(startingLetter, StringComparison.OrdinalIgnoreCase))
                .Select(student => student.Id);


            foreach (var code in selectedStudents)
            {
                Console.WriteLine("Student Id: " + code);
            }
            Console.WriteLine();
        }
        public static void TaskB()
        {
            var mergedStudentSessionData = from student in student_list
                                           join session in session
                                           on student.Id equals session.StudentId
                                           select new
                                           {
                                               student.Id,
                                               student.Surname,
                                               student.Group,
                                               session.SubjectName,
                                               session.TypeOfControl,
                                               session.EndScore,
                                               session.ControlDate,
                                           };

        }
        public static void TaskC()
        {
            var mergedStudentSessionData = from student in student_list
                                           join session in session
                                           on student.Id equals session.StudentId
                                           select new
                                           {
                                               student.Surname,
                                               session.EndScore,
                                           };

            var studentRating = mergedStudentSessionData
                .GroupBy(data => data.Surname)
                .Select(group => new
                {
                    Surname = group.Key,
                    AverageScore = group.Average(data => data.EndScore),
                    
                })
                .OrderByDescending(student => student.AverageScore)
                .ToList();

            
            Console.WriteLine("Surnames and their ending scores on their chosen subjects:");

            foreach (var student in studentRating)
            {
                Console.WriteLine($"Surname: {student.Surname}, Score: {student.AverageScore:F0}");
            }
            

        }
        public static void PrintStudents()
        {
            foreach (var student in student_list)
            {
                Console.WriteLine(student.ToString());
            }
            Console.WriteLine();
        }

        public static List<Session> session = new List<Session>()
        {
            new Session()
            {
                    StudentId = 1,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 35,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 2,
                    SubjectName = "Mathematical Analysis",
                    TypeOfControl = "Test",
                    EndScore = 72,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 3,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 61,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 4,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 63,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 5,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 71,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 6,
                    SubjectName = "Mathematical Analysis",
                    TypeOfControl = "Test",
                    EndScore = 78,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 7,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 95,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 8,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 65,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 9,
                    SubjectName = "Mathematical Analysis",
                    TypeOfControl = "Test",
                    EndScore = 32,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 10,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 89,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 11,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 45,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 12,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 62,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 13,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 76,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 14,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 27,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 15,
                    SubjectName = "Mathematical Analysis",
                    TypeOfControl = "Test",
                    EndScore = 87,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 16,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 93,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 17,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 99,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 18,
                    SubjectName = "Linear Algebra",
                    TypeOfControl = "Exam",
                    EndScore = 100 ,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 19,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 93,
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 20,
                    SubjectName = "Probability & Statistics",
                    TypeOfControl = "Exam",
                    EndScore = 58,
                    ControlDate = DateTime.Now,
            }
        };
        public static List<Student> student_list = new List<Student>()
        {
            new Student()
            {
                Id = 1,
                Surname = "Mccann",
                Group = "Computer Science"
            },
            new Student()
            {
                Id= 2,
                Surname = "Moyer",
                Group = "Software Engineering"
            },
            new Student()
            {
                Id= 3,
                Surname = "Craig",
                Group = "Computer Science"
            },
            new Student()
            {
                Id= 4,
                Surname = "Walls",
                Group = "Information Technology"
            },
            new Student()
            {
                Id= 5,
                Surname = "Martin",
                Group = "Software Engineering"
            },
            new Student()
            {
                Id= 6,
                Surname = "Leach",
                Group = "Software Engineering"
            },
            new Student()
            {
                Id= 7,
                Surname = "West",
                Group = "Computer Science"
            },
            new Student()
            {
                Id= 8,
                Surname = "Manning",
                Group = "Information Technology"
            },
            new Student()
            {
                Id= 9,
                Surname = "Calderon",
                Group = "Software Engineering"
            },
            new Student()
            {
                Id= 10,
                Surname = "Bernard",
                Group = "Information Technology"
            },
            new Student()
            {
                Id= 11,
                Surname = "Gibson",
                Group = "Information Technology"
            },
            new Student()
            {
                Id= 12,
                Surname = "Dunlap",
                Group = "Computer Science"
            },
            new Student()
            {
                Id= 13,
                Surname = "Oliver",
                Group = "Software Engineering"
            },
            new Student()
            {
                Id= 14,
                Surname = "Lindsey",
                Group = "Computer Science"
            },
            new Student()
            {
                Id= 15,
                Surname = "Harrell",
                Group = "Computer Science"
            },
            new Student()
            {
                Id= 16,
                Surname = "Davidson",
                Group = "Information Technology"
            },
            new Student()
            {
                Id= 17,
                Surname = "Mclean",
                Group = "Software Engineering"
            },
            new Student()
            {
                Id= 18,
                Surname = "Goodman",
                Group = "Information Technology"
            },
            new Student()
            {
                Id= 19,
                Surname = "Li",
                Group = "Software Engineering"
            },
            new Student()
            {
                Id= 20,
                Surname = "Poole",
                Group = "Software Engineering"
            },
        };










    }
}
