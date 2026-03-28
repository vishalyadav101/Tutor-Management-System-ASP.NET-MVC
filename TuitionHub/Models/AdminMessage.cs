using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TuitionHub.Models;

namespace TuitionHub.Models
{
    [Table("AdminMessages")]
    public class AdminMessage
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(20)]
        public string UserRole { get; set; }
        // "Student" / "Tutor" / "Guest"

        [Required(ErrorMessage = "Reason select karein")]
        [StringLength(30)]
        [Display(Name = "Reason")]
        public string Reason { get; set; }
        // "Complaint" / "Feedback" / "Suggestion" / "Other"

        [Required(ErrorMessage = "Subject zaroori hai")]
        [StringLength(200)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message zaroori hai")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [StringLength(20)]
        [Display(Name = "Priority")]
        public string Priority { get; set; } = "Medium";
        // "Low" / "Medium" / "High"

        [StringLength(20)]
        public string Status { get; set; } = "Pending";
        // "Pending" / "Resolved" / "Closed"

        [Display(Name = "Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string SenderName { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}

