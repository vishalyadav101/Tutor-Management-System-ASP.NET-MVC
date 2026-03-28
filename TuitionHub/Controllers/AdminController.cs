////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web;
////using System.Web.Mvc;

////namespace TuitionHub.Controllers
////{
////    public class AdminController : Controller
////    {
////        // GET: Admin
////        public ActionResult Dashboard() { return View(); }
////        public ActionResult Students() { return View(); }
////        public ActionResult Tutors() { return View(); }
////        public ActionResult Announcements() { return View(); }
////        public ActionResult Complaints() { return View(); }
////        public ActionResult Reports() { return View(); }

////        [HttpPost]
////        [ValidateAntiForgeryToken]
////        public ActionResult AddAnnouncement(string Title, string Content)
////        {
////            TempData["SuccessMsg"] = "Announcement post ho gayi!";
////            return RedirectToAction("Dashboard");
////        }
////    }
////}

//using System;
//using System.Linq;
//using System.Web.Mvc;
//using TuitionHub.Models;

//namespace TuitionHub.Controllers
//{
//    public class AdminController : Controller
//    {
//        private TuitionDbContext db = new TuitionDbContext();

//        // ========== DASHBOARD ==========
//        public ActionResult Dashboard()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");
//            if (Session["Role"].ToString() != "Admin")
//                return RedirectToAction("Login", "Account");

//            ViewBag.TotalStudents = db.Users.Count(u => u.Role == "Student");
//            ViewBag.TotalTutors = db.Users.Count(u => u.Role == "Tutor");
//            ViewBag.PendingComplaints = db.AdminMessages.Count(m => m.Status == "Pending");
//            ViewBag.TotalAnnouncements = db.Announcements.Count();

//            ViewBag.RecentStudents = db.Users
//                                       .Where(u => u.Role == "Student")
//                                       .OrderByDescending(u => u.JoinDate)
//                                       .Take(5)
//                                       .ToList();

//            ViewBag.RecentTutors = db.Users
//                                     .Where(u => u.Role == "Tutor")
//                                     .OrderByDescending(u => u.JoinDate)
//                                     .Take(5)
//                                     .ToList();

//            ViewBag.Announcements = db.Announcements
//                                      .OrderByDescending(a => a.Date)
//                                      .Take(3)
//                                      .ToList();

//            return View();
//        }

//        // ========== STUDENTS LIST ==========
//        public ActionResult Students(string search, string city,
//                                     string cls, string status)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var students = db.Users.Where(u => u.Role == "Student");

//            if (!string.IsNullOrEmpty(search))
//                students = students.Where(u => u.FullName.Contains(search)
//                                            || u.Email.Contains(search));

//            if (!string.IsNullOrEmpty(city))
//                students = students.Where(u => u.City.Contains(city));

//            if (!string.IsNullOrEmpty(cls))
//                students = students.Where(u => u.Class == cls);

//            if (status == "Active")
//                students = students.Where(u => u.IsActive == true);
//            else if (status == "Inactive")
//                students = students.Where(u => u.IsActive == false);

//            ViewBag.Students = students.ToList();
//            ViewBag.TotalCount = students.Count();
//            return View();
//        }

//        // ========== EDIT STUDENT ==========
//        public ActionResult EditStudent(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var student = db.Users.Find(id);
//            if (student == null)
//                return HttpNotFound();

//            ViewBag.Student = student;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult EditStudent(int id, string FullName, string Email,
//                                        string City, string Gender,
//                                        string Class, bool IsActive)
//        {
//            var student = db.Users.Find(id);
//            if (student != null)
//            {
//                student.FullName = FullName;
//                student.Email = Email;
//                student.City = City;
//                student.Gender = Gender;
//                student.Class = Class;
//                student.IsActive = IsActive;
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Student update ho gaya!";
//            }
//            return RedirectToAction("Students");
//        }

//        // ========== DELETE STUDENT ==========
//        public ActionResult DeleteStudent(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var student = db.Users.Find(id);
//            if (student != null)
//            {
//                db.Users.Remove(student);
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Student delete ho gaya!";
//            }
//            return RedirectToAction("Students");
//        }

//        // ========== TUTORS LIST ==========
//        public ActionResult Tutors(string search, string subject,
//                                   string verified, string city)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var tutors = db.Users.Where(u => u.Role == "Tutor");

