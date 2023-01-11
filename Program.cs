//HardCodedValues.AllInserts();
using Webshop.Methods;
using Microsoft.EntityFrameworkCore;
using Webshop.Models;

namespace Webshop
{

    internal class Program
    {
        static void Main(string[] args)
        {
            bool runProgram = true;
            Customer c = null;
            while (runProgram)
            {
                Console.Clear();
                //Helpers.Welcome();
                if (c == null)
                {
                    c = Menus.Show("Main", c);
                }
                else
                {
                    Helpers.DisplayCustomer(c);
                    if (c.UserName == "admin")
                    {

                    }
                    else
                    {

                    }


                    // Helpers.ShowGenres();
                    //Helpers.CreateProduct();
                    //Helpers.InsertGenre();
                    //HardCodedValues.AllInserts();
                    //Helpers.ShowOneHighlightedProduct();

                    //using (var db = new WebShopContext())
                    //{
                    //    var products = db.Products;

                    //    foreach (var product in products)
                    //    {
                    //        Console.WriteLine(product.Name);
                    //    }
                    //}
                }
            }
        }



    }
}