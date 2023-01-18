using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace Webshop.Methods
{
    public class HardCodedValues
    {
        public static void AllInserts()
        {
            InsertGenres();
            InsertSuppliers();
            InsertShipChoices();
            InsertCategories();
            InsertPaymentMethods();
            InsertCustomers();
            //InsertProducts();
        }
        private static void InsertGenres()
        {
            using (var db = new WebShopContext())
            {
                db.AddRange(
                    new Genre() { Name = "Action" },
                    new Genre() { Name = "JRPG" },
                    new Genre() { Name = "RPG" },
                    new Genre() { Name = "Survival" },
                    new Genre() { Name = "Shooter" },
                    new Genre() { Name = "Open World" },
                    new Genre() { Name = "Dungeon Crawler" },
                    new Genre() { Name = "Platformer" },
                    new Genre() { Name = "MMORPG" },
                    new Genre() { Name = "Adventure" },
                    new Genre() { Name = "Strategy" },
                    new Genre() { Name = "Fighting" },
                    new Genre() { Name = "Rhythm" },
                    new Genre() { Name = "Battle Royale" },
                    new Genre() { Name = "Horror" },
                    new Genre() { Name = "Simulation" },
                    new Genre() { Name = "Racing" },
                    new Genre() { Name = "Social Simulation" },
                    new Genre() { Name = "Wargame" },
                    new Genre() { Name = "Monster Tamer" },
                    new Genre() { Name = "Sandbox" },
                    new Genre() { Name = "Coding Game" },
                    new Genre() { Name = "None"}

                );
                db.SaveChanges();
            }            
        }
        private static void InsertSuppliers()
        {
            using (var db = new WebShopContext())
            {
                string[] values = (
                    "Blizzard Entertainment, Capcom, CD Projekt Red, Certain Affinity," +
                    " Chunsoft, Core Design, Electronic Arts," +
                    " Game Freak, Ganbarion, Grezzo, Guerrilla Games," +
                    " Harmonix Music Systems, HeroCraft, Infinity Ward, Activision," +
                    " LucasArts, Maxis, MicroProse Software, Mojang AB, NetEase, Neversoft," +
                    " Nintendo, Microsoft Studios, Project Soul," +
                    " PlatinumGames, Respawn Entertainment," +
                    " Square Enix, Ubisoft, Sony "
                    ).Split(", ");
                var resultList = new List<Supplier>();


                for (int i = 0; i < values.Length; i++)
                {
                    resultList.Add(new Supplier() { Name = values[i] });
                }
                db.AddRange(resultList);
                db.SaveChanges();
            }
        }
        private static void InsertShipChoices()
        {
            using (var db = new WebShopContext())
            {
                string[] values = "PostMord, DHL, Budbee, MickeFreightSolutions".Split(", ");
                int[] prices = { 79, 59, 199, 99 };
                string[] deliveryTimes = "1-3 days, 5-7 days, 1 day, IDD™ (Instant Drone Delivery)".Split(", ");
                var resultList = new List<ShipChoice>();


                for (int i = 0; i < values.Length; i++)
                {
                    resultList.Add(new ShipChoice() 
                    { 
                        ShipVia = values[i],
                        ShipPrice = prices[i],
                        DeliveryTime = deliveryTimes[i]
                    });
                }
                db.AddRange(resultList);
                db.SaveChanges();
            }
        }
        private static void InsertCustomers()
        {
            using (var db = new WebShopContext())
            {
                string[] userNames = "admin, Gradde, JLind, Chrillo, Andreas".Split(", ");
                string[] passwords = "admin, gradde, jlind, chrillo, anden".Split(", ");
                string[] firstNames = "admin, Mattias, Jonathan, Christoffer, Andreas".Split(", ");
                string[] lastNames = "admin, Gradin, Lind, Gustafsson, Tollmar".Split(", ");
                int[] ages = { 30, 36, 27, 28, 31 };
                string[] countries = "Sweden, Sweden, Sweden, Sweden, Sweden".Split(", ");
                string[] cities = "Nyköping, Nyköping, Nyköping, Nyköping, Nyköping".Split(", ");
                string[] streets = "Campus Nyköping 1, Oppeby Gård 181, Mejerivägen 11A, Regeringsvägen 8c, Borgaregatan 13A".Split(", ");
                int[] postalCodes = { 61140, 61155, 61156, 61156, 61130 };;
                int[] phones = { 0701234567, 0731234567, 0761234567, 0707654321, 0737654321 };;
                string[] emails = "admin@mail.se, gradde@mail.se, jlind@mail.se, chrillo@mail.se, andreas@mail.se".Split(", ");


                var resultList = new List<Customer>();


                for (int i = 0; i < userNames.Length; i++)
                {
                    resultList.Add(new Customer()
                    {
                        UserName = userNames[i],
                        Password = passwords[i],
                        FirstName = firstNames[i],
                        LastName = lastNames[i],
                        Age = ages[i],
                        Country = countries[i],
                        City = cities[i],
                        Street = streets[i],
                        PostalCode = postalCodes[i],
                        Phone = phones[i],
                        Email = emails[i],
                    });
                }
                db.AddRange(resultList);
                db.SaveChanges();
            }
        }
        private static void InsertPaymentMethods()
        {
            using (var db = new WebShopContext())
            {
                string[] values = "Swish, Klarna, Bank transfer, Bananas".Split(", ");
                var resultList = new List<PaymentMethod>();


                for (int i = 0; i < values.Length; i++)
                {
                    resultList.Add(new PaymentMethod() { PayVia = values[i] });
                }
                db.AddRange(resultList);
                db.SaveChanges();
            }
        }
        private static void InsertCategories()
        {
            using (var db = new WebShopContext())
            {
                string[] values = "Playstation 5, Playstation 4, Xbox Series X, Nintendo Switch, PC, TI-84 Plus Calculator, Merchandise, Accessories".Split(", ");
                var resultList = new List<Category>();


                for (int i = 0; i < values.Length; i++)
                {
                    resultList.Add(new Category() { Name = values[i] });
                }
                db.AddRange(resultList);
                db.SaveChanges();
            }
        }
        //private static void InsertProducts()
        //{
        //    using (var db = new WebShopContext())
        //    {
        //        string[] names = "World of WarCraft, Fifa 23, Horde T-shirt".Split(", ");
        //        int[] categoryIds = { 5, 1, 7 };
        //        int[] prices = { 999, 799, 2499 };
        //        int[] unitsInStock = { 500, 500, 100 };
        //        string[] descriptions = "Addictive, Play with friends, For the Horde".Split(", ");
        //        int[] suppliersId = { 1, 7, 1 };

        //        var resultList = new List<Product>();


        //        for (int i = 0; i < names.Length; i++)
        //        {
        //            resultList.Add(new Product()
        //            {
        //                Name = names[i],
        //                CategoryId = categoryIds[i],
        //                Price = prices[i],
        //                UnitsInStock = unitsInStock[i],
        //                Description = descriptions[i],
        //                SupplierId = suppliersId[i]
        //            });
        //        }
        //        db.AddRange(resultList);
        //        db.SaveChanges();
        //    }
        //}
    }
}
