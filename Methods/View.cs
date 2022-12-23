using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Methods
{
    internal class View
    {
        public static void MainMenu()
        {
            
        }
        public static void ShowInfo(int input)
        {

            switch (input)
            {
                case 1:
                    Games();
                    break;
                case 2:
                    Consoles();
                    break;
                case 3:
                    Accesories();
                    break;
                case 4:
                    PaymentMethod();
                    break;
                case 5:
                    ShipChoice();
                    break;
                case 6:
                    Customers();
                    break;
                case 7:
                    AllProducts();
                    break;
            };
        }

        public static void Games()      //Products
        {

        }
        public static void Consoles()   //Categories
        {

        }
        public static void PaymentMethod()
        {

        }
        public static void Customers()
        {

        }
        public static void ShipChoice()
        {

        }
        public static void HighlightedProducts()
        {

        }
        public static void Accesories()
        {

        }
        public static void AllProducts()
        {

        }
    }
}
