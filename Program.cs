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
            //Methods.HardCodedValues.AllInserts();
            bool runProgram = true;
            Customer? c = null;
            while (runProgram)
            {
                Console.Clear();
                Console.ResetColor();
                //Helpers.Welcome();
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