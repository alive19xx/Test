using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Restaurant.Domain.Enums;

namespace Restaurant.Web.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public UserPosition Position { get; set; }
    }
}