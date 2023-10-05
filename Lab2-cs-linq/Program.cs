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
        //b)+++ вивести назви груп, не більше двох різних студентів яких отримали двійки на іспиті у поточному році.
        //c)+++ вивести прізвища студентів та їх підсумковий рейтинг(середній бал за всіма предметами), впорядкувавши запису по рейтингу у порядку спадання.




        static void Main(string[] args)
        {
            PrintStudents();
            TaskA();
            TaskB();
            TaskC();
        }


        public static void TaskA()
        {
            Console.WriteLine("================================================================================");
            Console.Write("Write a first letter for selection of student`s surnames : ");
            var startingLetter = Console.ReadLine();
            var selectedStudents = student_list
                .Where(student => student.Surname.StartsWith(startingLetter, StringComparison.OrdinalIgnoreCase))
                .Select(student => student.Id);

            foreach (var code in selectedStudents)
            {
                Console.WriteLine($"Student Id: {code}");
            }
            Console.WriteLine();
        }
        public static void TaskB()
        {
            Console.WriteLine("================================================================================");
            var mergedStudentSessionData = from student in student_list
                                           join session in session
                                           on student.Id equals session.StudentId
                                           where session.ControlDate.Year == DateTime.Now.Year && session.TypeOfControl == "Test" && session.SubjectsScores.Min() < 60
                                           select new
                                           {
                                               student.Group,
                                               student.Id
                                           };

            var groupsWithTwoFailuresOrLess = mergedStudentSessionData
                .GroupBy(data => data.Group)
                .Where(group => group.Select(st => st.Id).Distinct().Count() <= 2)
                .Select(group => group.Key)
                .ToList();

            Console.WriteLine("Groups where students got an F): ");
            foreach (var groupName in groupsWithTwoFailuresOrLess)
            {
                Console.WriteLine(groupName);
            }
            Console.WriteLine();
        }
        public static void TaskC()
        {
            Console.WriteLine("================================================================================");
            var mergedStudentSessionData = from student in student_list
                                           join session in session
                                           on student.Id equals session.StudentId
                                           select new
                                           {
                                               student.Surname,
                                               session.SubjectsScores,
                                           };

            var studentRating = mergedStudentSessionData
                .Select(data => new
                {
                    data.Surname,
                    AverageScore = data.SubjectsScores.Average()
                })
                .OrderByDescending(student => student.AverageScore)
                .ToList();

            
            Console.WriteLine("Rating at the end of semester:");
            foreach (var student in studentRating)
            {
                Console.WriteLine($"Surname: {student.Surname}, Average score: {student.AverageScore:F2}");
            }
            Console.WriteLine();
        }
        public static void PrintStudents()
        {
            var mergedStudentSessionData = from student in student_list
                                           join session in session
                                           on student.Id equals session.StudentId
                                           select new
                                           {
                                               student.Id,
                                               student.Surname,
                                               student.Group,
                                               session.SubjectsScores,
                                               session.TypeOfControl,
                                               session.ControlDate,
                                           };

            foreach (var student in student_list)
            {
                Console.WriteLine(student.ToString());
            }
            Console.WriteLine();

            //foreach (var st in mergedStudentSessionData)
            //{
            //    Console.WriteLine($"Id {st.Id}: Student {st.Surname}, who belongs to group {st.Group}, has average score: {st.SubjectsScores.Average():F2}\n");
            //}

        }

        public static List<Session> session = new List<Session>()
        {
            new Session()
            {
                    StudentId = 1,
                    SubjectsScores = new List<double>
                    {
                        33,65,60
                    },
                    TypeOfControl = "Test",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 2,
                    SubjectsScores = new List<double>
                    {
                        72,80,65
                    },
                    TypeOfControl = "Test",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 3,
                    SubjectsScores = new List<double>
                    {
                        85,90,88
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 4,
                    SubjectsScores = new List<double>
                    {
                        100,100,100
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 5,
                    SubjectsScores = new List<double>
                    {
                        75,64,79
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 6,
                    SubjectsScores = new List<double>
                    {
                        86,94,98
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 7,
                    SubjectsScores = new List<double>
                    {
                        65,78,88
                    },
                    TypeOfControl = "Test",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 8,
                    SubjectsScores = new List<double>
                    {
                        91,90,98
                    },
                    TypeOfControl = "Test",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 9,
                    SubjectsScores = new List<double>
                    {
                        93,100,88
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 10,
                    SubjectsScores = new List<double>
                    {
                        92,65,87
                    },
                    TypeOfControl = "Test",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 11,
                    SubjectsScores = new List<double>
                    {
                        81,70,68
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 12,
                    SubjectsScores = new List<double>
                    {
                        93,100,60
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 13,
                    SubjectsScores = new List<double>
                    {
                        87,50,65
                    },
                    TypeOfControl = "Test",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 14,
                    SubjectsScores = new List<double>
                    {
                        81,90,76
                    },
                    TypeOfControl = "Test",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 15,
                    SubjectsScores = new List<double>
                    {
                        61,69,73
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 16,
                    SubjectsScores = new List<double>
                    {
                        92,93,98
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 17,
                    SubjectsScores = new List<double>
                    {
                        91,99,98
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 18,
                    SubjectsScores = new List<double>
                    {
                        100,100,98
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 19,
                    SubjectsScores = new List<double>
                    {
                        93,90,87
                    },
                    TypeOfControl = "Exam",
                    ControlDate = DateTime.Now,
            },
            new Session()
            {
                    StudentId = 20,
                    SubjectsScores = new List<double>
                    {
                        56,50,63
                    },
                    TypeOfControl = "Test",
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
                Group = "Computer Science"
            },
        };










    }
}
