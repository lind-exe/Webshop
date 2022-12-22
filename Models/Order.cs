using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public bool Purchased { get; set; }
        public DateOnly OrderDate { get; set; }
        public int ShipViaId { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
