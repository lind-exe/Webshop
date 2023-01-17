using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
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
                var correctUsername = customerList.SingleOrDefault(x => x.UserName == user);
                var correctUser = customerList.SingleOrDefault(x => x.Password == passWord && x.UserName == user);


                if (correctUsername != null && correctUser != null)
                {
                    c = correctUser;
                }
                else if (correctUsername == null && correctUser == null)
                {
                    Choose_Red_Message_Return_To_Login("User does not exist, try again or register new user!", c);
                }
                else if (correctUsername != null && correctUser == null)
                {
                    Helpers.Choose_Red_Message_Return_To_Login("Wrong password", c);
                }
                else
                {
                    Helpers.Choose_Red_Message_Return_To_Login("Try again", c);
                }
                return c;
            }
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
                if (!int.TryParse(Console.ReadLine(), out number) || number > maxValue || number < minValue)
                {
                    Console.Write("Wrong input, try again: ");
                    //ClearLine();
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }
        // Trycatch >>>> unika email
        public static Customer CreateUser(Customer c)
        {

            Console.WriteLine("Input username: ");
            string userName = CheckStringInput();
            Console.WriteLine("Input password: ");
            string passWord = CheckStringInput();
            Console.WriteLine("Input first name: ");
            string firstName = CheckStringInput();
            Console.WriteLine("Input last name: ");
            string lastName = CheckStringInput();
            Console.WriteLine("Input age: ");
            int age = 15;
            age = TryNumber(age, 100, 15);
            Console.WriteLine("Input country: ");
            string country = CheckStringInput();
            Console.WriteLine("Input city: ");
            string city = CheckStringInput();
            Console.WriteLine("Input street name: ");
            string street = CheckStringInput();
            Console.WriteLine("Input postal code: ");
            int postal = 10000;
            postal = TryNumber(postal, 99999, 10000);
            Console.WriteLine("Input phone number: ");
            int phone = 99999999;
            phone = TryNumber(phone, 999999999, 99999999);
            Console.WriteLine("Input email address: ");
            string email = CheckStringInput();

            using (var database = new WebShopContext())            //detta lägger till varje sak
            {
                var customerList = database.Customers;
                var userNameExists = customerList.SingleOrDefault(x => x.UserName == userName) != null;
                if (userNameExists)
                {
                    Choose_Red_Message_Return_To_Login("User does already exist, try again or register new user!", c);
                }
                else
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
                    c = newCustomer;
                }
                return c;
            }

        }
        internal static void BuildPicture()
        {
            Console.WriteLine("┌" + "".PadRight(13, '─') + "┐" + "\t\t┌" + "".PadRight(13, '─') + "┐" + "\t\t┌" + "".PadRight(13, '─') + "┐");

            for (int rows = 0; rows < 6; rows++)
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
        internal static void WrongInputWithoutClear()
        {
            Console.SetCursorPosition(2, 2);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("WRONGINPUT");
            Console.ResetColor();
        }
        internal static void WrongInput()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("WRONGINPUT");
            Thread.Sleep(500);
            Console.ResetColor();
            Console.Clear();
        }
        public static string CheckStringInput()
        {
            bool tryAgain = true;
            string? outPut = "";
            while (tryAgain)
            {
                outPut = Console.ReadLine();
                if (outPut == null)
                {
                    Console.WriteLine("Your input must contain atleast one character");
                }
                else if (outPut.Length > 0)
                {
                    tryAgain = false;
                }
                else
                {
                    Console.WriteLine("Your input must contain atleast one character");
                }

            }
            return outPut;
        }
        public static void ClearLine() // fixa så att input skrivs på samma ställe utan spam i konsollen
        {
            Console.Write("                                      ");
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - (Console.WindowWidth >= Console.BufferWidth ? 1 : 0));
        }

        internal static void PressAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPress any key to continue: ");
            Console.ReadKey(true);
            Console.ResetColor();
        }

        internal static void Choose_Red_Message_Return_To_Login(string message, Customer c)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
            Console.ResetColor();
            Console.Clear();
            Menus.Show("LogIn", c);
        }
        public static void AddProductToCart(int pId, Customer c)
        {
            int answear = 0;
            Console.WriteLine("\n\n\n1. Add to cart \n2. Return to main menu ");
            answear = TryNumber(answear, 2, 1);
            int amount = 0;

            if (answear == 1)
            {
                using (var db = new WebShopContext())
                {
                    var order = db.Orders.Where(x => x.CustomerId == c.Id).OrderBy(x => x.Id).LastOrDefault();
                    if (order == null || order.Purchased == true)
                    {
                        var newOrder = new Order
                        {
                            CustomerId = c.Id
                        };

                        db.Add(newOrder);
                        db.SaveChanges();
                    }

                    var orderUpdated = db.Orders.Where(x => x.CustomerId == c.Id).OrderBy(x => x.Id).LastOrDefault();
                    var product = db.Products.Where(x => x.Id == pId).ToList();
                    Console.Write("How many do you want to buy?: ");
                    amount = TryNumber(amount, product[0].UnitsInStock, 1);

                    var orderDetails = db.OrderDetails.ToList();
                    var newOrderDetails = new OrderDetail()
                    {
                        OrderId = orderUpdated.Id,
                        Quantity = amount,
                        ProductId = pId,
                        UnitPrice = product[0].Price
                        //discount?


                    };
                    db.Add(newOrderDetails);
                    db.SaveChanges();
                    Console.WriteLine("Added " + product[0].Name + " to cart.");
                    Thread.Sleep(1000);
                    Menus.Show("Main", c);
                }
            }
            else
            {
                Menus.Show("Main", c);
            }
        }

        internal static void CheckOut(Customer c)
        {
            Console.Clear();
            int shipper = 0;
            int payment = 0;

            using (var db = new WebShopContext())
            {
                var shipmentChoices = db.ShipChoices.ToList();
                var paymentMethods = db.PaymentMethods.ToList();
                var productList = db.Products.ToList();


                View.ShowOrders(c);
                View.ShippingMethods();
                Console.Write("Select shipping method: ");
                shipper = TryNumber(shipper, shipmentChoices.Count(), 1);
                View.PaymentMethods();
                Console.Write("Select payment method: ");
                payment = TryNumber(payment, paymentMethods.Count(), 1);

                var orderUpdated = db.Orders.Where(x => x.CustomerId == c.Id).OrderBy(x => x.Id).LastOrDefault();
                var orderDetails = db.OrderDetails.Where(x => x.OrderId == orderUpdated.Id).ToList();
                
                orderUpdated.ShipChoice = shipmentChoices.Where(x => x.Id == shipper).FirstOrDefault();
                orderUpdated.PaymentMethod = paymentMethods.Where(x => x.Id == payment).FirstOrDefault();
                orderUpdated.OrderDate = DateTime.Now;
                orderUpdated.Purchased = true;

                db.SaveChanges();
                
                for(int i = 0; i < orderDetails.Count(); i++)
                {

                    var choosenProduct = db.Products.Where(x => x.Id == orderDetails[i].ProductId).ToList();
                    choosenProduct[0].UnitsInStock = choosenProduct[0].UnitsInStock - orderDetails[0].Quantity;
                    db.SaveChanges();
                }

                var newOrder = new Order()
                {
                    CustomerId = c.Id
                };
                db.Add(newOrder);
                db.SaveChanges();

            }
        }

        internal static void EditCartQuantity(Customer c)
        {
            using (var db = new WebShopContext())
            {
                int i = 0;
                var input = 0;
                var newQuantity = 0;
                var result = (
               from orders in db.Orders
               join orderDetails in db.OrderDetails on orders.Id equals orderDetails.OrderId
               join product in db.Products on orderDetails.ProductId equals product.Id

               where orders.CustomerId == c.Id && orders.Purchased == null
               select new { Orders = orders, OrderDetails = orderDetails, Products = product }
               );
                var resultList=result.ToList();
                Console.WriteLine("ID\tProduct\tPrice\tQuantity\tOrder ID");
                Console.WriteLine("------------------------------------------------------------------------------");
                foreach (var p in result)
                {
                    i++;
                    Console.WriteLine(i+"\t"+p.Products.Name + "\t" + p.Products.Price + "\t" + "\t" + p.OrderDetails.Quantity + "\t" + p.Orders.Id);
                }
                Console.Write("\nEnter the id of the product you want to change: ");
                input = TryNumber(input, result.Count(), 1);
                Console.Write("\nEnter the new quantity: ");
                newQuantity = TryNumber(newQuantity, resultList[input-1].Products.UnitsInStock,1);
                resultList[input - 1].OrderDetails.Quantity = newQuantity;
                db.SaveChanges();
            }
        }

        internal static void RemoveCartProducts(Customer c)
        {
            using (var db = new WebShopContext())
            {
                int i = 0;
                var input = 0;
                var newQuantity = 0;
                
                var result = (
               from orders in db.Orders
               join orderDetails in db.OrderDetails on orders.Id equals orderDetails.OrderId
               join product in db.Products on orderDetails.ProductId equals product.Id

               where orders.CustomerId == c.Id && orders.Purchased == null
               select new { Orders = orders, OrderDetails = orderDetails, Products = product }
               );
                var resultList = result.ToList();
                Console.WriteLine("ID\tProduct\tPrice\tQuantity\tOrder ID");
                Console.WriteLine("------------------------------------------------------------------------------");
                foreach (var p in result)
                {
                    i++;
                    Console.WriteLine(i + "\t" + p.Products.Name + "\t" + p.Products.Price + "\t" + "\t" + p.OrderDetails.Quantity + "\t" + p.Orders.Id);
                }
                Console.Write("\nEnter the id of the product you want to remove: ");
                input = TryNumber(input, result.Count(), 1);
                var idToRemove = (from od in db.OrderDetails where od.Id == resultList[input-1].OrderDetails.Id select od).FirstOrDefault();
                db.OrderDetails.Remove((OrderDetail)idToRemove);
                db.SaveChanges();              


            }
        }
    }
}
