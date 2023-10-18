using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKR_Koval_Sofiia_oct2023
{
    internal class Program
    {
        public static double a0;
        public static double a1;
        public static double a2;
        public static double b0;
        public static double b1;
        public static double b2;
        public static double result1;
        public static double result2;


        static void Main(string[] args)
        {
            GetCoef();
            FractionFunction ex1 = new FractionFunction(a0, a1, a2, b0, b1, b2);
            Console.WriteLine(ex1.ToString());
            ex1.CalculateValueInCpecificX();     
        }


        public static void GetCoef()
        {
            Console.Write("a0 = ");
            a0 = Convert.ToDouble(Console.ReadLine());
            Console.Write("a1 = ");
            a1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("a2 = ");
            a2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("b0 = ");
            b0 = Convert.ToDouble(Console.ReadLine());
            Console.Write("b1 = ");
            b1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("b2 = ");
            b2 = Convert.ToDouble(Console.ReadLine());
        }
    }
}
