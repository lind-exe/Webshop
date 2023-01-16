using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.Methods;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using Dapper;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Xml.Linq;
using System.Numerics;
using System.Net;
using System.Net.Mail;

namespace Webshop.Methods
{
    internal class Admin
    {
        static readonly string _connString = "data source=.\\SQLEXPRESS; initial catalog = MyWebShop; persist security info = True; Integrated Security = True;";

        internal static void RemoveProduct(int pId)  // lägga till product.Name
        {
            Console.WriteLine("Are you sure you want to delete? y/ n");
            string? answer = Console.ReadLine();
            if (answer == "y")
            {
                var sql = $"DELETE FROM Products WHERE Id={pId}";
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                    connection.Close();
                }
            }
        }
        public static void AddProduct()
        {
            using (var database = new WebShopContext())
            {
                //workaround for hardcoded values
                var categoryList = database.Categories.ToList();
                var supplierList = database.Suppliers.ToList();
                Console.WriteLine("Input name of the product");
                string? productName = Helpers.CheckStringInput();
                Console.WriteLine("Enter the price of the product.");
                int price = 0;
                price = Helpers.TryNumber(price, 999999999, 1);
                Console.WriteLine("Enter the amount you have in stock.");
                int stock = 0;
                stock = Helpers.TryNumber(stock, 999999999, 0);
                Console.WriteLine("Enter a description of the product.");
                string? description = Helpers.CheckStringInput();
                View.ShowCategories();
                Console.WriteLine("Enter the catergory the product belongs to.");
                int category = 0;
                category = Helpers.TryNumber(category, categoryList.Count, 1);
                View.ShowSupplier();
                Console.WriteLine("Enter the id of the supplier.");
                int supplier = 0;
                supplier = Helpers.TryNumber(supplier, supplierList.Count, 1);
                Console.Clear();
                var newProduct = new Product
                {
                    Name = productName,
                    Price = price,
                    UnitsInStock = stock,
                    Description = description,
                    CategoryId = category,
                    SupplierId = supplier
                };

                database.Add(newProduct);
                database.SaveChanges();

                AddGenreToProduct();
            }

        }
        public static void AddGenreToProduct()
        {
            bool run = true;
            using (var database = new WebShopContext())
            {
                var genrelist = database.Genres.ToList();
                var productlist = database.Products;
                var lastproduct = productlist.ToList().LastOrDefault();
                int idOfLastProd = 0;
                if (lastproduct != null)
                {
                    idOfLastProd = lastproduct.Id;
                }
                else
                {
                    Helpers.WrongInput();
                }

                while (run)
                {
                    View.ShowGenres();
                    Console.WriteLine("Add genre to the product.");
                    int genre = 0;
                    genre = Helpers.TryNumber(genre, genrelist.Count, 1);

                    var sql = $"INSERT INTO GenreProduct Values({genre},{idOfLastProd})";
                    using (var connection = new SqlConnection(_connString))
                    {
                        connection.Open();
                        connection.Execute(sql);
                        connection.Close();
                    }
                    Console.WriteLine("Would you like to enter another genre?\n1. Yes\n2. No");
                    int answer = 0;
                    answer = Helpers.TryNumber(answer, 2, 1);
                    if (answer == 1)
                    {
                        run = true;
                        Console.Clear();
                    }
                    else
                    {
                        run = false;
                        Helpers.PressAnyKey();
                    }
                }
            }
        }
        public static void InsertGenre()
        {
            Console.Write("Enter name of new genre: ");
            using (var database = new WebShopContext())
            {
                var newGenre = new Genre()
                {
                    Name = Console.ReadLine()
                };
                database.Add(newGenre);
                database.SaveChanges();
            }
        }
        public static void InsertCategory()
        {
            Console.Write("Enter name of new category: ");
            using (var database = new WebShopContext())
            {
                var newCategory = new Category()
                {
                    Name = Console.ReadLine()
                };
                database.Add(newCategory);
                database.SaveChanges();
            }
        }
        public static void InsertPaymentMethod()
        {
            Console.Write("Enter name of new Payment Method: ");
            using (var database = new WebShopContext())
            {
                var newPayment = new PaymentMethod()
                {
                    PayVia = Console.ReadLine()
                };
                database.Add(newPayment);
                database.SaveChanges();
            }
        }
        public static void InsertNewShipper()
        {
            Console.Write("Enter name of new shipper: ");
            using (var database = new WebShopContext())
            {
                var newShipper = new ShipChoice()
                {
                    ShipVia = Console.ReadLine()
                };
                database.Add(newShipper);
                database.SaveChanges();
            }
        }
        public static void UpdatePrice(int id)
        {
            using (var database = new WebShopContext())
            {
                Console.Write("Enter the new price of the product:");
                int price = 0;
                price = Helpers.TryNumber(price, 999999999, 1);
                var sql = $"UPDATE dbo.Products\r\nSET Price={price}\r\nWHERE ID={id}";
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                    connection.Close();
                }
            }
        }
        public static void UpdateUnitsInStock(int id)
        {
            using (var database = new WebShopContext())
            {

                Console.Write("Enter the new inventory balance of the product:");
                int inventorybalance = 0;
                inventorybalance = Helpers.TryNumber(inventorybalance, 999999999, 1);
                var sql = $"UPDATE dbo.Products\r\nSET UnitsInStock={inventorybalance}\r\nWHERE ID={id}";
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                    connection.Close();
                }
            }
        }
        private static void UpdateSupplier(int pId)
        {
            using (var database = new WebShopContext())
            {
                var supplierList = database.Suppliers.ToList();
                View.ShowSupplier();
                Console.Write("Enter Id of new supplier?");
                int newSupplierId = 0;
                newSupplierId = Helpers.TryNumber(newSupplierId, supplierList.Count, 1);

                var sql = $"UPDATE Products SET SupplierId = '{newSupplierId}' WHERE Id = '{pId}'";
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                    connection.Close();
                }
            }
        }

        private static void UpdateDescription(int pId)
        {
            Console.Write("\nNew Description: ");
            string? newDescription = Helpers.CheckStringInput();

            var sql = $"UPDATE Products SET Description = '{newDescription}' WHERE Id = '{pId}'";
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                connection.Execute(sql);
                connection.Close();
            }
        }

        internal static void ChosenCategory(Customer c)   //lägga till return
        {
            View.ShowCategories();
            using (var database = new WebShopContext())
            {
                var catList = database.Categories.ToList();
                var prodList = database.Products.ToList();
                int categoryId = 0;

                Console.WriteLine();
                Console.WriteLine("Enter id of the category you wish to browse: ");
                categoryId = Helpers.TryNumber(categoryId, catList.Count, 1);
                Console.Clear();
                int productId = View.ProductsInCategory(categoryId);

                Console.WriteLine();
                ChosenProduct(productId, categoryId, c);
            }
        }
        internal static void ChosenProduct(int pId, int categoryId, Customer c)
        {
            int number = 0;
            OneProduct(pId, categoryId);
            if (c.UserName == "admin")
            {
                Console.WriteLine("\n\n\nWhich property would you like to edit?");
                Console.WriteLine("1. Name\n2. Price\n3. Units in stock\n4. Description\n5. Supplier\n6. Remove\n7. Return");
                number = Helpers.TryNumber(number, 7, 1);
                Console.Clear();
                switch (number)
                {
                    case 1:
                        UpdateProductName(pId);
                        Console.Clear();
                        OneProduct(pId, categoryId);
                        Helpers.PressAnyKey();
                        break;
                    case 2:
                        UpdatePrice(pId);
                        Console.Clear();
                        OneProduct(pId, categoryId);
                        Helpers.PressAnyKey();
                        break;
                    case 3:
                        UpdateUnitsInStock(pId);
                        Console.Clear();
                        OneProduct(pId, categoryId);
                        Console.Clear();
                        Helpers.PressAnyKey();
                        break;
                    case 4:
                        UpdateDescription(pId);
                        Console.Clear();
                        OneProduct(pId, categoryId);
                        Helpers.PressAnyKey();
                        break;
                    case 5:
                        UpdateSupplier(pId);
                        Console.Clear();
                        OneProduct(pId, categoryId);
                        Helpers.PressAnyKey();
                        break;
                    case 6:
                        RemoveProduct(pId);
                        Console.Clear();
                        OneProduct(pId, categoryId);
                        Helpers.PressAnyKey();
                        break;
                    case 7:
                        Console.Clear();
                        Menus.Show("AdminProducts", c);
                        break;
                }
                Menus.Show("AdminProducts", c);
            }
            else
            {
                Helpers.PressAnyKey();
            }


        }

        private static void UpdateProductName(int pId)
        {
            Console.Write("\nNew name: ");
            string? newName = Helpers.CheckStringInput();

            var sql = $"UPDATE Products SET Name = '{newName}' WHERE Id = '{pId}'";
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                connection.Execute(sql);
                connection.Close();
            }
        }

        internal static void OneProduct(int pId, int categoryId)
        {
            using (var database = new WebShopContext())
            {
                int padValue1 = 15;
                int padValue2 = 20;
                var pIdExist = database.Products.Where(x => x.Id == pId) != null;
                if (pIdExist)
                {
                    var productList = database.Products.Where(x => x.Id == pId && x.CategoryId == categoryId).ToList();

                    Console.WriteLine("Id".PadRight(padValue1) + "Name".PadRight(30) + "Price".PadRight(padValue1) +
                        "Units in stock".PadRight(padValue2) + "Description".PadRight(padValue2) + "Supplier Id".PadRight(padValue1));
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                    foreach (var p in productList)
                    {
                        Console.WriteLine(p.Id.ToString().PadRight(padValue1) + p.Name.PadRight(30) + p.Price.ToString().PadRight(padValue1) +
                            p.UnitsInStock.ToString().PadRight(padValue2) + p.Description.PadRight(padValue2) + p.SupplierId.ToString().PadRight(padValue1));
                    }
                }
                else
                {
                    Console.WriteLine("No product with the corresponding ID");
                }
            }
        }

        internal static void OneCustomer(int cId)
        {
            using (var database = new WebShopContext())
            {
                var customerList = database.Customers.Where(x => x.Id == cId);

                int padValue1 = 6;
                int padValue2 = 14;
                int padValue3 = 25;

                Console.WriteLine("Id".PadRight(padValue1) + "Username".PadRight(padValue2) + "First Name".PadRight(padValue2) +
                    "Last Name".PadRight(padValue2) + "Email".PadRight(padValue3) + "Street".PadRight(padValue3) + "Postal Code".PadRight(padValue2) +
                    "City".PadRight(padValue2) + "Phone".PadRight(padValue2) + "Country".PadRight(padValue1));
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var customer in customerList)
                {
                    Console.WriteLine(customer.Id.ToString().PadRight(padValue1) + customer.UserName.PadRight(padValue2) +
                        customer.FirstName.PadRight(padValue2) + customer.LastName.PadRight(padValue2) + customer.Email.PadRight(padValue3) +
                        customer.Street.PadRight(padValue3) + customer.PostalCode.ToString().PadRight(padValue2) + customer.City.PadRight(padValue2) +
                        customer.Phone.ToString().PadRight(padValue2) + customer.Country.PadRight(padValue1));
                }
                Console.WriteLine("\n\n");
            }
        }

        internal static void UpdateCustomer(int cId)
        {
            foreach (int i in Enum.GetValues(typeof(Menus.CustomerEdit)))
            {
                Console.WriteLine($"{i}. {Enum.GetName(typeof(Menus.CustomerEdit), i).Replace("_", " ")}");
            }

            int nr;
            Menus.CustomerEdit customer = (Menus.CustomerEdit)99; //Default
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(Menus.CustomerEdit)).Length - 1)
            {
                customer = (Menus.CustomerEdit)nr;
                Console.Clear();
            }
            else
            {
                Helpers.WrongInput();
            }
            switch (customer)
            {
                case Menus.CustomerEdit.User_Name:
                    UpdateUserString("UserName", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.First_Name:
                    UpdateUserString("FirstName", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.Last_Name:
                    UpdateUserString("LastName", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.Country:
                    UpdateUserString("Country", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.City:
                    UpdateUserString("City", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.Street:
                    UpdateUserString("Street", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.Postal:
                    UpdateUserInt("PostalCode", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.Phone:
                    UpdateUserInt("Phone", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.Email:
                    UpdateUserString("Email", cId);
                    OneCustomer(cId);
                    Helpers.PressAnyKey();
                    Console.Clear();
                    break;
                case Menus.CustomerEdit.Return:
                    Console.WriteLine("Press any key to return");
                    break;


            }
        }
        public static void UpdateUserString(string property, int cId)
        {
            Console.WriteLine("Enter new value for " + property + ": ");
            string value = Helpers.CheckStringInput();

            using (var database = new WebShopContext())
            {
                var sql = $"UPDATE Customers\r\nSET {property}='{value}'\r\nWHERE ID={cId}";
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                    connection.Close();
                }
            }
        }
        public static void UpdateUserInt(string property, int cId)
        {

            Console.WriteLine("Enter new value for " + property + ": ");
            int value = 0;
            switch (property)
            {
                case "Phone":
                    value = Helpers.TryNumber(value, 999999999, 99999999);
                    break;
                case "PostalCode":
                    value = Helpers.TryNumber(value, 99999, 10000);
                    break;
                case "Age":
                    value = Helpers.TryNumber(value, 100, 15);
                    break;
            }
            using (var database = new WebShopContext())
            {
                var sql = $"UPDATE Customers\r\nSET {property}={value}\r\nWHERE ID={cId}";
                using (var connection = new SqlConnection(_connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                    connection.Close();
                }
            }
        }
        public static void SendEmail(string fromAddress, string password)
        {
            using SmtpClient email = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(fromAddress, password),
            };

            string subject = "Password recovery";
            string body = "The password for your account is : {passWord}";
            try
            {
                Console.WriteLine("Sending email *************");
                email.Send(fromAddress, ToAddress(), subject, body);
                Console.WriteLine("Email sent *********");
            }
            catch
            {

            }
        }
        public static string GetUserName()
        {
            return "lindjonathan95@gmail.com";
        }
        public static string GetPassword()
        {
            return "password_";
        }
        public static string ToAddress()
        {
            return "jojje_lind@live.se";
        }

        public static void SetHiglightedProducts(View v)
        {
            using (var db = new WebShopContext())
            {
                var prodList = db.Products.ToList();

                bool continueChoosing = true;
                int padValue = 12;
                int padValue1 = 24;
                int padValue2 = 16;
                int i = 0;
                var highestIdNrOfProduct = 99;
                try { highestIdNrOfProduct = prodList.LastOrDefault().Id; }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                while (continueChoosing)
                {
                    int pos = 0;
                    int prodToHighlight = 0;

                    Console.Clear();
                    Console.SetCursorPosition(0, 17);
                    var productName1 = prodList.SingleOrDefault(x => x.Id == v.Prod1Id);
                    var productName2 = prodList.SingleOrDefault(x => x.Id == v.Prod2Id);
                    var productName3 = prodList.SingleOrDefault(x => x.Id == v.Prod3Id);
                    Console.WriteLine("Position 1: " + v.Prod1Id + "\t" + productName1.Name + "\nPosition 2: " + v.Prod2Id + "\t" + productName2.Name + 
                        "\nPosition 3: " + v.Prod3Id + "\t" + productName3.Name);
                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine("Nr".PadRight(padValue) + "Name".PadRight(padValue1) + "Price".PadRight(padValue) + "Units In Stock".PadRight(padValue));
                    Console.WriteLine("--------------------------------------------------------------");
                    for (i = 0; i < prodList.Count; i++)
                    {
                        Console.WriteLine((i + 1).ToString().PadRight(padValue) + prodList[i].Name.PadRight(padValue1) + prodList[i].Price.ToString().PadRight(padValue2) +
                            prodList[i].UnitsInStock.ToString().PadRight(padValue2));
                    }

                    Console.Write("\n\n\nChoose product to higlight: ");
                    prodToHighlight = Helpers.TryNumber(prodToHighlight, i, 1);
                    prodToHighlight = prodList[prodToHighlight - 1].Id;
                    Console.Write("Which position should it have: ");
                    pos = Helpers.TryNumber(pos, 3, 1);
                    switch (pos)
                    {
                        case 1:
                            v.Prod1Id = prodToHighlight;
                            break;
                        case 2:
                            v.Prod2Id = prodToHighlight;
                            break;
                        case 3:
                            v.Prod3Id = prodToHighlight;
                            break;
                    }
                    int quit = 0;
                    Console.WriteLine("1. Continue choosing\n2. Quit");
                    quit = Helpers.TryNumber(quit, 2, 1);
                    switch (quit)
                    {
                        case 1:
                            break;
                        case 2:
                            continueChoosing = false;
                            break;
                    }
                }
            }
        }
    }
}
