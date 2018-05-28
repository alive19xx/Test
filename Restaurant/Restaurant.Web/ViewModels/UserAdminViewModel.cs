using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using Restaurant.Domain.Enums;

namespace Restaurant.Web.ViewModels
{
    public class UserAdminViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        
        [Required]
        [MaxLength(255)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Second name")]
        public string SecondName { get; set; }

        [MaxLength(255)]
        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Position")]
        public UserPosition Position { get; set; }
    }
}