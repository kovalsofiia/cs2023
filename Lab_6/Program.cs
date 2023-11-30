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
            Console.WriteLine("Enter faculty to search: ");
            string fac = Console.ReadLine();
            // a) Simple select query
            string selectQueryA = $"SELECT * FROM students WHERE Faculty = '{fac}'";



            Console.WriteLine("Enter which column starts with what for item: ");
            string what = Console.ReadLine();
            string fullname = Console.ReadLine();
            // b) Using special functions: LIKE, IS NULL, IN, BETWEEN
            string selectQueryB1 = $"SELECT * FROM students WHERE {what} LIKE '{fullname}%'";

            Console.WriteLine("Enter what column for item is null: ");
            string smth = Console.ReadLine();
            string selectQueryB2 = $"SELECT * FROM students WHERE {smth} IS NULL";

            //Console.WriteLine("Enter what is null to search: ");
            //string smth = Console.ReadLine();
            //string selectQueryB3 = "SELECT * FROM students WHERE WorkPlace IN ('Global Innovations', 'Research Labs')";
            //string selectQueryB4 = "SELECT * FROM students WHERE BirthYear BETWEEN 2000 AND 2001";

            // c) Query with a complex criterion
            string selectQueryC = "SELECT * FROM students WHERE (GroupUni = 'Group2' AND AverageScore < 90 AND City = 'City3')";

            // d) Query with unique values
            string selectQueryD = "SELECT DISTINCT Faculty FROM students";

            // e) Query using a calculated field
            string selectQueryE = "SELECT Surname, AverageScore / 10 AS Total10Score FROM students";

            // f) Grouping query
            string selectQueryF = "SELECT GroupUni, AVG(AverageScore) AS AverageScore FROM students GROUP BY GroupUni HAVING AVG(AverageScore) > 80";

            // g) Sorting query in ascending and descending order
            string selectQueryG1 = "SELECT Surname, GroupUni, AverageScore FROM students ORDER BY AverageScore ASC";
            string selectQueryG2 = "SELECT Surname, GroupUni, AverageScore FROM students ORDER BY AverageScore DESC";

            // h) Update query
            string updateQueryH = "UPDATE students SET AverageScore = AverageScore / 10";




            ExecuteQuery(selectQueryA);
            ExecuteQuery(selectQueryB1);
            ExecuteQuery(selectQueryB2);
            //ExecuteQuery(selectQueryB3);
            //ExecuteQuery(selectQueryB4);
            //ExecuteQuery(selectQueryC);
            //ExecuteQuery(selectQueryD);
            //ExecuteQuery(selectQueryE);
            //ExecuteQuery(selectQueryF);
            //ExecuteQuery(selectQueryG1);
            //ExecuteQuery(selectQueryG2);
            //ExecuteQuery(updateQueryH);
        }
        static void Main(string[] args)
        {


            //FUNC();
            lab6();

        }
    }
}