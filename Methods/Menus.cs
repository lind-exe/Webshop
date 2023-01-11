using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Methods
{
    internal class Menus
    {
        enum MainMenu
        {
            Browse_Shop = 1,
            Search_Product,
            Exit_Shop

        }
        enum BrowseShop
        {
            Games = 1,
            Consoles,
            Accessories,
            Merchandise,
            Shoppingcart,
            Profile,
            Return = 0

        }
        enum LogIn
        {
            Sign_In = 1,
            Create_New_User,
            Return = 0
        }

        public static Customer Show(string value, Customer c)
        {
            bool logIn = true;
            bool goMain = true;  
            bool browseShop = true;
            if(value == "Main")                
            {
                while (goMain)
                {
                    View.DisplayCustomer(c);
                    foreach (int i in Enum.GetValues(typeof(MainMenu)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(MainMenu), i).Replace("_", " ")}");
                    }

                    int nr;
                    MainMenu menu = (MainMenu)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        menu = (MainMenu)nr;
                        Console.Clear();
                    }
                    else
                    {
                        Helpers.WrongInput();
                    }
                    switch (menu)
                    {
                        case MainMenu.Browse_Shop:
                            Show("BrowseShop", c);
                            goMain = false;
                            break;
                        case MainMenu.Search_Product:
                            Show("SearchProduct", c);
                            goMain = false;
                            break;
                        case MainMenu.Exit_Shop:
                            Console.WriteLine("Thank you come again"); ;
                            goMain = false;
                            break;
                    }
                }
            }
            if(value == "LogIn")
            {
                while(logIn)
                {
                    foreach (int i in Enum.GetValues(typeof(LogIn)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(LogIn), i).Replace("_", " ")}");
                    }

                    int nr;
                    LogIn login = (LogIn)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        login = (LogIn)nr;
                        Console.Clear();
                    }
                    else
                    {
                        Helpers.WrongInput();
                    }
                    switch (login)
                    {
                        case LogIn.Sign_In:
                            c = Helpers.TryLogIn(c);
                            logIn = false;
                            break;
                        case LogIn.Create_New_User:
                            Helpers.CreateUser();
                            logIn = false;
                            break;
                        case LogIn.Return:
                            Show("Main", c);
                            logIn = false;
                            break;

                    }
                }
            }
            if (value == "BrowseShop")
            {
                while (browseShop)
                {
                    View.DisplayCustomer(c);
                    foreach (int i in Enum.GetValues(typeof(BrowseShop)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(BrowseShop), i).Replace("_", " ")}");
                    }

                    int nr;
                    BrowseShop shop = (BrowseShop)99; //Default
                    if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        Helpers.WrongInput();
                    }
                    else
                    {
                        shop = (BrowseShop)nr;
                        Console.Clear();
                        
                    }
                    switch (shop)
                    {
                        case BrowseShop.Games:
                            View.DisplayCustomer(c);
                            View.ShowProducts();
                            //User.SelectProduct
                            Console.ReadKey();
                            browseShop = false;
                            break;
                        case BrowseShop.Consoles:
                            View.ShowCategoryId();
                            browseShop = false;
                            break;
                        case BrowseShop.Accessories:
                            View.ShowAccessories();
                            break;
                        case BrowseShop.Return:
                            Show("Main", c);
                            browseShop = false;
                            break;

                    }
                }
            }
            return c;
        }
    }
}
