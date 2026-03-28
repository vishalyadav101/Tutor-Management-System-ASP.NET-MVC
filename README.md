
# TuitionHub - Tutor Management System

A full-stack web application built with **ASP.NET MVC 5** that connects 
students with verified tutors. The platform supports three user roles — 
Student, Tutor, and Admin — each with a dedicated dashboard and features.

---

## Features

### Student
- Register and login securely
- Search tutors by subject, city, gender, and qualification
- Hire tutors with one click
- View hired tutors in "Mere Tutors" section
- Update profile and contact admin

### Tutor
- Register and get verified by admin
- View available students list with filters
- Update profile (subjects, bio, experience, qualification)
- Contact admin for support

### Admin
- Dashboard with real-time stats (students, tutors, hires, complaints)
- Manage students and tutors (edit, delete, verify)
- Post announcements visible to all users
- View and resolve complaints/feedback from users
- Reports page with subject-wise demand and city-wise user data

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET MVC 5 (.NET Framework 4.8) |
| Language | C# |
| ORM | Entity Framework 6 (Code First) |
| Database | SQL Server Express |
| Frontend | Bootstrap 5, HTML5, CSS3 |
| Icons | Font Awesome 6 |
| IDE | Visual Studio 2022 |

---

## Database Tables

- `Users` — Students, Tutors, and Admins (single table, role-based)
- `HireRequests` — Student-Tutor hiring records
- `Announcements` — Admin announcements
- `AdminMessages` — Complaints, feedback, suggestions
- `ContactMessages` — Public contact form submissions

---

## Project Structure
```
TuitionHub/
├── Controllers/
│   ├── AccountController.cs
│   ├── AdminController.cs
│   ├── HomeController.cs
│   ├── StudentController.cs
│   └── TutorController.cs
├── Models/
│   ├── User.cs
│   ├── HireRequest.cs
│   ├── Announcement.cs
│   ├── AdminMessage.cs
│   ├── ContactMessage.cs
│   └── TuitionDbContext.cs
├── Views/
│   ├── Admin/
│   ├── Student/
│   ├── Tutor/
│   ├── Home/
│   └── Shared/
├── Content/
│   └── site.css
└── Scripts/
    └── site.js
```

---

## Getting Started

### Prerequisites
- Visual Studio 2022
- SQL Server Express
- .NET Framework 4.8

### Setup

**1. Clone the repository**
```bash
git clone https://github.com/yourusername/TuitionHub.git
```

**2. Update connection string in `Web.config`**
```xml
<connectionStrings>
  <add name="TuitionHubDB"
       connectionString="Data Source=YOUR_SERVER\SQLEXPRESS;
       Initial Catalog=TuitionHubDB;
       Integrated Security=True;
       MultipleActiveResultSets=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**3. Run the application**
- Open `TuitionHub.sln` in Visual Studio
- Press `F5` to build and run
- Database will be created automatically via Entity Framework

**4. Create Admin account (SSMS)**
```sql
INSERT INTO Users (FullName, Email, Password, Role, IsActive, IsVerified, JoinDate)
VALUES ('Admin', 'admin@tuitionhub.in', 'admin123', 'Admin', 1, 1, GETDATE());
```

---

## Demo Credentials

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@tuitionhub.in | admin123 |
| Tutor | Register as Tutor | - |
| Student | Register as Student | - |

---

## Screenshots

> Home Page, Student Dashboard, Admin Panel, Tutor Profile

---

## Key Highlights

- Role-based access control (Student / Tutor / Admin)
- Session-based authentication
- CSRF protection on all forms
- Entity Framework 6 with proper foreign key constraints
- Responsive UI — works on all screen sizes
- Real-time database stats on dashboards
- Admin can verify tutors before they appear in search

---

## License

This project is for educational purposes.

---

## Author

**[Aapka Naam]**  
[GitHub Profile Link]  
[LinkedIn Profile Link]
```

---

## GitHub Topics (Tags) Add Karo
```
aspnet-mvc  csharp  sql-server  entity-framework  
bootstrap  tuition-platform  web-application  dotnet
