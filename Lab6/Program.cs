using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class Program
    {
        //        Використовуючи SqlCommand підготувати програмну оболонку для виконання завдань лабораторної роботи 5. 
        //Забезпечити користувачу можливість ввести значення параметрів запиту.
        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
            }
            Console.Read();
        }
    }
}