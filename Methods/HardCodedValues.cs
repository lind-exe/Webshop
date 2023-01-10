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
                    new Genre() { Name = "Coding Game" }

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
                    " Square Enix, Ubisoft "
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
                string[] values = "Postmord, DHL, Budbee, MickeFreightSolutions".Split(", ");
                var resultList = new List<ShipChoice>();


                for (int i = 0; i < values.Length; i++)
                {
                    resultList.Add(new ShipChoice() { ShipVia = values[i] });
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
    }
}
