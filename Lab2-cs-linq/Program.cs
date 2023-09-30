using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        //a)+++ вивести коди студентів, прізвища яких починаються на задану літеру.
        //b) вивести назви груп, не більше двох різних студентів яких отримали двійки на іспиті у поточному році.
        //c) вивести прізвища студентів та їх підсумковий рейтинг(середній бал за всіма предметами), впорядкувавши запису по рейтингу у порядку спадання.



        //string startingLetter = "D";

        //// Використовуємо LINQ для вибору студентів з відповідними прізвищами
        //var selectedStudents = students
        //    .Where(student => student.Surname.StartsWith(startingLetter, StringComparison.OrdinalIgnoreCase))
        //    .Select(student => student.Id);

        //    // Виводимо коди студентів, що відповідають умові
        //    foreach (var code in selectedStudents)
        //    {
        //        Console.WriteLine("Student Code: " + code);
        //    }
    static void Main(string[] args)
        {
        }
    }
}
