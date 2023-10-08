using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Lab3_xml
{
    internal class Program
    {
        //        Сформувати файл “Pasaghyr.xml”, що містить інформацію про дані з полями:
        //        прізвище;
        //        пункт призначення;
        //        кількість місць багажу;
        //        загальна вага даного багажу.
        //1. +++Переглянути дані на консолі;
        //2. +++Вивести загальну кількість місць багажу і загальну вагу багажу пасажирів, які слідують у пункту призначення Х, де Х вводиться з клавіатури.
        //3.+++ Обчислити загальну вагу багажу пасажирів, які слідують у пункт Y.
        static void Main(string[] args)
        {
            List<Pasaghyr> listOfPassengersFromXML = ImportFromXml("D:\\Documents\\uzhnu 3 semester\\C#\\repos\\cs2023\\Lab3-xml\\XMLFile1.xml");
            PrintPasaghyrsList(listOfPassengersFromXML);
            SummaryOfLuggage(listOfPassengersFromXML);
            SummaryOfLuggageY(listOfPassengersFromXML);
        }

        //===================================================================================
        //спочатку я створила список об*єктів типу Пасажир. Для цього я використала функцію CreatePasaghyrsList. Потім я записала їх у файл XML за допомогою відповідної функції ExportToXmlFileFromList. Закоментувала ці функції, оскільки вже отримала у форматі XML потрібні для подальшої роботи дані. Покроково перевіряла чи працює у менй. А потім видаляла звернення, залишаючи тільки описані за межами мейну функції.
        public static List<Pasaghyr> CreatePasaghyrsList()
        {
            Pasaghyr p1 = new Pasaghyr("Bondarenko", "Lviv", 3, 15.5);
            Pasaghyr p2 = new Pasaghyr("Sich", "Ivano-Frankivsk", 1, 7);
            Pasaghyr p3 = new Pasaghyr("Korolovych", "Uzhhorod", 2, 9.8);
            List<Pasaghyr> pasaghyrsList = new List<Pasaghyr>() { p1, p2, p3};
            return pasaghyrsList;
        }    
        public static void ExportToXmlFileFromList(List<Pasaghyr> list, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Pasaghyr>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, list);
            }
        }
        //===================================================================================

        /// <summary>
        /// ця функція мені потрібна для імпортування із файлу типу XML у вигляді списку. Щоб потім було легше працювати із даними усередині C#
        /// </summary>
        /// <param name="filePath">шлях до файлу</param>
        /// <returns>список пасажирів які було імпортовано із файлу типу XML</returns>
        static List<Pasaghyr> ImportFromXml(string filePath)
        {
            List<Pasaghyr> list = new List<Pasaghyr>();
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Pasaghyr>));
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    list = (List<Pasaghyr>)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while importing data from XML: {ex.Message}");
            }
            return list;
        }

        public static void PrintPasaghyrsList(List<Pasaghyr> list)
        {
            foreach (Pasaghyr p in list)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public static void SummaryOfLuggage(List<Pasaghyr> list)
        {
            Console.Write("Where are you headed?(write a city) ");
            string city = Console.ReadLine().ToLower();
            int places = 0;
            double weight = 0;
            
            foreach (Pasaghyr p in list)
            {
                if (p.Destination.ToLower() == city)
                {
                    places += p.LuggageAmount;
                    weight += p.LuggageWeight;
                }
            }
            Console.WriteLine($"Summary amount of places is {places}, summary luggage weight = {weight} kg");
        }

        public static void SummaryOfLuggageY(List<Pasaghyr> list)
        {
            Console.Write("Where are you headed?(choose another city) ");
            string city = Console.ReadLine().ToLower();
            double weight = 0;

            foreach (Pasaghyr p in list)
            {
                if (p.Destination.ToLower() == city)
                {
                    weight += p.LuggageWeight;
                }
            }
            Console.WriteLine($"\nSummary luggage weight = {weight} kg");
        }
    }
}
