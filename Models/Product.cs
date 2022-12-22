using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public int UnitsInStock { get; set; }
        public string? Description { get; set; }
        public int? GenreId { get; set; }
        public int SupplierId { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Genre>? Genres { get; set; }
    }
}
