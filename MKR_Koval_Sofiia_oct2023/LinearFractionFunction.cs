using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKR_Koval_Sofiia_oct2023
{
    internal class LinearFractionFunction : FractionFunction
    {
        public LinearFractionFunction(double a0, double a1, double b0, double b1) : base(a0, a1, 0, b0, b1, 0){
           
        }

        public override string ToString()
        {
            return $"a0 = {a0}, a1 = {a1}, a2 = {0}, b0 = {b0}, b1 = {b1}, b2 = {0} \n ({a1}*x + {a0})/({b1}*x + {b0})";
        }

        public double CalculateValueInCpecificXLinear(double x)
        {
            double chyselnyk = a1 * x + a0;
            double znamennyk = b1 * x + b0;

            if (znamennyk == 0)
            {
                throw new Exception("Znamennyk = 0");
            }
            return chyselnyk / znamennyk;
        }
    }
}
