using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Methods
{
    internal class Menus
    {
        enum MainMenu
        {
            Log_in = 1,
            New_Customer,
            Browse_Shop,
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
            Profile

        }
        enum LogIn
        {
            Sign_In = 1,
            Create_New_User,
            Return = 0
        }

        public static void Show(string value)
        {
            bool logIn = true;
            bool goMain = true;            
            if(value == "Main")                
            {
                while (goMain)
                {

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
                        Console.WriteLine("Wrong Input");
                    }
                    switch (menu)
                    {
                        case MainMenu.Log_in:
                            Show("LogIn");
                            goMain = false;
                            break;
                        case MainMenu.New_Customer:
                            Show("LogIn");
                            goMain = false;
                            break;
                        case MainMenu.Browse_Shop:
                            Show("LogIn");
                            goMain = false;
                            break;
                        case MainMenu.Search_Product:
                            Show("LogIn");
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
                        Console.WriteLine("Wrong Input");
                    }
                    switch (login)
                    {
                        case LogIn.Sign_In:
                            Show("LogIn");
                            logIn = false;
                            break;
                        case LogIn.Create_New_User:
                            Helpers.CreateUser();
                            logIn = false;
                            break;
                        case LogIn.Return:
                            Show("Main");
                            logIn = false;
                            break;

                    }
                }
            }
         
        }
    }
}
