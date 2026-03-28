using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TuitionHub.Models;


//using static TuitionHub.Models.HireRequestController;

namespace TuitionHub.Models
{
    public class TuitionDbContext : DbContext
    {
        public TuitionDbContext() : base("TuitionHubDB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<HireRequest> HireRequests { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<HireRequest>()
                .HasRequired(h => h.Student)
                .WithMany()
                .HasForeignKey(h => h.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HireRequest>()
                .HasRequired(h => h.Tutor)
                .WithMany()
                .HasForeignKey(h => h.TutorId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}