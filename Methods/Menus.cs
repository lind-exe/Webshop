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
        private static int _count = 0;

        private static int[] _highlightedProdsId = new int[3];
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
            Products = 1,
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
            Queries,
            Return = 0

        }
        enum AdminProducts
        {
            Edit_Products = 1,
            Add_Product,
            Return = 0
        }
        public enum CustomerEdit
        {
            User_Name = 1,
            First_Name,
            Last_Name,
            Country,
            City,
            Street,
            Postal,
            Phone,
            Email,
            Return = 0
        }

        public static Customer Show(string value, Customer c)                   //fixa wrong input till meny val - funkar nästan
        {
            bool logIn = true;
            bool goMain = true;
            bool browseShop = true;
            bool adminMenu = true;
            bool adminProducts = true;

            if (_count < 1)
            {
                using (var db = new WebShopContext())
                {
                    if (db.Products.ToList().Count > 0)
                    {
                        var product1 = db.Products.FirstOrDefault(x => x.Id > 0);
                        var product2 = db.Products.FirstOrDefault(x => x.Id > 0 && x.Id != product1.Id);
                        var product3 = db.Products.FirstOrDefault(x => x.Id > 0 && x.Id != product2.Id);
                        _highlightedProdsId[0] = product1.Id;
                        _highlightedProdsId[1] = product2.Id;
                        _highlightedProdsId[2] = product3.Id;

                    }
                }
                _count++;
            }
            if (value == "Main")
            {
                while (goMain)
                {
                    View.DisplayCustomer(c);
                    Console.SetCursorPosition(0, 10);

                    View.Show3HighlightedProducts(_highlightedProdsId);       

                    Console.SetCursorPosition(0, 2);
                    foreach (int i in Enum.GetValues(typeof(MainMenu)))
                    {
                        Console.WriteLine($"{i}. {Enum.GetName(typeof(MainMenu), i).Replace("_", " ")}");
                    }

                    int nr;
                    MainMenu menu = (MainMenu)99; //Default
                    if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(MainMenu)).Length - 1)
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
                            Queries.Search(c);
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
                    if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(LogIn)).Length)
                    {
                        Helpers.WrongInput();
                    }
                    else
                    {
                        login = (LogIn)nr;
                        Console.Clear();
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
                    if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(BrowseShop)).Length - 1)
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
                        case BrowseShop.Products:
                            View.ShowCategories();
                            View.ShowProductInOneCategory(c);
                            Console.ReadKey();
                            browseShop = false;
                            break;
                        case BrowseShop.Shoppingcart:
                            View.ShoppingCart(c);                       //Checkout klar, måste cleara och skit + frakt osv
                            browseShop = false;
                            break;
                        case BrowseShop.Profile:
                            View.CustomerProfile(c);
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
                    if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(Admin)).Length - 1)
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
                            Show("AdminCustomers", c);
                            Console.ReadKey();
                            adminMenu = false;
                            break;
                        case Admin.Edit_Products:
                            Show("AdminProducts", c);
                            Console.ReadKey();
                            adminMenu = false;
                            break;
                        case Admin.Edit_Highlighted_Products:
                            Show("AdminHighLightedProducts", c);
                            break;
                        case Admin.Queries:
                            Queries.AllQueries(c);
                            adminMenu = false;
                            break;
                        case Admin.Return:
                            Show("Main", c);
                            adminMenu = false;
                            break;

                    }
                }
            }
            if (value == "AdminProducts")               //Fixade lite admin menus
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
                    if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(AdminProducts)).Length - 1)
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
            if (value == "AdminCustomers")
            {
                int input = 0;
                using (var db = new WebShopContext())
                {
                    int counter = 0;
                    bool checkUser = true;
                    var customerList = db.Customers.ToList();
                    int padValue1 = 6;
                    int padValue2 = 14;
                    int padValue3 = 25;
                    Console.WriteLine("Nr".PadRight(padValue1) + "Username".PadRight(padValue2) + "First Name".PadRight(padValue2) +
                        "Last Name".PadRight(padValue2) + "Email".PadRight(padValue3) + "Street".PadRight(padValue3) + "Postal Code".PadRight(padValue2) +
                        "City".PadRight(padValue2) + "Phone".PadRight(padValue2) + "Country".PadRight(padValue1));
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
                    foreach (var customer in customerList)
                    {
                        counter++;
                        Console.WriteLine(counter.ToString().PadRight(padValue1) + customer.UserName.PadRight(padValue2) +
                            customer.FirstName.PadRight(padValue2) + customer.LastName.PadRight(padValue2) + customer.Email.PadRight(padValue3) +
                            customer.Street.PadRight(padValue3) + customer.PostalCode.ToString().PadRight(padValue2) + customer.City.PadRight(padValue2) +
                            customer.Phone.ToString().PadRight(padValue2) + customer.Country.PadRight(padValue1));
                    }
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("Enter the ID of the customer you wish you edit: ");
                    while (checkUser)
                    {

                        input = Helpers.TryNumber(input, counter, 1);

                        var pIdExist = db.Customers.Where(x => x.UserName == customerList[input].UserName) != null;
                        if (pIdExist)
                        {
                            checkUser = false;
                        }
                        else
                        {
                            Console.WriteLine("User does not exist, try again");
                        }


                    }
                    input = customerList[input - 1].Id;
                    Console.Clear();
                    Methods.Admin.OneCustomer(input);
                }
                Methods.Admin.UpdateCustomer(input);


            }
            if (value == "AdminHighLightedProducts")
            {

                _highlightedProdsId = Methods.Admin.SetHiglightedProducts(_highlightedProdsId);
                

            }
            return c;
        }
    }
}
