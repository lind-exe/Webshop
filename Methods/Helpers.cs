using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Methods
{
    internal class Helpers
    {
        internal static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            string title1 = @" __     __   _  _    __  __                                                                 
|  \   |  \ | \| \  |  \|  \                                                                
| $$   | $$ _\$_\$  | $$| $$   __   ______   ______ ____   ______ ____    ______   _______  
| $$   | $$|      \ | $$| $$  /  \ /      \ |      \    \ |      \    \  /      \ |       \ 
 \$$\ /  $$ \$$$$$$\| $$| $$_/  $$|  $$$$$$\| $$$$$$\$$$$\| $$$$$$\$$$$\|  $$$$$$\| $$$$$$$\
  \$$\  $$ /      $$| $$| $$   $$ | $$  | $$| $$ | $$ | $$| $$ | $$ | $$| $$    $$| $$  | $$
   \$$ $$ |  $$$$$$$| $$| $$$$$$\ | $$__/ $$| $$ | $$ | $$| $$ | $$ | $$| $$$$$$$$| $$  | $$
    \$$$   \$$    $$| $$| $$  \$$\ \$$    $$| $$ | $$ | $$| $$ | $$ | $$ \$$     \| $$  | $$
     \$     \$$$$$$$ \$$ \$$   \$$  \$$$$$$  \$$  \$$  \$$ \$$  \$$  \$$  \$$$$$$$ \$$   \$$
                                                                                            
                                                                                            
                                                                                            ";
            string title2 = @"   __      __  __  __ 
  |  \    |  \|  \|  \
 _| $$_    \$$| $$| $$
|   $$ \  |  \| $$| $$
 \$$$$$$  | $$| $$| $$
  | $$ __ | $$| $$| $$
  | $$|  \| $$| $$| $$
   \$$  $$| $$| $$| $$
    \$$$$  \$$ \$$ \$$
                      
                      
                      ";

            string title3 = @"  ______    ______   __       __  ________   ______  ________   ______   _______    ______   __    __   ______   _______  
 /      \  /      \ |  \     /  \|        \ /      \|        \ /      \ |       \  /      \ |  \  |  \ /      \ |       \ 
|  $$$$$$\|  $$$$$$\| $$\   /  $$| $$$$$$$$|  $$$$$$\\$$$$$$$$|  $$$$$$\| $$$$$$$\|  $$$$$$\| $$  | $$|  $$$$$$\| $$$$$$$\
| $$ __\$$| $$__| $$| $$$\ /  $$$| $$__    | $$___\$$  | $$   | $$  | $$| $$__/ $$| $$___\$$| $$__| $$| $$  | $$| $$__/ $$
| $$|    \| $$    $$| $$$$\  $$$$| $$  \    \$$    \   | $$   | $$  | $$| $$    $$ \$$    \ | $$    $$| $$  | $$| $$    $$
| $$ \$$$$| $$$$$$$$| $$\$$ $$ $$| $$$$$    _\$$$$$$\  | $$   | $$  | $$| $$$$$$$  _\$$$$$$\| $$$$$$$$| $$  | $$| $$$$$$$ 
| $$__| $$| $$  | $$| $$ \$$$| $$| $$_____ |  \__| $$  | $$   | $$__/ $$| $$      |  \__| $$| $$  | $$| $$__/ $$| $$      
 \$$    $$| $$  | $$| $$  \$ | $$| $$     \ \$$    $$  | $$    \$$    $$| $$       \$$    $$| $$  | $$ \$$    $$| $$      
  \$$$$$$  \$$   \$$ \$$      \$$ \$$$$$$$$  \$$$$$$    \$$     \$$$$$$  \$$        \$$$$$$  \$$   \$$  \$$$$$$  \$$      
                                                                                                                          
                                                                                                                          
                                                                                                                          ";
            Console.WriteLine(title1);
            Thread.Sleep(700);
            Console.WriteLine(title2);
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine(title3);
            Thread.Sleep(3000);
            Console.Clear();
            Console.ResetColor();
        }
        public static int TryNumber(int number, int maxValue, int minValue)               //input security
        {
            bool correctInput = false;

            while (!correctInput)
            {
                if (int.TryParse(Console.ReadLine(), out number) && (number <= maxValue && number >= minValue))
                {
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, try again.");
                }
            }
            return number;
        }
        // Trycatch >>>> unika email
        public static void CreateUser()
        {

            Console.WriteLine("Input username: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Input password: ");
            string passWord = Console.ReadLine();
            Console.WriteLine("Input first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Input last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Input age: ");
            int age = 0;
            age = TryNumber(age, 100, 0);
            Console.WriteLine("Input country: ");
            string country = Console.ReadLine();
            Console.WriteLine("Input city: ");
            string city = Console.ReadLine();
            Console.WriteLine("Input street name: ");
            string street = Console.ReadLine();
            Console.WriteLine("Input postal code: ");
            int postal = 0;
            postal = TryNumber(postal, 99999, 10000);
            Console.WriteLine("Input phone number: ");
            int phone = 0;
            phone = TryNumber(phone, 999999999, 0);
            Console.WriteLine("Input email address: ");
            string email = Console.ReadLine();

            using (var database = new WebShopContext())            //detta lägger till varje sak
            {
                var newCustomer = new Customer
                {
                    UserName = userName,
                    Password = passWord,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Country = country,
                    City = city,
                    Street = street,
                    PostalCode = postal,
                    Phone = phone,
                    Email = email
                };

                database.Add(newCustomer);
                database.SaveChanges();
            }

            //var sql = "INSERT INTO dbo.Customers(UserName, PassWord, FirstName, LastName, Age, Country, City, Street, PostalCode, Phone, Email)" +
            //     " VALUES ()";
        }
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

        internal static void ShowOneHighlightedProduct()
        {
            using (var db = new WebShopContext())
            {
                var products = db.Products;
                var productsList = db.Products.ToList();
                var orderDetailsList = db.OrderDetails;
                Random rnd = new Random();
                int randomNr = rnd.Next(1, productsList.Count);

                if (productsList.Count > 0)
                {
                    var discountPriceList = orderDetailsList.Where(x => x.Discount > 0).ToList();
                    var randomProduct = productsList.Where(x => x.Id == randomNr).SingleOrDefault();
                    var discount = discountPriceList.SingleOrDefault(x => x.ProductId == randomProduct.Id);
                    if (randomProduct!= null)
                    {

                        // Print Name
                        Console.WriteLine("    " + randomProduct.Name.ToUpper() + "\t\t\n");
                        // print picture
                        BuildPicture();
                        // Print Price
                        Console.WriteLine((discount != null ? "Sale Price: " + discount.Discount + " SEK" : "Price: " + randomProduct.Price) + " SEK");
                        Console.WriteLine("---------------");
                        // Print Description
                        Console.WriteLine("Description: " + randomProduct.Description);
                        // Print Stock
                        Console.WriteLine("Avaiable: " + randomProduct.UnitsInStock);
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

        internal static void BuildPicture()
        {
            Console.WriteLine("┌" + "".PadRight(13, '─') + "┐");

            for (int rows = 0; rows < 10; rows++)
            {
                Console.Write("│");
                for (int cols = 0; cols < 13; cols++)
                {
                    if (rows == 5 && cols == 0)
                    {
                        Console.Write("[Image here] ");
                        cols = 14;
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write("│");
                Console.WriteLine();
            }

            Console.WriteLine("└" + "".PadRight(13, '─') + "┘");
        }


    }
}
