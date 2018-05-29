using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int TableNumber { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
