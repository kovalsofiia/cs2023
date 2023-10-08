using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_xml
{
    public class Pasaghyr
    {
        public string Surname { get; set; }
        public string Destination { get; set; }
        public int LuggageAmount { get; set; }
        public double LuggageWeight { get; set; }

        public Pasaghyr(string surname, string destination, int luggageAmount, double luggageWeight)
        {
            this.Surname = surname;
            this.Destination = destination;
            this.LuggageAmount = luggageAmount;
            this.LuggageWeight = luggageWeight;
        }

        public Pasaghyr() { }
        public override string ToString()
        {
            return $"Surname: {Surname} \n => destination: {Destination}. \nAmount of luggage {LuggageAmount} with the weight {LuggageWeight} kg\n";
        }
    }
}
