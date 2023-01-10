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
            //Helpers.Welcome();
            //Methods.Menus.Show("Main");
            // Helpers.ShowGenres();
            //Helpers.CreateProduct();
            //Helpers.InsertGenre();
            //HardCodedValues.AllInserts();
            Helpers.ShowOneHighlightedProduct();

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