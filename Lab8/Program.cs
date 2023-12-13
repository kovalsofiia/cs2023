using Lab8.Models; // Include the necessary namespaces
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Lab8
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await FUNC();
        }

        public static async Task FUNC()
        {
            using (var context = new SchoolDbContext())
            {
                var repository = new StudentsRepository(context);

                bool continueExecution = true;

                while (continueExecution)
                {
                    Console.WriteLine("Hello. Please enter numbers what you want to do: \n 1 - simple select, \n 2 - using spec func, \n 3 - complex criterion, 4 - unique values, \n 5 - calculated field, 6 - grouping query, \n 7 - sorting, \n 8 - update, \n 0 - exit");

                    if (int.TryParse(Console.ReadLine(), out int option))
                    {
                        switch (option)
                        {
                            case 1:
                                Console.WriteLine("A. Simple select query: ");
                                Console.WriteLine("Column name: ");
                                string Acolumn = Console.ReadLine();
                                Console.WriteLine("Column value: ");
                                string Avalue = Console.ReadLine();
                                await repository.ExecuteQueryAsync(Acolumn, Avalue);
                                break;

                            case 2:
                                Console.WriteLine("B. Using special functions: LIKE, IS NULL, IN, BETWEEN: ");
                                Console.WriteLine("B1.(LIKE) Column name: ");
                                string BColumn = Console.ReadLine();
                                Console.WriteLine("B1.(LIKE) Column value: ");
                                string Bvalue = Console.ReadLine();
                                await repository.ExecuteSpecialQueryAsync(BColumn, Bvalue);

                                Console.WriteLine("B2.(IS NULL) Column name: ");
                                string B2Column = Console.ReadLine();
                                await repository.ExecuteIsNullQueryAsync(B2Column);

                                Console.WriteLine("B3.(IN)");
                                await repository.ExecuteInQueryAsync(new List<string> { "Global Innovations", "Research Labs" });

                                Console.WriteLine("B4.(BETWEEN)");
                                await repository.ExecuteBetweenQueryAsync(2000, 2001);
                                break;

                            case 3:
                                Console.WriteLine("C. Query with a complex criterion: ");
                                await repository.ExecuteComplexCriterionQueryAsync();
                                break;

                            case 4:
                                Console.WriteLine("D. Unique values");
                                Console.WriteLine("Column name: ");
                                string DColumn = Console.ReadLine();
                                await repository.ExecuteDistinctQueryAsync(DColumn);
                                break;

                            case 5:
                                Console.WriteLine("E. Query using a calculated field: ");
                                await repository.ExecuteCalculatedFieldQueryAsync();
                                break;

                            case 6:
                                Console.WriteLine("F. Grouping query: ");
                                await repository.ExecuteGroupingQueryAsync();
                                break;

                            case 7:
                                Console.WriteLine("G. Sorting query: ");
                                Console.WriteLine("Sort asc by: ");
                                Console.WriteLine("Column name: ");
                                string GColumnAsc = Console.ReadLine();
                                await repository.ExecuteSortingQueryAsync(GColumnAsc, ascending: true);

                                Console.WriteLine("Sort desc by: ");
                                Console.WriteLine("Column name: ");
                                string GColumnDesc = Console.ReadLine();
                                await repository.ExecuteSortingQueryAsync(GColumnDesc, ascending: false);
                                break;

                            case 8:
                                Console.WriteLine("H. Update query: ");
                                await repository.ExecuteUpdateQueryAsync();
                                break;

                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid numeric option.");
                    }

                    // Check if the user wants to continue
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

}
