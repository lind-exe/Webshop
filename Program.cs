//HardCodedValues.AllInserts();
using Webshop.Methods;
using Microsoft.EntityFrameworkCore;
using Webshop.Models;
using System.Net.Mail;
using System.Net;

namespace Webshop
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool runProgram = true;
            Customer? c = null;
            Helpers.Welcome();
            while (runProgram)
            {
                Console.Clear();
                Console.ResetColor();
                if (c == null)
                {
                    c = Menus.Show("LogIn", c);
                }
                else
                {
                    View.DisplayCustomer(c);
                    if (c.UserName == "admin")
                    {
                        Menus.Show("Main", c);
                    }
                    else
                    {
                        View.DisplayCustomer(c);
                        Menus.Show("Main", c);
                    }
                }
            }
        }
    }
}