//            if (!string.IsNullOrEmpty(search))
//                tutors = tutors.Where(u => u.FullName.Contains(search)
//                                        || u.Subjects.Contains(search));

//            if (!string.IsNullOrEmpty(subject))
//                tutors = tutors.Where(u => u.Subjects.Contains(subject));

//            if (verified == "true")
//                tutors = tutors.Where(u => u.IsVerified == true);
//            else if (verified == "false")
//                tutors = tutors.Where(u => u.IsVerified == false);

//            if (!string.IsNullOrEmpty(city))
//                tutors = tutors.Where(u => u.City.Contains(city));

//            ViewBag.Tutors = tutors.ToList();
//            ViewBag.TotalCount = tutors.Count();
//            ViewBag.PendingVerification = tutors.Count(u => u.IsVerified == false);
//            return View();
//        }

//        // ========== VERIFY TUTOR ==========
//        public ActionResult VerifyTutor(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var tutor = db.Users.Find(id);
//            if (tutor != null)
//            {
//                tutor.IsVerified = true;
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Tutor verify ho gaya! ✅";
//            }
//            return RedirectToAction("Tutors");
//        }

//        // ========== EDIT TUTOR ==========
//        public ActionResult EditTutor(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var tutor = db.Users.Find(id);
//            if (tutor == null)
//                return HttpNotFound();

//            ViewBag.Tutor = tutor;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult EditTutor(int id, string FullName, string Email,
//                                      string City, string Gender,
//                                      string Qualification, int? Experience,
//                                      string Subjects, bool IsVerified, bool IsActive)
//        {
//            var tutor = db.Users.Find(id);
//            if (tutor != null)
//            {
//                tutor.FullName = FullName;
//                tutor.Email = Email;
//                tutor.City = City;
//                tutor.Gender = Gender;
//                tutor.Qualification = Qualification;
//                tutor.Experience = Experience;
//                tutor.Subjects = Subjects;
//                tutor.IsVerified = IsVerified;
//                tutor.IsActive = IsActive;
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Tutor update ho gaya!";
//            }
//            return RedirectToAction("Tutors");
//        }

//        // ========== DELETE TUTOR ==========
//        public ActionResult DeleteTutor(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var tutor = db.Users.Find(id);
//            if (tutor != null)
//            {
//                db.Users.Remove(tutor);
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Tutor delete ho gaya!";
//            }
//            return RedirectToAction("Tutors");
//        }

