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
            HardCodedValues.AllInserts();
            //bool runProgram = true;
            //Customer c = null;
            //while (runProgram)
            //{
            //    Console.Clear();
            //    //Helpers.Welcome();
            //    if (c == null)
            //    {
            //        c = Menus.Show("LogIn", c);
            //    }
            //    else
            //    {
            //        Helpers.DisplayCustomer(c);
            //        if (c.UserName == "admin")
            //        {

            //        }
            //        else
            //        {
            //            Helpers.DisplayCustomer(c);
            //            Console.ReadKey();
            //        }



            //        using (var db = new WebShopContext())
            //        {
            //            var products = db.Products;

            //            foreach (var product in products)
            //            {
            //                Console.WriteLine(product.Name);
            //            }
            //        }

            //    }
            //}
            //Methods.Admin.ChangePrice();
            //Methods.Admin.ChangeUnitsInStock();
        }
    }
}