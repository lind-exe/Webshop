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
            Search_Products,
            Exit_Shop,
            Admin_Menu,
            Log_Out = 9

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
        }
        enum Admin
        {
            Edit_Customers = 1,
            Edit_Products,
            Edit_Highlighted_Products,
            Return = 0

        }
        enum AdminProducts
        {
            Edit_Products = 1,           
            Add_Product,
            Remove_Product,
            Return = 0
        }

        public static Customer Show(string value, Customer c)
        {
            bool logIn = true;
            bool goMain = true;
            bool browseShop = true;
            bool adminMenu = true;
            bool adminProducts = true;
            if (value == "Main")
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
                        case MainMenu.Search_Products:
                            Show("SearchProduct", c);
                            goMain = false;
                            break;
                        case MainMenu.Exit_Shop:
                            Console.WriteLine("Thank you come again"); ;
                            goMain = false;
                            break;
                        case MainMenu.Admin_Menu:
                            if (c.UserName == "admin")
                            {
                                Show("Admin", c);
                            }
                            else
                            {
                                Helpers.WrongInput();
                            }
                            break;
                        case MainMenu.Log_Out:
                            c = null;
                            goMain = false;
                            Show("LogIn", c);
                            break;

                    }
                }
            }
            if (value == "LogIn")
            {
                while (logIn)
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
                            c = Helpers.CreateUser(c);
                            logIn = false;
                            break;

                    }
                }
                Show("Main", c);
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
                            Methods.Admin.ChosenProduct(4, c);
                            //User.SelectProduct
                            Console.ReadKey();
                            browseShop = false;
                            break;
                        case BrowseShop.Consoles:
                            View.ShowCategoryId();
                            Console.ReadKey();
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
            if (value == "Admin")
            {
                while (adminMenu)
                {
                    View.DisplayCustomer(c);
                    foreach (int i in Enum.GetValues(typeof(Admin)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(Admin), i).Replace("_", " ")}");
                    }

                    int nr;
                    Admin admin = (Admin)99; //Default
                    if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        Helpers.WrongInput();
                    }
                    else
                    {
                        admin = (Admin)nr;
                        Console.Clear();

                    }
                    switch (admin)
                    {
                        case Admin.Edit_Customers:



                            Console.ReadKey();
                            browseShop = false;
                            break;
                        case Admin.Edit_Products:
                            Show("AdminProducts", c);
                            Console.ReadKey();
                            browseShop = false;
                            break;
                        case Admin.Edit_Highlighted_Products:
                            break;
                        case Admin.Return:
                            Show("Main", c);
                            browseShop = false;
                            break;

                    }
                }
            }
            if (value == "AdminProducts")
            {
                while (adminProducts)
                {
                    View.DisplayCustomer(c);
                    foreach (int i in Enum.GetValues(typeof(AdminProducts)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(AdminProducts), i).Replace("_", " ")}");
                    }

                    int nr;
                    AdminProducts admin = (AdminProducts)99; //Default
                    if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                    {
                        Helpers.WrongInput();
                    }
                    else
                    {
                        admin = (AdminProducts)nr;
                        Console.Clear();

                    }
                    switch (admin)
                    {
                        case AdminProducts.Edit_Products:
                            Methods.Admin.ChosenCategory(c);

                            Console.ReadKey();
                            adminProducts = false;
                            break;
                        case AdminProducts.Add_Product:
                            Methods.Admin.AddProduct();
                            adminProducts = false;
                            break;
                        case AdminProducts.Return:
                            Show("Main", c);
                            adminProducts = false;
                            break;

                    }
                }
            }
            return c;
        }
    }
}
