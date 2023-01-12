using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("User does not exist, try again or register new user!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    Console.ResetColor();
                    Console.Clear();
                    Menus.Show("LogIn", c);
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
                    Thread.Sleep(800);
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
            string passWord = Console.ReadLine();
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
                return c = newCustomer;
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
            string outPut = "";
            while (tryAgain)
            {
                outPut = Console.ReadLine();
                if (outPut.Count() > 0)
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
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - (Console.WindowWidth >= Console.BufferWidth ? 1 : 0));
        }

        internal static void PressAnyKey()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press any key to continue: ");
            Console.ReadKey(true);
            Console.ResetColor();
        }
    }
}
