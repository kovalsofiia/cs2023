using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr___2
{
    //    Варіант 17.
    //Створити таблицю БД “Zavod”, що містить інформацію про дані з полями; номер цеху; посада; стаж роботи;
    //    заробітна плата.
    //     Вивести дані у консоль;
    // Вивести номер цеху, посаду, стаж роботи та заробітну плату за заданим прізвищем.
    // Обчислити середню заробітну плату в цеху Х.
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Documents\uzhnu 3 semester\C#\repos\cs2023\mkr___2\Zavod.mdf"";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var rep = new ZavodRepository(connection);

                while (true)
                {
                Start: Console.WriteLine("Table 'Zavod' in the database containing information with fields: department number; position; work experience; salary.");
                    Console.WriteLine("1 - Display data in the console.");
                    Console.WriteLine("2 - Display department number, position, work experience, and salary for a given surname.");
                    Console.WriteLine("3 - Calculate the average salary in department X.");
                    Console.WriteLine("0 - exit");
                    Console.WriteLine("=================");
                    int request = Convert.ToInt32(Console.ReadLine());
                    List<Zavod> zavod = rep.GetAll();
                    if (request == 1)
                    {
                        zavod = rep.GetAll();
                    }
                    else if (request == 2)
                    {
                        Console.Write("Surname: ");
                        zavod = rep.GetBySurname(Console.ReadLine());
                    }
                    else if (request == 3)
                    {
                        Console.Write("Cech number (from 1 to 3): ");
                        int averageSalary = rep.GetAverageSalary(int.Parse(Console.ReadLine()));
                        Console.WriteLine($"{averageSalary}");
                        goto Start;
                    }
                    else if (request == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Non exist");
                        goto End;
                    }

                    foreach (var zav in zavod)
                    {
                        Console.WriteLine($"Surname: {zav.Surname}, CechNumber: {zav.CechNumber}, Position: {zav.Position}, Experience: {zav.Experience}, Salary: {zav.Salary}");
                    }
                End: Console.ReadLine();
                    goto Start;
                }
            }
        }
    }
}
