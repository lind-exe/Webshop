using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webshop.Models
{

    [Index(nameof(UserName), IsUnique = true)]

    public class Customer
    {
        public int Id { get; set; }
        public string UserName { get; set; }  //unique string for username login
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
