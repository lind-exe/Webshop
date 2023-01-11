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
        public static Customer TryLogIn(Customer c)
        {
            using (var db = new WebShopContext())
            {
                var customerList = db.Customers;
                Console.Write("Username: ");
                string user = Console.ReadLine();
                Console.Write("Password: ");
                string passWord = Console.ReadLine();
                var correctUser = customerList.SingleOrDefault(x => x.UserName == user && x.Password == passWord);

                if (correctUser != null)
                {
                    c = correctUser;
                }
                else
                {
                    Console.WriteLine("Try again");
                }
                return c;
            }
        }
        internal static void DisplayCustomer(Customer c)
        {
            Console.Clear();
            Console.SetCursorPosition(40, 0);
            Console.WriteLine("User: " + c.FirstName + "\n");
        }
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
                if (!int.TryParse(Console.ReadLine(), out number) && (number > maxValue && number < minValue))
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

        internal static void Show3HighlightedProducts()
        {
            using (var db = new WebShopContext())
            {
                var products = db.Products;
                var productsList = db.Products.ToList();
                var orderDetailsList = db.OrderDetails;
                Random rnd = new Random();
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
                        Console.Write("    " + randomProduct1.Name.ToUpper() + "\t\t" + "    " + randomProduct2.Name.ToUpper() + "\t\t" + "    " + randomProduct3.Name.ToUpper() + "\n");
                        // print picture
                        BuildPicture();
                        // Print Price
                        Console.Write((discount1 != null ? "Sale Price: " + discount1.Discount + " SEK" : "Price: " + randomProduct1.Price) + " SEK\t\t");
                        Console.Write((discount2 != null ? "Sale Price: " + discount2.Discount + " SEK" : "Price: " + randomProduct2.Price) + " SEK\t\t");
                        Console.Write((discount3 != null ? "Sale Price: " + discount3.Discount + " SEK" : "Price: " + randomProduct3.Price) + " SEK");
                        Console.WriteLine("\n---------------\t\t---------------\t\t---------------");
                        // Print Stock
                        Console.Write("Avaiable: " + randomProduct1.UnitsInStock + "\t\tAvaiable: " + randomProduct2.UnitsInStock + "\t\tAvaiable: " + randomProduct3.UnitsInStock + "\n");
                        // Print Description
                        Console.Write(randomProduct1.Description + "\t" + randomProduct2.Description + "\t" + randomProduct3.Description);
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
            Console.WriteLine("┌" + "".PadRight(13, '─') + "┐" + "\t\t┌" + "".PadRight(13, '─') + "┐" + "\t\t┌" + "".PadRight(13, '─') + "┐");

            for (int rows = 0; rows < 10; rows++)
            {
                for (int cols = 0; cols <= 62; cols++)
                {
                    if ((rows == 5 && cols == 0) || (rows == 5 && cols == 10) || (rows == 5 && cols == 21))
                    {
                        Console.Write("│[Image here] │");
                    }

                    else if (cols == 0 || cols == 14 || cols == 24 || cols == 38 || cols == 48 || cols == 62)
                    {
                        if (rows != 5)
                        {
                            Console.Write("│");
                        }

                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("└" + "".PadRight(13, '─') + "┘" + "\t\t└" + "".PadRight(13, '─') + "┘" + "\t\t└" + "".PadRight(13, '─') + "┘");
        }

        internal static void ShowAccessories()
        {
            throw new NotImplementedException();
        }
    }
}
