using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        public int TableNumber { get; set; }
        public IList<MenuItemViewModel> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal TotalPrice { get; set; }
    }
}