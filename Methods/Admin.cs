using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.Methods;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Webshop.Methods
{
    internal class Admin
    {
        public static void AddProduct()
        {






        }
        public static void CreateProduct()
        {
            using (var database = new WebShopContext())
            {
                //workaround for hardcoded values
                var categoryList = database.Categories.ToList();
                var supplierList = database.Suppliers.ToList();
                Console.WriteLine("Input name of the product");
                string productName = Console.ReadLine();                
                Console.WriteLine("Enter the price of the product.");
                int price = 0;
                price = Helpers.TryNumber(price, 999999999, 1);
                Console.WriteLine("Enter the amount you have in stock.");
                int stock = 0;
                stock = Helpers.TryNumber(stock, 999999999, 0);
                Console.WriteLine("Enter a description of the product.");
                string description = Console.ReadLine();
                Helpers.ShowCategoryId();
                Console.WriteLine("Enter the catergory the product belongs to.");
                int category = 0;
                category = Helpers.TryNumber(category, categoryList.Count(), 1);
                Helpers.ShowSupplier();
                Console.WriteLine("Enter the id of the supplier.");
                int supplier = 0;
                supplier = Helpers.TryNumber(supplier, supplierList.Count(), 1);

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
                AddGenresToProduct(productName);
            }

        }
        public static void AddGenresToProduct(string name)
        {
            string connString = "data source=.\\SQLEXPRESS; initial catalog = MyWebShop; persist security info = True; Integrated Security = True;";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                var getProductId = $"SELECT Id FROM [dbo].[Products] WHERE Name LIKE '{name}' ";
                //var getName = $"SELECT NAME FROM [dbo].[Products] WHERE Id = {getProductId} ";

                Console.WriteLine("Enter the genre id.");               
                bool enterGenre = true;
               
                while(enterGenre)
                {                    
                    Helpers.ShowGenres();
                    Console.WriteLine("What genre do you like to add to " + name);
                    int genre = 0;
                    genre = Helpers.TryNumber(genre, int.Parse(getProductId), 1);
                    string sqlCode = $"INSERT INTO [dbo].[GenreProduct] (GenreId, ProductId) VALUES ({genre}, {getProductId}) ";


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
    }
}
