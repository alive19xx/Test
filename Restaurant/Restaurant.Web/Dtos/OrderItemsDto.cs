using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Web.Dtos
{
    public class OrderItemDto
    {
        public int MenuItemId { get; set; }
        public int NumberOfItems { get; set; }
    }
}