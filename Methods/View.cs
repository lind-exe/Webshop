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
        public static void ShowCategories()
        {

            using (var database = new WebShopContext())
            {
                var categorylist = database.Categories;
                var maxCategory = database.Categories.ToList();

                foreach (var d in categorylist)
                {
                    Console.WriteLine(d.Id + "\t" + d.Name);
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
            catch (Exception) { Helpers.Choose_Red_Message_Return_To_Login("Could not find a username", c); }
        }
        internal static void Show3HighlightedProducts(int[] highlightedProdsId)
        {
            using (var db = new WebShopContext())
            {
                var products = db.Products;
                var productsList = db.Products.ToList();
                var orderDetailsList = db.OrderDetails;
                Random rnd = new Random();
                int padRightNr = 24;
                int prodId1 = highlightedProdsId[0];
                int prodId2 = highlightedProdsId[1];
                int prodId3 = highlightedProdsId[2];

                //int randomNr1 = rnd.Next(1, productsList.Count + 1); // -> if random: randomNr1 goes instead of prod1Id
                //int randomNr2 = rnd.Next(1, productsList.Count + 1);
                //int randomNr3 = rnd.Next(1, productsList.Count + 1);

                if (productsList.Count > 0)
                {
                    var discountPriceList = orderDetailsList.Where(x => x.Discount > 0).ToList();

                    var highLightedProduct1 = productsList.Where(x => x.Id == prodId1).SingleOrDefault();
                    var highLightedProduct2 = productsList.Where(x => x.Id == prodId2).SingleOrDefault();
                    var highLightedProduct3 = productsList.Where(x => x.Id == prodId3).SingleOrDefault();

                    if (highLightedProduct1 != null && highLightedProduct2 != null && highLightedProduct3 != null)
                    {
                        var discount1 = discountPriceList.SingleOrDefault(x => x.ProductId == highLightedProduct1.Id);
                        var discount2 = discountPriceList.SingleOrDefault(x => x.ProductId == highLightedProduct2.Id);
                        var discount3 = discountPriceList.SingleOrDefault(x => x.ProductId == highLightedProduct3.Id);
                        var descriptionProd1 = highLightedProduct1.Description == null ? "" : highLightedProduct1.Description;
                        var descriptionProd2 = highLightedProduct2.Description == null ? "" : highLightedProduct2.Description;
                        var descriptionProd3 = highLightedProduct3.Description == null ? "" : highLightedProduct3.Description;
                        // Print Name
                        Console.Write("  " + highLightedProduct1.Name.ToUpper().PadRight(padRightNr) + highLightedProduct2.Name.ToUpper().PadRight(padRightNr) + highLightedProduct3.Name.ToUpper() + "\n");
                        // print picture
                        Helpers.BuildPicture();
                        // Print Price
                        Console.Write((discount1 != null ? "Sale Price: " + discount1.Discount + " SEK" : "Price: " + highLightedProduct1.Price) + " SEK\t\t");
                        Console.Write((discount2 != null ? "Sale Price: " + discount2.Discount + " SEK" : "Price: " + highLightedProduct2.Price) + " SEK\t\t");
                        Console.Write((discount3 != null ? "Sale Price: " + discount3.Discount + " SEK" : "Price: " + highLightedProduct3.Price) + " SEK");
                        Console.WriteLine("\n---------------\t\t---------------\t\t---------------");
                        // Print Stock
                        Console.Write("Avaiable: " + highLightedProduct1.UnitsInStock + "\t\tAvaiable: " + highLightedProduct2.UnitsInStock + "\t\tAvaiable: " + highLightedProduct3.UnitsInStock + "\n");
                        // Print Description
                        Console.Write(descriptionProd1.PadRight(padRightNr) + descriptionProd2.PadRight(padRightNr) + descriptionProd3.PadRight(padRightNr));
                    }
                    else
                    {
                        Helpers.WrongInput();
                    }
                }
                else
                {
                    Console.WriteLine("For now we have no products to show you. Be back soon for the latest updates");
                }
            }
        }
        public static int ProductsInCategory(int value)
        {
            int i = 1;
            int chosenP = 0;
            using (var database = new WebShopContext())
            {
                Console.Clear();
                var productList = database.Products.Where(x => x.CategoryId == value).ToList();
                var chosenCategory = database.Categories.Where(x => x.Id == value);
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var cat in chosenCategory)
                {

                    Console.WriteLine("Listing all " + cat.Name + " products: \n");
                }
                Console.ResetColor();
                Console.WriteLine("Id\tName");
                Console.WriteLine("---------------");
                for (i = 0; i < productList.Count; i++)
                {
                    Console.WriteLine((i + 1) + "\t" + productList[i].Name);
                }
                Console.Write("\nChoose product to edit: ");
                chosenP = Helpers.TryNumber(chosenP, i, 1);
                chosenP = productList[chosenP - 1].Id;
                Console.Clear();
            }
            return chosenP;
        }
        public static void ShowProductInOneCategory(Customer c)
        {

            int categoryInput = 0;

            using (var database = new WebShopContext())
            {
                var categorylist = database.Categories;
                var maxCategory = database.Categories.ToList();
                Console.Write("Enter id of the Category: ");
                categoryInput = Helpers.TryNumber(categoryInput, maxCategory.Count, 1);
                var result = (from product in database.Products
                              join category in database.Categories on product.CategoryId equals category.Id
                              where product.CategoryId == categoryInput
                              select new { Products = product, CategoryId = category }
                              ).ToList();
                int padright = 20;
                int padrightshort = 15;
                int answear = 0;
                int i = 0;
                Console.WriteLine("\nID".PadRight(padrightshort) + "Product Name".PadRight(padright) + "Category".PadRight(padrightshort) + "Price");
                Console.WriteLine("-------------------------------------------------------");
                foreach (var d in result)
                {
                    i++;
                    Console.WriteLine(i + "".PadRight(13) + d.Products.Name.PadRight(padright) + d.CategoryId.Name.PadRight(padrightshort) + d.Products.Price);

                }
                Console.Write("Enter the Id of the product: ");
                answear = Helpers.TryNumber(answear, result.Count(), 1);
                answear = result[answear - 1].Products.Id;
                Console.Clear();
                Admin.OneProduct(answear, categoryInput);
                if (result[answear - 1].Products.UnitsInStock > 0)
                {
                    Helpers.AddProductToCart(answear, c);
                }
                else
                {
                    Console.WriteLine("No units in stock. Come back at a later time.");
                }
            }
        }
    }
}
