using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Methods
{
    internal class View
    {
        public static void ShowGenres()
        {
            using (var database = new WebShopContext())
            {
                var genreList = database.Genres;
                foreach (var genre in genreList)
                {
                    Console.WriteLine(genre.Id + " " + genre.Name);
                }

            }
        }
        public static void ShowCategoryId()
        {
            using (var database = new WebShopContext())
            {
                var categorylist = database.Categories;
                foreach (var c in categorylist)
                {
                    Console.WriteLine(c.Id + " " + c.Name);
                }

            }
        }
        public static void ShowSupplier()
        {
            using (var database = new WebShopContext())
            {
                var supplierlist = database.Suppliers;
                foreach (var c in supplierlist)
                {
                    Console.WriteLine(c.Id + " " + c.Name);
                }

            }
        }
        public static void ShowProducts()
        {
            using (var database = new WebShopContext())
            {
                int padValue1 = 12;
                int padValue2 = 30;
                int padValue3 = 20;
                var productlist = database.Products;
                Console.WriteLine("ID".PadRight(padValue1) + "Name".PadRight(padValue2) + "Price, SEK".PadRight(padValue3) + "Unitsinstock");
                Console.WriteLine("--------------------------------------------------------------------------");
                foreach (var c in productlist)
                {
                    Console.WriteLine(c.Id + "".PadRight(padValue1) + c.Name.PadRight(padValue2) + c.Price.ToString().PadRight(padValue3) + c.UnitsInStock);
                }

            }
        }
        internal static void ShowAccessories()
        {

        }
        internal static void DisplayCustomer(Customer c)
        {
            Console.Clear();
            Console.SetCursorPosition(40, 0);
            string? displayUserName = ("User: " + ((c.FirstName != "" ? c.FirstName : (c.UserName == "" ? "Unknown" : c.UserName)) + "\n"));
            try
            {
                Console.WriteLine(displayUserName);
            }
            catch(Exception ex) { Helpers.Choose_Red_Message_Return_To_Login("Could not find a username", c); }
        }
        internal static void Show3HighlightedProducts()
        {
            using (var db = new WebShopContext())
            {
                var products = db.Products;
                var productsList = db.Products.ToList();
                var orderDetailsList = db.OrderDetails;
                Random rnd = new Random();
                int padRightNr = 24;
                int randomNr1 = rnd.Next(1, productsList.Count + 1);
                int randomNr2 = rnd.Next(1, productsList.Count + 1);
                int randomNr3 = rnd.Next(1, productsList.Count + 1);

                if (productsList.Count > 0)
                {
                    var discountPriceList = orderDetailsList.Where(x => x.Discount > 0).ToList();
                    var randomProduct1 = productsList.Where(x => x.Id == randomNr1).SingleOrDefault();
                    var randomProduct2 = productsList.Where(x => x.Id == randomNr2).SingleOrDefault();
                    var randomProduct3 = productsList.Where(x => x.Id == randomNr3).SingleOrDefault();
                    var discount1 = discountPriceList.SingleOrDefault(x => x.ProductId == randomProduct1.Id);
                    var discount2 = discountPriceList.SingleOrDefault(x => x.ProductId == randomProduct2.Id);
                    var discount3 = discountPriceList.SingleOrDefault(x => x.ProductId == randomProduct3.Id);
                    if (randomProduct1 != null && randomProduct2 != null && randomProduct3 != null)
                    {

                        // Print Name
                        Console.Write("  " + randomProduct1.Name.ToUpper().PadRight(padRightNr) + randomProduct2.Name.ToUpper().PadRight(padRightNr) + randomProduct3.Name.ToUpper() + "\n");
                        // print picture
                        Helpers.BuildPicture();
                        // Print Price
                        Console.Write((discount1 != null ? "Sale Price: " + discount1.Discount + " SEK" : "Price: " + randomProduct1.Price) + " SEK\t\t");
                        Console.Write((discount2 != null ? "Sale Price: " + discount2.Discount + " SEK" : "Price: " + randomProduct2.Price) + " SEK\t\t");
                        Console.Write((discount3 != null ? "Sale Price: " + discount3.Discount + " SEK" : "Price: " + randomProduct3.Price) + " SEK");
                        Console.WriteLine("\n---------------\t\t---------------\t\t---------------");
                        // Print Stock
                        Console.Write("Avaiable: " + randomProduct1.UnitsInStock + "\t\tAvaiable: " + randomProduct2.UnitsInStock + "\t\tAvaiable: " + randomProduct3.UnitsInStock + "\n");
                        // Print Description
                        Console.Write(randomProduct1.Description.PadRight(padRightNr) + randomProduct2.Description.PadRight(padRightNr) + randomProduct3.Description);
                    }
                    else
                    {
                        Console.WriteLine("Fel");
                    }
                }
                else
                {
                    Console.WriteLine("For now we have no products to show you. Be back soon for the latest updates");
                }

            }
        }
        public static void ProductsInCategory(int value)
        {

            using (var database = new WebShopContext())
            {
                Console.Clear();
                var productList = database.Products.Where(x => x.CategoryId == value);
                var chosenCategory = database.Categories.Where(x => x.Id == value);
                foreach (var cat in chosenCategory)
                {
                    Console.WriteLine("Listing all " + cat.Name + " products");
                }
                Console.WriteLine("Id\tName");
                foreach (var c in productList)
                {
                    Console.WriteLine(c.Id + "\t" + c.Name);
                }
            }
        }
    }
}
