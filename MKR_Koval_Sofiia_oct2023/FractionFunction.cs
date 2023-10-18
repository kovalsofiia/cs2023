using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKR_Koval_Sofiia_oct2023
{
    internal class FractionFunction
    {
        public double a0;
        public double a1;
        public double a2;
        public double b0;
        public double b1;
        public double b2;

        public FractionFunction(double a0, double a1, double a2, double b0, double b1, double b2)
        {
            this.a0 = a0;
            this.a1 = a1;
            this.a2 = a2;
            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
        }

        public override string ToString()
        {
            return $" a0 = {a0}, a1 = {a1}, a2 = {a2}, b0 = {b0}, b1 = {b1}, b2 = {b2} \n ({a2}*x^2 + {a1}*x + {a0})/({b2}*x^2 + {b1}*x + {b0})";
        }

        public void CalculateValueInCpecificX()
        {
            Console.Write("Write x0 = ");
            double x = Convert.ToDouble(Console.ReadLine());

            double chyselnyk = a2 * x * x + a1 * x + a0;
            double znamennyk = b2 * x * x + b1 * x + b0;

            if(znamennyk == 0)
            {
                throw new Exception("Znamennyk = 0");
            }

            double result = chyselnyk / znamennyk;
            Console.WriteLine($"Answer at {x} = {result}");

        }
    }
}
