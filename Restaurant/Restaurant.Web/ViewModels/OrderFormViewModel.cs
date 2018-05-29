using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Web.ViewModels
{
    public class OrderFormViewModel
    {
        [Required]
        public int TableNumber { get; set; }
        public int? Id { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public DateTime DateTimeCreated { get; set; }
        
        public IEnumerable<MenuItemViewModel> Menu { get; set; }
    }
}