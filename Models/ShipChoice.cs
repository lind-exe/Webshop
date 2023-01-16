using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class ShipChoice
    {
        public int Id { get; set; }
        public string? ShipVia { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
