using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_C__Koval_SA2_09_2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Дано структуру даних(колекцію) відповідно до варіанта.
            //+++Додати зазначену кількість елементів, які описують відповідну предметну область.
            //+++Вивести всі елементи на консоль в прямому та зворотному порядку.
            //+++Вивести кількість елементів у колекції. 
            //+++Очистити колекцію.

            // Завдання No 1
            //12 Dictionary Назви областей України 6

            int count = 1;
            Dictionary<int, string> dictRegionsOfUkr = new Dictionary<int, string>();
            string[] array = { "Zakarpatska", "Lvivska", "Chernivetska", "Ivano-Frankivska", "Volynska", "Ternopilska" };
            foreach (string region in array)
            {
                dictRegionsOfUkr.Add(count, region);
                count++;
            }


            Console.WriteLine("Straight order");
            foreach (var pair in dictRegionsOfUkr)
            {
                Console.WriteLine("{0} - {1}",
                pair.Key,
                pair.Value);
            }


            Console.WriteLine("Reverse order");
            foreach (var pair in dictRegionsOfUkr.Reverse())
            {
                Console.WriteLine("{0} - {1}",
                 pair.Key,
                 pair.Value);
            }


            Console.WriteLine($"Amount of Regions in dictionary is: {dictRegionsOfUkr.Count}");
            dictRegionsOfUkr.Clear();


            Console.WriteLine("===================================================================");

            /*            Завдання No 2*/
            //12 Дано чергу цілих чисел, яка складається з n елементів.
            //+++Обчислити кількість елементів черги, кратних семи.
            //+++За відсутності таких елементів вивести повідомлення «Елементів кратних 7 немає».

            Queue<int> queueOfNumbers = new Queue<int>();
            Console.Write("Type n here: ");
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i < n; i++)
            {
                Console.Write($"Element {i} type here: ");
                int el = Convert.ToInt32(Console.ReadLine());
                queueOfNumbers.Enqueue(el);
            }

            int countOfNumbers = 0;
            foreach (int value in queueOfNumbers)
            {
                if (value % 7 == 0)
                {
                    countOfNumbers++;
                }
            }
            if (countOfNumbers != 0)
            {
                Console.WriteLine("The amount of elements which are multiple of seven is : {0}.", countOfNumbers);

            }
            else
            {
                Console.WriteLine("No elements were found.");
            }
            Console.ReadKey();
        }
    }
}

