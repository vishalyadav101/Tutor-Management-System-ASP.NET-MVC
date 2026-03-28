using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TuitionHub.Models;

namespace TuitionHub.Models
{
    [Table("HireRequests")]
    public class HireRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int TutorId { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Status { get; set; } = "Pending";  // Pending, Active, Rejected, Completed

        [ForeignKey("StudentId")]
        public virtual User Student { get; set; }

        [ForeignKey("TutorId")]
        public virtual User Tutor { get; set; }
    }
}