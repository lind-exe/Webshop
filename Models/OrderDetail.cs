﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float? Discount { get; set; }
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Products { get; set; }
    }
}
