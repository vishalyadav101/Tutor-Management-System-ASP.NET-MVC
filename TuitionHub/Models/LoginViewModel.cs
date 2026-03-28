using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuitionHub.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email zaroori hai")]
        [EmailAddress(ErrorMessage = "Sahi email dalein")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password zaroori hai")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Mujhe yaad rakho")]
        public bool RememberMe { get; set; }
    }
}