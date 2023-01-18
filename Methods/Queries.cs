using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;
using Webshop.Dapper;

namespace Webshop.Methods
{
    public class Queries
    {
        enum ShowQueries
        {
            Most_Popular_Product = 1,
            Most_Popular_Category,
            Genre_With_Most_Games,
            Total_Orders_Placed,
            Most_Used_Shipper,
            Revenue,
            Return = 0
        }

        internal static void AllQueries(Customer c)
        {
            bool queryMenu = true;
            while (queryMenu)
            {
                foreach (int i in Enum.GetValues(typeof(ShowQueries)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(ShowQueries), i).Replace("_", " ")}");
                }

                int nr;
                ShowQueries menu = (ShowQueries)99; //Default
                if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr) || nr > Enum.GetNames(typeof(ShowQueries)).Length - 1)
                {
                    Helpers.WrongInput();
                }
                else
                {
                    menu = (ShowQueries)nr;
                    Console.Clear();

                }
                switch (menu)
                {
                    case ShowQueries.Most_Popular_Product:
                        GetPopularProducts();
                        break;
                    case ShowQueries.Most_Popular_Category:
                        GetMostPopularCategory();
                        break;
                    case ShowQueries.Genre_With_Most_Games:
                        GetGenreWithMostProducts();
                        break;
                    case ShowQueries.Total_Orders_Placed:
                        GetTotalOrdersPlaced();
                        break;
                    case ShowQueries.Most_Used_Shipper:
                        GetShipperWithMostShipments();
                        break;
                    case ShowQueries.Revenue:
                        GetRevenueForTheBoys();
                        break;
                    case ShowQueries.Return:
                        Menus.Show("AdminMenu", c);
                        break;
                }
            }
        }

        internal static List<MostUsedShipper> GetShipperWithMostShipments()
        {
            var sql = "SELECT top 1 s.ShipVia as Name, count(od.ProductId) as UsedTimes FROM Orders o\r\njoin ShipChoices s on s.Id = o.ShipChoiceId\r\njoin OrderDetails od on od.OrderId = o.Id\r\nwhere o.Purchased = 1\r\ngroup by s.ShipVia\r\norder by UsedTimes Desc";

            var shipperList = new List<MostUsedShipper>();

            using (var connection = new SqlConnection(Admin._connString))
            {
                connection.Open();
                shipperList = connection.Query<MostUsedShipper>(sql).ToList();
                connection.Close();

            }

            foreach (var p in shipperList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(p.Name + " has shipped the most products with " + p.UsedTimes + " products shipped!");
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");

            return shipperList;
            Console.ReadKey();
        }

        internal static List<TotalOrdersPlaced> GetTotalOrdersPlaced()
        {
            var sql = "SELECT count(o.Id) AS OrderCount FROM Orders o";

            var orderList = new List<TotalOrdersPlaced>();

            using (var connection = new SqlConnection(Admin._connString))
            {
                connection.Open();
                orderList = connection.Query<TotalOrdersPlaced>(sql).ToList();
                connection.Close();

            }

            foreach (var p in orderList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Total orders = " + p.OrderCount);
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");
            return orderList;
            Console.ReadKey();
        }

        internal static List<GenreWithMostGames> GetGenreWithMostProducts()
        {
            var sql = "SELECT top 1 g.Name AS Name, count(p.Id) as NrOfProducts FROM OrderDetails od\r\njoin Orders o on o.Id = od.OrderId\r\njoin Products p on p.Id = od.ProductId\r\njoin Categories c on c.Id = p.CategoryId\r\njoin GenreProduct gp on gp.ProductsId = p.Id\r\njoin Genres g on gp.GenresId = g.Id\r\nwhere o.Purchased = 1\r\ngroup by g.Name\r\norder by NrOfProducts Desc";

            var genreList = new List<GenreWithMostGames>();

            using (var connection = new SqlConnection(Admin._connString))
            {
                connection.Open();
                genreList = connection.Query<GenreWithMostGames>(sql).ToList();
                connection.Close();

            }

            foreach (var p in genreList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Our most popular genre is " + p.Name + " with " + p.NrOfProducts + " sold");
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");
            return genreList;
            Console.ReadKey();
        }

        internal static List<MostPopularCategory> GetMostPopularCategory()
        {
            var sql = "SELECT top 1 c.Name AS Name, count(p.Id) as NrOffProducts FROM OrderDetails od\r\njoin Orders o on o.Id = od.OrderId\r\njoin Products p on p.Id = od.ProductId\r\njoin Categories c on c.Id = p.CategoryId\r\nwhere o.Purchased = 1\r\ngroup by c.Name\r\norder by NrOffProducts Desc";

            var categoryList = new List<MostPopularCategory>();

            using (var connection = new SqlConnection(Admin._connString))
            {
                connection.Open();
                categoryList = connection.Query<MostPopularCategory>(sql).ToList();
                connection.Close();

            }

            foreach (var p in categoryList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Webshops most popular category is " + p.Name + " with " + p.NrOffProducts + " products sold.");
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");
            return categoryList;
            Console.ReadKey();
        }

        internal static List<PopularProducts> GetPopularProducts()
        {
            var sql = "SELECT SUM(Quantity) AS Sold,P.Name\r\nFrom OrderDetails As OD\r\nJOIN Products As P On P.Id =Od.ProductId\r\nJoin Orders As O On O.Id=OD.OrderId\r\n\r\n\r\nGROUP BY P.Name\r\nORDER BY SOLD DESC";

            var productList = new List<PopularProducts>();

            using (var connection = new SqlConnection(Admin._connString))
            {
                connection.Open();
                productList = connection.Query<PopularProducts>(sql).ToList();
                connection.Close();

            }

            foreach (var p in productList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(p.Name + " " + p.Sold);
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");
            return productList;
            Console.ReadKey();
        }
        internal static List<RevenueForTheBoys> GetRevenueForTheBoys()
        {
            var sql = "SELECT \r\n    SUM(UnitPrice * Quantity / 4) as TotalMoneySpent \r\n  FROM OrderDetails";

            var reveneue = new List<RevenueForTheBoys>();

            using (var connection = new SqlConnection(Admin._connString))
            {
                connection.Open();
                reveneue = connection.Query<RevenueForTheBoys>(sql).ToList();
                connection.Close();

            }

            foreach (var p in reveneue)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Revenue for the boys: " + "$" + p.TotalMoneySpent);
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");
            return reveneue;
            Console.ReadKey();
        }

        internal static List<Search> Search(Customer c)
        {
            {
                Console.Write("Search: ");
                var search = new List<Search>();
                string value = Helpers.CheckStringInput();
                var sql = $"SELECT p.Name as ProductName, c.Name as CategoryName, p.CategoryId As CategoryId, p.Id As ProductId FROM Products p\r\njoin Categories c on c.Id = p.CategoryId\r\nWHERE p.Name Like '%{value}%'";
                int i = 0;
                int answer = 0;
                using (var connection = new SqlConnection(Admin._connString))
                {
                    connection.Open();
                    search = connection.Query<Search>(sql).ToList();
                    connection.Close();

                }                
                if(search.Count > 0)
                {
                    Console.WriteLine("I found these results:");
                    foreach (var p in search)
                    {
                        i++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("ID " + i + ". Product name: " + p.ProductName + " is listed under " + p.CategoryName);
                        Console.ResetColor();

                        Console.WriteLine("______________________________________________________________________");
                        Console.WriteLine("1. Visit product page.\n2. Go back");
                        answer = Helpers.TryNumber(answer, 2, 1);
                        int selectedProduct = 0;

                        if (answer == 1)
                        {
                            Console.Write("Enter id of the product you want to go to:");
                            selectedProduct = Helpers.TryNumber(selectedProduct, search.Count(), 1);
                            Console.Clear();
                            Admin.OneProduct(search[selectedProduct - 1].ProductId, search[selectedProduct - 1].CategoryId);

                            Helpers.AddProductToCart(search[selectedProduct - 1].ProductId, c);
                        }
                        else if (answer == 2)
                        {
                            Menus.Show("Main", c);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No result found");
                    Thread.Sleep(1000);
                    Menus.Show("Main", c);
                }
                

                Console.WriteLine("\n\n\n");
                Console.ReadKey();
                return search;
            }
        }
    }
}