//        // ========== ANNOUNCEMENTS ==========
//        public ActionResult Announcements()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            ViewBag.Announcements = db.Announcements
//                                           .OrderByDescending(a => a.Date)
//                                           .ToList();
//            ViewBag.AnnouncementCount = db.Announcements.Count();
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AddAnnouncement(string Title, string Content,
//                                            string Target, string Priority)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Content))
//            {
//                var ann = new Announcement
//                {
//                    Title = Title,
//                    Content = Content,
//                    Target = Target ?? "All",
//                    Priority = Priority ?? "Normal",
//                    Date = DateTime.Now,
//                    IsActive = true
//                };
//                db.Announcements.Add(ann);
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Announcement post ho gayi! 📢";
//            }
//            return RedirectToAction("Announcements");
//        }

//        // ========== DELETE ANNOUNCEMENT ==========
//        public ActionResult DeleteAnnouncement(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var ann = db.Announcements.Find(id);
//            if (ann != null)
//            {
//                db.Announcements.Remove(ann);
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Announcement delete ho gayi!";
//            }
//            return RedirectToAction("Announcements");
//        }

//        // ========== COMPLAINTS ==========
//        public ActionResult Complaints(string type, string status)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var messages = db.AdminMessages.AsQueryable();

//            if (!string.IsNullOrEmpty(type))
//                messages = messages.Where(m => m.Reason == type);

//            if (!string.IsNullOrEmpty(status))
//                messages = messages.Where(m => m.Status == status);

//            ViewBag.Messages = messages.OrderByDescending(m => m.Date).ToList();
//            ViewBag.PendingCount = db.AdminMessages.Count(m => m.Status == "Pending");
//            ViewBag.ResolvedCount = db.AdminMessages.Count(m => m.Status == "Resolved");
//            return View();
//        }

//        // ========== RESOLVE COMPLAINT ==========
//        public ActionResult ResolveComplaint(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var msg = db.AdminMessages.Find(id);
//            if (msg != null)
//            {
//                msg.Status = "Resolved";
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Complaint resolve ho gayi! ✅";
//            }
//            return RedirectToAction("Complaints");
//        }

//        // ========== DELETE MESSAGE ==========
//        public ActionResult DeleteMessage(int id)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var msg = db.AdminMessages.Find(id);
//            if (msg != null)
//            {
//                db.AdminMessages.Remove(msg);
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Message delete ho gaya!";
//            }
//            return RedirectToAction("Complaints");
//        }

//        // ========== REPORTS ==========
//        public ActionResult Reports()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            ViewBag.TotalUsers = db.Users.Count();
//            ViewBag.TotalStudents = db.Users.Count(u => u.Role == "Student");
//            ViewBag.TotalTutors = db.Users.Count(u => u.Role == "Tutor");
//            ViewBag.TotalHires = db.HireRequests.Count();
//            return View();
//        }
//    }
//}
using System;
using System.Linq;
using System.Web.Mvc;
using TuitionHub.Models;

namespace TuitionHub.Controllers
{
    public class AdminController : Controller
    {
        private TuitionDbContext db = new TuitionDbContext();

        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            if (Session["Role"].ToString() != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.TotalStudents = db.Users.Count(u => u.Role == "Student");
            ViewBag.TotalTutors = db.Users.Count(u => u.Role == "Tutor");
            ViewBag.PendingComplaints = db.AdminMessages.Count(m => m.Status == "Pending");
            ViewBag.TotalAnnouncements = db.Announcements.Count();

            ViewBag.RecentStudents = db.Users
                                       .Where(u => u.Role == "Student")
                                       .OrderByDescending(u => u.JoinDate)
                                       .Take(5)
                                       .ToList();

            ViewBag.RecentTutors = db.Users
                                     .Where(u => u.Role == "Tutor")
                                     .OrderByDescending(u => u.JoinDate)
                                     .Take(5)
                                     .ToList();

            ViewBag.Announcements = db.Announcements
                                      .OrderByDescending(a => a.Date)
                                      .Take(3)
                                      .ToList();

            return View();
        }

        public ActionResult Students(string search, string city,
                                     string cls, string status)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            var studentsList = db.Users
                                 .Where(u => u.Role == "Student")
                                 .ToList();

            if (!string.IsNullOrEmpty(search))
                studentsList = studentsList
                    .Where(u => (u.FullName != null && u.FullName.Contains(search)) ||
                                (u.Email != null && u.Email.Contains(search)))
                    .ToList();

            if (!string.IsNullOrEmpty(city))
                studentsList = studentsList
                    .Where(u => u.City != null && u.City.Contains(city))
                    .ToList();

            if (!string.IsNullOrEmpty(cls))
                studentsList = studentsList
                    .Where(u => u.Class == cls)
                    .ToList();

            if (status == "Active")
                studentsList = studentsList.Where(u => u.IsActive == true).ToList();
            else if (status == "Inactive")
                studentsList = studentsList.Where(u => u.IsActive == false).ToList();

            ViewBag.Students = studentsList;
            ViewBag.TotalCount = studentsList.Count;
            return View();
        }

        public ActionResult EditStudent(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var student = db.Users.Find(id);
            if (student == null) return HttpNotFound();
            ViewBag.Student = student;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(int id, string FullName, string Email,
                                        string City, string Gender,
                                        string Class, bool IsActive)
        {
            var student = db.Users.Find(id);
            if (student != null)
            {
                student.FullName = FullName;
                student.Email = Email;
                student.City = City;
                student.Gender = Gender;
                student.Class = Class;
                student.IsActive = IsActive;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Student update ho gaya!";
            }
            return RedirectToAction("Students");
        }

        public ActionResult DeleteStudent(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var student = db.Users.Find(id);
            if (student != null)
            {
                db.Users.Remove(student);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Student delete ho gaya!";
            }
            return RedirectToAction("Students");
        }

        public ActionResult Tutors(string search, string subject,
                                   string verified, string city)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            var tutorsList = db.Users
                               .Where(u => u.Role == "Tutor")
                               .ToList();

            // PendingVerification — filtered se pehle calculate karo
            int pendingCount = tutorsList.Count(u => u.IsVerified == false);

            if (!string.IsNullOrEmpty(search))
                tutorsList = tutorsList
                    .Where(u => (u.FullName != null && u.FullName.Contains(search)) ||
                                (u.Subjects != null && u.Subjects.Contains(search)))
                    .ToList();

            if (!string.IsNullOrEmpty(subject))
                tutorsList = tutorsList
                    .Where(u => u.Subjects != null && u.Subjects.Contains(subject))
                    .ToList();

            if (verified == "true")
                tutorsList = tutorsList.Where(u => u.IsVerified == true).ToList();
            else if (verified == "false")
                tutorsList = tutorsList.Where(u => u.IsVerified == false).ToList();

            if (!string.IsNullOrEmpty(city))
                tutorsList = tutorsList
                    .Where(u => u.City != null && u.City.Contains(city))
                    .ToList();

            ViewBag.Tutors = tutorsList;
            ViewBag.TotalCount = tutorsList.Count;
            ViewBag.PendingVerification = pendingCount;
            return View();
        }

        public ActionResult VerifyTutor(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var tutor = db.Users.Find(id);
            if (tutor != null)
            {
                tutor.IsVerified = true;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Tutor verify ho gaya!";
            }
            return RedirectToAction("Tutors");
        }

        public ActionResult EditTutor(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var tutor = db.Users.Find(id);
            if (tutor == null) return HttpNotFound();
            ViewBag.Tutor = tutor;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTutor(int id, string FullName,
                                      string Email, string City,
                                      string Gender, string Qualification,
                                      int? Experience, string Subjects,
                                      bool IsVerified, bool IsActive)
        {
            var tutor = db.Users.Find(id);
            if (tutor != null)
            {
                tutor.FullName = FullName;
                tutor.Email = Email;
                tutor.City = City;
                tutor.Gender = Gender;
                tutor.Qualification = Qualification;
                tutor.Experience = Experience;
                tutor.Subjects = Subjects;
                tutor.IsVerified = IsVerified;
                tutor.IsActive = IsActive;
                db.SaveChanges();
                TempData["SuccessMsg"] = "Tutor update ho gaya!";
            }
            return RedirectToAction("Tutors");
        }

        public ActionResult DeleteTutor(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var tutor = db.Users.Find(id);
            if (tutor != null)
            {
                db.Users.Remove(tutor);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Tutor delete ho gaya!";
            }
            return RedirectToAction("Tutors");
        }

        public ActionResult Announcements()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Announcements = db.Announcements
                                           .OrderByDescending(a => a.Date)
                                           .ToList();
            ViewBag.AnnouncementCount = db.Announcements.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnnouncement(string Title, string Content,
                                            string Target, string Priority)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Content))
            {
                var ann = new Announcement
                {
                    Title = Title,
                    Content = Content,
                    Target = Target ?? "All",
                    Priority = Priority ?? "Normal",
                    Date = DateTime.Now,
                    IsActive = true
                };
                db.Announcements.Add(ann);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Announcement post ho gayi!";
            }
            return RedirectToAction("Announcements");
        }

        public ActionResult DeleteAnnouncement(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var ann = db.Announcements.Find(id);
            if (ann != null)
            {
                db.Announcements.Remove(ann);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Announcement delete ho gayi!";
            }
            return RedirectToAction("Announcements");
        }

        public ActionResult Complaints(string type, string status)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            var messages = db.AdminMessages.ToList();

            if (!string.IsNullOrEmpty(type))
                messages = messages.Where(m => m.Reason == type).ToList();

            if (!string.IsNullOrEmpty(status))
                messages = messages.Where(m => m.Status == status).ToList();

            ViewBag.Messages = messages.OrderByDescending(m => m.Date).ToList();
            ViewBag.PendingCount = db.AdminMessages.Count(m => m.Status == "Pending");
            ViewBag.ResolvedCount = db.AdminMessages.Count(m => m.Status == "Resolved");
            return View();
        }

        public ActionResult ResolveComplaint(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var msg = db.AdminMessages.Find(id);
            if (msg != null)
            {
                msg.Status = "Resolved";
                db.SaveChanges();
                TempData["SuccessMsg"] = "Complaint resolve ho gayi!";
            }
            return RedirectToAction("Complaints");
        }

        public ActionResult DeleteMessage(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            var msg = db.AdminMessages.Find(id);
            if (msg != null)
            {
                db.AdminMessages.Remove(msg);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Message delete ho gaya!";
            }
            return RedirectToAction("Complaints");
        }

        public ActionResult Reports()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }
    }
}