using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Web.Dtos
{
    public class OrderItemsDto
    {
        public int OrderId { get; set; }
        public IEnumerable<int> NewItemsIds { get; set; }
    }
}