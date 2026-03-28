using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuitionHub.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Naam zaroori hai")]
        [StringLength(100)]
        [Display(Name = "Pura Naam")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email zaroori hai")]
        [EmailAddress(ErrorMessage = "Sahi email dalein")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(15)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "City zaroori hai")]
        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Gender select karein")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Role select karein")]
        [Display(Name = "Role")]
        public string Role { get; set; }
        // "Student" / "Tutor"

        [Required(ErrorMessage = "Password zaroori hai")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password kam se kam 6 characters ka hona chahiye")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password zaroori hai")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password aur Confirm Password match nahi kar rahe")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        // Tutor Fields
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Display(Name = "Experience")]
        public int? Experience { get; set; }

        [Display(Name = "Subjects")]
        public string Subjects { get; set; }

        [Display(Name = "Bio")]
        public string Bio { get; set; }

        // Student Fields
        [Display(Name = "Class")]
        public string Class { get; set; }

        [Display(Name = "Required Subject")]
        public string RequiredSubject { get; set; }

        public bool AgreeTerms { get; set; }
    }
}
    
