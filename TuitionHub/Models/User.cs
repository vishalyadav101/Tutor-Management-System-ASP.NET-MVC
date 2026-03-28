using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuitionHub.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam zaroori hai")]
        [StringLength(100)]
        [Display(Name = "Pura Naam")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email zaroori hai")]
        [EmailAddress(ErrorMessage = "Sahi email dalein")]
        [StringLength(150)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password zaroori hai")]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(15)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        [StringLength(10)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; } = false;

        [Display(Name = "Join Date")]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        // ===== TUTOR FIELDS =====
        [StringLength(50)]
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Display(Name = "Experience (Years)")]
        public int? Experience { get; set; }

        [StringLength(500)]
        [Display(Name = "Subjects")]
        public string Subjects { get; set; }

        [StringLength(1000)]
        [Display(Name = "Bio")]
        public string Bio { get; set; }

        [Display(Name = "Rating")]
        public double? Rating { get; set; } = 0.0;

        // ===== STUDENT FIELDS =====
        [StringLength(20)]
        [Display(Name = "Class")]
        public string Class { get; set; }

        [StringLength(100)]
        [Display(Name = "Required Subject")]
        public string RequiredSubject { get; set; }

        // ===== HELPER PROPERTIES =====
        [NotMapped]
        public string Initials
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(FullName))
                        return "??";

                    string[] parts = FullName.Trim().Split(
                        new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length >= 2 &&
                        parts[0].Length > 0 &&
                        parts[1].Length > 0)
                    {
                        return (parts[0][0].ToString() +
                                parts[1][0].ToString()).ToUpper();
                    }
                    else if (parts.Length >= 1 &&
                             parts[0].Length > 0)
                    {
                        return parts[0].Substring(
                            0, Math.Min(2, parts[0].Length)).ToUpper();
                    }
                    return "??";
                }
                catch
                {
                    return "??";
                }
            }
        }

        [NotMapped]
        public string AvatarColor
        {
            get
            {
                string[] colors = {
                    "linear-gradient(135deg,#e94560,#c73652)",
                    "linear-gradient(135deg,#f5a623,#e08800)",
                    "linear-gradient(135deg,#3498db,#2176ae)",
                    "linear-gradient(135deg,#2ecc71,#27ae60)",
                    "linear-gradient(135deg,#9b59b6,#7d3c98)",
                    "linear-gradient(135deg,#e67e22,#ca6f1e)"
                };
                return colors[Math.Abs(Id % colors.Length)];
            }
        }
    }
}