using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuitionHub.Models
{
    [Table("Announcements")]
    public class Announcement
    {
        [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title zaroori hai")]
    [StringLength(200)]
    [Display(Name = "Title")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Content zaroori hai")]
    [Display(Name = "Content")]
    public string Content { get; set; }

    [Display(Name = "Date")]
    public DateTime Date { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;

    [StringLength(20)]
    public string Target { get; set; } = "All";
    // "All" / "Students" / "Tutors"

    [StringLength(20)]
    public string Priority { get; set; } = "Normal";
    // "Normal" / "Important" / "Urgent"
}
}

