using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Restaurant.Domain.Enums;

namespace Restaurant.Web.ViewModels
{
    public class UserFormViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }
        public string MiddleName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserPosition Position { get; set; }
    }
}