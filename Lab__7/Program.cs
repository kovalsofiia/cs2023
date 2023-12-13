using Lab__7.Models; // Include the necessary namespaces
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Lab_7
{
    internal class Program
    {
        public static void FUNC()
        {
            using (var context = new SchoolDbContext())
            {
                var repository = new StudentsRepository(context);

                bool continueExecution = true;

                while (continueExecution)
                {
                    Console.WriteLine("Hello. Please enter numbers what you want to do: \n 1 - simple select, \n 2 - using spec func, \n 3 - complex criterion, 4 - unique values, \n 5 - calculated field, 6 - grouping query, \n 7 - sorting, \n 8 - update, \n 0 - exit");

                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("A. Simple select query: ");
                            Console.WriteLine("Column name: ");
                            string Acolumn = Console.ReadLine();
                            Console.WriteLine("Column value: ");
                            string Avalue = Console.ReadLine();
                            repository.ExecuteQuery(Acolumn, Avalue);
                            break;

                        case 2:
                            Console.WriteLine("B. Using special functions: LIKE, IS NULL, IN, BETWEEN: ");
                            Console.WriteLine("B1.(LIKE) Column name: ");
                            string BColumn = Console.ReadLine();
                            Console.WriteLine("B1.(LIKE) Column value: ");
                            string Bvalue = Console.ReadLine();
                            repository.ExecuteSpecialQuery(BColumn, Bvalue);

                            Console.WriteLine("B2.(IS NULL) Column name: ");
                            string B2Column = Console.ReadLine();
                            repository.ExecuteIsNullQuery(B2Column);

                            Console.WriteLine("B3.(IN)");
                            repository.ExecuteInQuery(new List<string> { "Global Innovations", "Research Labs" });

                            Console.WriteLine("B4.(BETWEEN)");
                            repository.ExecuteBetweenQuery(2000, 2001);
                            break;

                        case 3:
                            Console.WriteLine("C. Query with a complex criterion: ");
                            repository.ExecuteComplexCriterionQuery();
                            break;

                        case 4:
                            Console.WriteLine("D. Unique values");
                            Console.WriteLine("Column name: ");
                            string DColumn = Console.ReadLine();
                            repository.ExecuteDistinctQuery(DColumn);
                            break;

                        case 5:
                            Console.WriteLine("E. Query using a calculated field: ");
                            repository.ExecuteCalculatedFieldQuery();
                            break;

                        case 6:
                            Console.WriteLine("F. Grouping query: ");
                            repository.ExecuteGroupingQuery();
                            break;

                        case 7:
                            Console.WriteLine("G. Sorting query: ");
                            Console.WriteLine("Sort asc by: ");
                            Console.WriteLine("Column name: ");
                            string GColumnAsc = Console.ReadLine();
                            repository.ExecuteSortingQuery(GColumnAsc, ascending: true);

                            Console.WriteLine("Sort desc by: ");
                            Console.WriteLine("Column name: ");
                            string GColumnDesc = Console.ReadLine();
                            repository.ExecuteSortingQuery(GColumnDesc, ascending: false);
                            break;

                        case 8:
                            Console.WriteLine("H. Update query: ");
                            repository.ExecuteUpdateQuery();
                            break;

                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                    // Check if the user wants to continue
                    if (continueExecution)
                    {
                        Console.WriteLine("Do you want to continue? (y/n)");
                        string continueChoice = Console.ReadLine().ToLower();

                        if (continueChoice != "y")
                        {
                            continueExecution = false;
                        }
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            FUNC();
        }
    }
}
