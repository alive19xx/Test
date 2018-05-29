using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Web.ViewModels.OrderForm
{
    public class OrderFormItemViewModel
    {
        [Required]
        public int MenuItemId { get; set; }
        [Required]
        public int NumberOfItems { get; set; }
    }
}