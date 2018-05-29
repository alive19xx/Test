using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Domain.Entities;

namespace Restaurant.Web.ViewModels
{
    public class OrderItemViewModel
    {
        public virtual MenuItemViewModel MenuItem { get; set; }

        public int NumberOfItems { get; set; }
    }
}