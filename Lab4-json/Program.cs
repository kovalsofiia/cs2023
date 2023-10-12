using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab4_json
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //            Сформувати файл “Pasaghyr.json”, що містить інформацію про дані з полями:
            //            прізвище;
            //            пункт
            //            призначення;
            //            кількість місць багажу;
            //            загальна вага даного багажу.
            //1.+++ Переглянути дані на консолі;
            //2.+++ Вивести загальну кількість місць багажу і загальну вагу багажу пасажирів, які слідують у
            //пункту призначення Х, де Х вводиться з клавіатури.
            //3.+++ Обчислити загальну вагу багажу пасажирів, які слідують у пункт Y.

            //List<Pasaghyr> x = CreatePasaghyrsList();
            //ExportToJsonFileFromList(x, "D:\\Documents\\uzhnu 3 semester\\C#\\repos\\cs2023\\Lab4-json\\json1.json");

            ImportFromJson();


        }
        //======================================================================================
        public static List<Pasaghyr> CreatePasaghyrsList()
        {
            Pasaghyr p1 = new Pasaghyr("Bondarenko", "Lviv", 3, 15.5);
            Pasaghyr p2 = new Pasaghyr("Sich", "Ivano-Frankivsk", 1, 7);
            Pasaghyr p3 = new Pasaghyr("Korolovych", "Uzhhorod", 2, 9.8);
            List<Pasaghyr> pasaghyrsList = new List<Pasaghyr>() { p1, p2, p3 };
            return pasaghyrsList;
        }

        public static void ExportToJsonFileFromList(List<Pasaghyr> list)
        {
            // запис даних у форматі json
            var serializedOcject = JsonConvert.SerializeObject(list);
            Console.WriteLine(serializedOcject);

            // зберегти в файл
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"D:\Documents\uzhnu 3 semester\C#\repos\cs2023\Lab4-json\json1.json", serializedOcject);
        }
        //=========================================================================================
        public static void ImportFromJson()
        {
            try
            {
                string jsonFileName = @"D:\Documents\uzhnu 3 semester\C#\repos\cs2023\Lab4-json\json1.json";
                if (File.Exists(jsonFileName))
                {
                    string jsonData = File.ReadAllText(jsonFileName);
                    List<Pasaghyr> pasaghyrsList = JsonConvert.DeserializeObject<List<Pasaghyr>>(jsonData);

                    if (pasaghyrsList != null && pasaghyrsList.Count > 0)
                    {
                        Console.WriteLine("Data imported from JSON file:");
                        PrintPasaghyrsList(pasaghyrsList);

                        // Now you can perform operations on the imported data
                        SummaryOfLuggage(pasaghyrsList);
                        SummaryOfLuggageY(pasaghyrsList);
                    }
                    else
                    {
                        Console.WriteLine("No data found in the JSON file.");
                    }
                }
                else
                {
                    Console.WriteLine("JSON file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while importing data from JSON: {ex.Message}");
            }
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
