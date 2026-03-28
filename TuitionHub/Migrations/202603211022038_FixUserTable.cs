namespace TuitionHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        UserRole = c.String(maxLength: 20),
                        Reason = c.String(nullable: false, maxLength: 30),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Message = c.String(nullable: false),
                        Priority = c.String(maxLength: 20),
                        Status = c.String(maxLength: 20),
                        Date = c.DateTime(nullable: false),
                        SenderName = c.String(maxLength: 100),
                        SenderEmail = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 15),
                        Role = c.String(nullable: false, maxLength: 20),
                        Gender = c.String(maxLength: 10),
                        City = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        IsVerified = c.Boolean(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        Qualification = c.String(maxLength: 50),
                        Experience = c.Int(),
                        Subjects = c.String(maxLength: 500),
                        Bio = c.String(maxLength: 1000),
                        Rating = c.Double(),
                        Class = c.String(maxLength: 20),
                        RequiredSubject = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Target = c.String(maxLength: 20),
                        Priority = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(maxLength: 15),
                        Type = c.String(maxLength: 30),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Message = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HireRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        TutorId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.StudentId)
                .ForeignKey("dbo.Users", t => t.TutorId)
                .Index(t => t.StudentId)
                .Index(t => t.TutorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HireRequests", "TutorId", "dbo.Users");
            DropForeignKey("dbo.HireRequests", "StudentId", "dbo.Users");
            DropForeignKey("dbo.AdminMessages", "UserId", "dbo.Users");
            DropIndex("dbo.HireRequests", new[] { "TutorId" });
            DropIndex("dbo.HireRequests", new[] { "StudentId" });
            DropIndex("dbo.AdminMessages", new[] { "UserId" });
            DropTable("dbo.HireRequests");
            DropTable("dbo.ContactMessages");
            DropTable("dbo.Announcements");
            DropTable("dbo.Users");
            DropTable("dbo.AdminMessages");
        }
    }
}
