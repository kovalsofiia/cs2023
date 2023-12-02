using Lab_6.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    internal class Program
    {
        const string connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Documents\uzhnu 3 semester\C#\repos\cs2023\Lab5\SA2_Koval_Sofiia.mdf"";Integrated Security=True;Connect Timeout=30";
        public static void FUNC()
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();

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

                string sqlQuery = "INSERT INTO students (Surname, BirthYear, GroupUni, Faculty, AverageScore, WorkPlace, City) VALUES (@Surname, @BirthYear, @GroupUni, @Faculty, @AverageScore, @WorkPlace, @City)";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, connection);

                sqlCommand.Parameters.AddWithValue("@Surname", Surname);
                sqlCommand.Parameters.AddWithValue("@BirthYear", BirthYear);
                sqlCommand.Parameters.AddWithValue("@GroupUni", GroupUni);
                sqlCommand.Parameters.AddWithValue("@Faculty", Faculty);
                sqlCommand.Parameters.AddWithValue("@AverageScore", AverageScore);
                sqlCommand.Parameters.AddWithValue("@WorkPlace", WorkPlace);
                sqlCommand.Parameters.AddWithValue("@City", City);

                sqlCommand.ExecuteNonQuery();

                Console.WriteLine("Added student's data successfully!");

            }
        }


        static void ExecuteQuery(string query)
        {

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("Query Result:");
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetName(i)}: {reader[i]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        public static void lab6()
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
                    string selectQueryA = $"SELECT * FROM students WHERE {Acolumn} = '{Avalue}'";
                    ExecuteQuery(selectQueryA);
                    break;
                case 2:

                    Console.WriteLine("B. Using special functions: LIKE, IS NULL, IN, BETWEEN: ");
                    Console.WriteLine("B1.(LIKE) Column name: ");
                    string BColumn = Console.ReadLine();
                    Console.WriteLine("B1.(LIKE) Column value: ");
                    string Bvalue = Console.ReadLine();
                    // b) Using special functions: LIKE, IS NULL, IN, BETWEEN
                    string selectQueryB1 = $"SELECT * FROM students WHERE {BColumn} LIKE '{Bvalue}%'";


                    Console.WriteLine("B2.(IS NULL) Column name: ");
                    string B2Column = Console.ReadLine();
                    string selectQueryB2 = $"SELECT * FROM students WHERE {B2Column} IS NULL";

                    Console.WriteLine("B3.(IN)");
                    string selectQueryB3 = $"SELECT * FROM students WHERE WorkPlace IN ('Global Innovations', 'Research Labs')";

                    Console.WriteLine("B4.(BETWEEN)");
                    string selectQueryB4 = $"SELECT * FROM students WHERE BirthYear BETWEEN 2000 AND 2001";

                    ExecuteQuery(selectQueryB1);
                    ExecuteQuery(selectQueryB2);
                    ExecuteQuery(selectQueryB3);
                    ExecuteQuery(selectQueryB4);
                    break;
                case 3:
                    // c) Query with a complex criterion
                    string selectQueryC = "SELECT * FROM students WHERE (GroupUni = 'Group2' AND AverageScore < 90 AND City = 'City3')";
                    ExecuteQuery(selectQueryC);
                    break;
                case 4:
                    Console.WriteLine("D. Unique values");
                    Console.WriteLine("Column name: ");
                    string DColumn = Console.ReadLine();
                    // d) Query with unique values
                    string selectQueryD = $"SELECT DISTINCT {DColumn} FROM students";
                    ExecuteQuery(selectQueryD);
                    break;
                case 5:
                    // e) Query using a calculated field
                    string selectQueryE = "SELECT Surname, AverageScore / 10 AS Total10Score FROM students";
                    ExecuteQuery(selectQueryE);
                    break;
                case 6:
                    // f) Grouping query
                    string selectQueryF = "SELECT GroupUni, AVG(AverageScore) AS AverageScore FROM students GROUP BY GroupUni HAVING AVG(AverageScore) > 80";
                    ExecuteQuery(selectQueryF);
                    break;
                case 7:
                    Console.WriteLine("Sort asc by: ");
                    Console.WriteLine("Column name: ");
                    string GColumn = Console.ReadLine();
                    // g) Sorting query in ascending and descending order
                    string selectQueryG1 = $"SELECT * FROM students ORDER BY {GColumn} ASC";
                    ExecuteQuery(selectQueryG1);
                    Console.WriteLine("Sort desc by: ");
                    Console.WriteLine("Column name: ");
                    string G2Column = Console.ReadLine();
                    string selectQueryG2 = $"SELECT * FROM students ORDER BY {G2Column} DESC";
                    ExecuteQuery(selectQueryG2);
                    break;
                case 8:
                    // h) Update query
                    string updateQueryH = "UPDATE students SET AverageScore = AverageScore / 10";
                    ExecuteQuery(updateQueryH);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        static void Main(string[] args)
        {
            lab6();
        }
    }
}