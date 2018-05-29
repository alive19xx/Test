using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant.Web.ViewModels.OrderForm
{
    public class OrderFormViewModel
    {
        [Required]
        public int TableNumber { get; set; }
        public IList<OrderFormItemViewModel> OrderItems { get; set; }
    }
}