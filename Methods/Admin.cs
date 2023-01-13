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

namespace Webshop.Methods
{
    internal class Admin
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog = MyWebShop; persist security info = True; Integrated Security = True;";

        internal static void RemoveProduct(int pId)  // lägga till product.Name
        {
            Console.WriteLine("Are you sure you want to delete? y/ n");
            string? answer = Console.ReadLine();
            if (answer == "y")
            {
                var sql = $"DELETE FROM Products WHERE Id={pId}";
                using (var connection = new SqlConnection(connString))
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
                string? productName = Console.ReadLine();
                Console.WriteLine("Enter the price of the product.");
                int price = 0;
                price = Helpers.TryNumber(price, 999999999, 1);
                Console.WriteLine("Enter the amount you have in stock.");
                int stock = 0;
                stock = Helpers.TryNumber(stock, 999999999, 0);
                Console.WriteLine("Enter a description of the product.");
                string? description = Console.ReadLine();
                View.ShowCategories();
                Console.WriteLine("Enter the catergory the product belongs to.");
                int category = 0;
                category = Helpers.TryNumber(category, categoryList.Count(), 1);
                View.ShowSupplier();
                Console.WriteLine("Enter the id of the supplier.");
                int supplier = 0;
                supplier = Helpers.TryNumber(supplier, supplierList.Count(), 1);
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
                    genre = Helpers.TryNumber(genre, genrelist.Count(), 1);

                    var sql = $"INSERT INTO GenreProduct Values({genre},{idOfLastProd})";
                    using (var connection = new SqlConnection(connString))
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
                using (var connection = new SqlConnection(connString))
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
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    connection.Execute(sql);
                    connection.Close();
                }
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
                //var lastproduct = prodList.ToList().LastOrDefault();
                //int maxValue = 0;
                //if (lastproduct != null)
                //{
                //    maxValue = lastproduct.Id;
                //}
                //else
                //{
                //    Helpers.WrongInput();
                //}


                Console.WriteLine();
                Console.Write("Enter id of the category you wish to browse: ");
                categoryId = Helpers.TryNumber(categoryId, catList.Count(), 1);
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
                Console.WriteLine("Which property would you like to edit?");
                Console.WriteLine("1. Name\n2. Price\n3. Units in stock\n4. Description\n5. Supplier\n6. Remove\n7. Return");
                number = Helpers.TryNumber(number, 6, 1);
                Console.Clear();
                switch (number)
                {
                    case 1:
                        UpdateName(pId);
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
                    case 6:
                        RemoveProduct(pId);
                        Console.Clear();
                        OneProduct(pId, categoryId);
                        Console.Clear();
                        Helpers.PressAnyKey();
                        break;
                }
                Menus.Show("AdminProducts", c);
            }
            else
            {
                Console.WriteLine("hej customer");
            }


        }

        private static void UpdateName(int pId)
        {
            Console.Write("\nNew name: ");
            string? newName = Helpers.CheckStringInput();

            var sql = $"UPDATE Products SET Name = '{newName}' WHERE Id = '{pId}'";
            using (var connection = new SqlConnection(connString))
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
                var pIdExist = database.Products.Where(x => x.Id == pId) != null;
                if (pIdExist)
                {
                    var productList = database.Products.Where(x => x.Id == pId && x.CategoryId == categoryId).ToList();

                    Console.WriteLine("Id\tName\tPrice\tUnits in stock\tDescription\tSupplier Id");
                    foreach (var p in productList)
                    {
                        Console.WriteLine($"{p.Id}\t{p.Name}\t{p.Price}\t{p.UnitsInStock}\t{p.Description}\t{p.SupplierId}");
                    }
                }
                else
                {
                    Console.WriteLine("No product with the corresponding ID");
                }
            }
        }
    }
}
