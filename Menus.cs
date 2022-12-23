using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop
{
    internal class Menus
    {
        enum MenuList
        {
            Log_in,
            New_Customer,
            Browse_Shop,
            Search_Product,
            Exit_Shop

        }

        public static void Show()
        {
            bool go = true;
            while(go)
            {

                foreach(int i in Enum.GetValues(typeof(MenuList)))
                {
                    Console.WriteLine($"{i}. {Enum.GetName(typeof(MenuList), i).Replace("_", " ")}");
                }
            }

            int nr;
            MenuList menu = (MenuList)99; //Default
            if(int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
            {
                menu = (MenuList)nr;
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Wrong Input");
            }

            switch(menu)
            {
                case MenuList.Log_in:
                    break;
                case MenuList.New_Customer:
                    break;
                case MenuList.Browse_Shop:
                    break;
                case MenuList.Search_Product:
                    break;
                case MenuList.Exit_Shop:
                    break;
            }
        }

    }
}
