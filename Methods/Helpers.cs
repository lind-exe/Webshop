using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Methods
{
    internal class Helpers
    {
        internal static int TryNumber(int number, int maxValue, int minValue)               //input security
        {
            bool correctInput = false;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number) || number <= maxValue || number >= minValue)
                {
                    Console.WriteLine("Wrong input, try again.");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }
        public static void CreateUser()
        {
            Console.WriteLine("Hej Skriv ditt namn eller tryck x för backa");
        }
    }
}
