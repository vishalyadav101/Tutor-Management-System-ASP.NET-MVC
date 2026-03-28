using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuitionHub.Models
{
    [Table("ContactMessages")]
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam zaroori hai")]
        [StringLength(100)]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email zaroori hai")]
        [EmailAddress(ErrorMessage = "Sahi email dalein")]
        [StringLength(150)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(15)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [StringLength(30)]
        [Display(Name = "Type")]
        public string Type { get; set; }
        // "Suggestion" / "Feedback" / "Complaint" / "Other"

        [Required(ErrorMessage = "Subject zaroori hai")]
        [StringLength(200)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message zaroori hai")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; } = DateTime.Now;

      
        public bool IsRead { get; set; } = false;
   
    }

}


