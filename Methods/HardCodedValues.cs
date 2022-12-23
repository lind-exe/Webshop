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
        public static void InsertGenres()
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


    }
}
