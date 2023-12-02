using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Lab7.Models;

namespace Lab7
{
    internal class Program
    {
        static void PrintResult<T>(IEnumerable<T> items)
        {
            Console.WriteLine("Query Result:");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString()); // Override ToString() method in your entities or use specific properties.
            }
        }

        public static void FUNC()
        {
            using (var context = new StudentDbContext())
            {
                Console.Write("Student's surname: ");
                string Surname = Console.ReadLine();

                Console.Write("Birth year: ");
                if (!int.TryParse(Console.ReadLine(), out int BirthYear) || BirthYear <= 0)
                {
                    Console.WriteLine("Invalid input for Birth year. Please enter a valid positive integer.");
                    return;
                }

                Console.Write("GroupUni: ");
                string GroupUni = Console.ReadLine();

                Console.Write("Faculty: ");
                string Faculty = Console.ReadLine();

                Console.Write("AverageScore: ");
                if (!float.TryParse(Console.ReadLine(), out float AverageScore) || AverageScore < 0 || AverageScore > 100)
                {
                    Console.WriteLine("Invalid input for AverageScore. Please enter a valid float between 0 and 100.");
                    return;
                }

                Console.Write("WorkPlace: ");
                string WorkPlace = Console.ReadLine();

                Console.Write("City: ");
                string City = Console.ReadLine();

                var newStudent = new Student
                {
                    Surname = Surname,
                    BirthYear = BirthYear,
                    GroupUni = GroupUni,
                    Faculty = Faculty,
                    AverageScore = AverageScore,
                    WorkPlace = WorkPlace,
                    City = City
                };

                context.Students.Add(newStudent);
                context.SaveChanges();

                Console.WriteLine("Added student's data successfully!");
            }
        }

        static void ExecuteQuery(StudentDbContext context, string query)
        {
            var students = context.Students.FromSqlRaw(query).ToList();

            Console.WriteLine("Query Result:");
            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, GroupUni: {student.GroupUni}, Faculty: {student.Faculty}, AverageScore: {student.AverageScore}, WorkPlace: {student.WorkPlace}, City: {student.City}");
            }
        }


        public static void lab7()
        {
            using (var context = new StudentDbContext())
            {
                Console.WriteLine("Hello. Please enter numbers what you want to do: \n 1 - simple select, \n 2 - using spec func, \n 3 - complex criterion, 4 - unique values, \n 5 - calculated field, 6 - grouping query, \n 7 - sorting, \n 8 - update");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("A. Simple select query: ");
                        Console.WriteLine("Column name: ");
                        string Acolumn = Console.ReadLine();
                        Console.WriteLine("Column value: ");
                        string Avalue = Console.ReadLine();
                        // a) Simple select query
                        var studentsA = context.Students.Where(s => EF.Property<string>(s, Acolumn) == Avalue).ToList();
                        PrintStudents(studentsA);
                        break;
                    case 2:
                        Console.WriteLine("B. Using special functions: LIKE, IS NULL, IN, BETWEEN: ");
                        Console.WriteLine("B1.(LIKE) Column name: ");
                        string BColumn = Console.ReadLine();
                        Console.WriteLine("B1.(LIKE) Column value: ");
                        string Bvalue = Console.ReadLine();
                        // b) Using special functions: LIKE, IS NULL, IN, BETWEEN
                        var studentsB1 = context.Students.Where(s => EF.Property<string>(s, BColumn).StartsWith(Bvalue)).ToList();

                        Console.WriteLine("B2.(IS NULL) Column name: ");
                        string B2Column = Console.ReadLine();
                        var studentsB2 = context.Students.Where(s => EF.Property<string>(s, B2Column) == null).ToList();

                        Console.WriteLine("B3.(IN)");
                        var studentsB3 = context.Students.Where(s => s.WorkPlace == "Global Innovations" || s.WorkPlace == "Research Labs").ToList();

                        Console.WriteLine("B4.(BETWEEN)");
                        var studentsB4 = context.Students.Where(s => s.BirthYear >= 2000 && s.BirthYear <= 2001).ToList();

                        PrintStudents(studentsB1);
                        PrintStudents(studentsB2);
                        PrintStudents(studentsB3);
                        PrintStudents(studentsB4);
                        break;
                    case 3:
                        // c) Query with a complex criterion
                        var studentsC = context.Students.Where(s => s.GroupUni == "Group2" && s.AverageScore < 90 && s.City == "City3").ToList();
                        PrintStudents(studentsC);
                        break;
                    case 4:
                        Console.WriteLine("D. Unique values");
                        Console.WriteLine("Column name: ");
                        string DColumn = Console.ReadLine();
                        // d) Query with unique values
                        var distinctValues = context.Students.Select(s => EF.Property<string>(s, DColumn)).Distinct().ToList();
                        Console.WriteLine($"Distinct {DColumn} values:");
                        foreach (var value in distinctValues)
                        {
                            Console.WriteLine(value);
                        }
                        break;
                    case 5:
                        // e) Query using a calculated field
                        var studentsE = context.Students.Select(s => new
                        {
                            s.Surname,
                            Total10Score = s.AverageScore / 10
                        }).ToList();
                        Console.WriteLine("Calculated field (Total10Score):");
                        foreach (var student in studentsE)
                        {
                            Console.WriteLine($"Surname: {student.Surname}, Total10Score: {student.Total10Score}");
                        }
                        break;
                    case 6:
                        // f) Grouping query
                        var groupedStudents = context.Students.GroupBy(s => s.GroupUni)
                                                              .Where(g => g.Average(s => s.AverageScore) > 80)
                                                              .Select(g => new
                                                              {
                                                                  GroupUni = g.Key,
                                                                  AverageScore = g.Average(s => s.AverageScore)
                                                              }).ToList();

                        Console.WriteLine("Grouping query (AverageScore > 80):");
                        foreach (var group in groupedStudents)
                        {
                            Console.WriteLine($"GroupUni: {group.GroupUni}, AverageScore: {group.AverageScore}");
                        }
                        break;
                    case 7:
                        Console.WriteLine("Sort asc by: ");
                        Console.WriteLine("Column name: ");
                        string GColumn = Console.ReadLine();
                        // g) Sorting query in ascending and descending order
                        var studentsG1 = context.Students.OrderBy(s => EF.Property<object>(s, GColumn)).ToList();
                        PrintStudents(studentsG1);

                        Console.WriteLine("Sort desc by: ");
                        Console.WriteLine("Column name: ");
                        string G2Column = Console.ReadLine();
                        var studentsG2 = context.Students.OrderByDescending(s => EF.Property<object>(s, G2Column)).ToList();
                        PrintStudents(studentsG2);
                        break;
                    case 8:
                        // h) Update query
                        var studentsToUpdate = context.Students.ToList();
                        foreach (var student in studentsToUpdate)
                        {
                            student.AverageScore /= 10;
                        }
                        context.SaveChanges();
                        Console.WriteLine("Updated AverageScore for all students.");
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }


        static void PrintStudents(IEnumerable<Student> students)
        {
            Console.WriteLine("Query Result:");
            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, GroupUni: {student.GroupUni}, Faculty: {student.Faculty}, AverageScore: {student.AverageScore}, WorkPlace: {student.WorkPlace}, City: {student.City}");
            }
        }

        static void Main(string[] args)
        {
            lab7();
        }
    }
}
