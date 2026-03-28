////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web;
////using System.Web.Mvc;

////namespace TuitionHub.Controllers
////{
////    public class TutorController : Controller
////    {
////        // GET: Tutor
////        public ActionResult Dashboard() { return View(); }
////        public ActionResult Students() { return View(); }
////        public ActionResult UserProfile() { return View(); }
////        public ActionResult Contact() { return View(); }

////        [HttpPost]
////        [ValidateAntiForgeryToken]
////        public ActionResult Contact(string Reason, string Subject, string Message, string Priority)
////        {
////            TempData["SuccessMsg"] = "Message admin tak pahunch gaya!";
////            return RedirectToAction("Contact");
////        }
////    }
////}
//using System;
//using System.Linq;
//using System.Web.Mvc;
//using TuitionHub.Models;

//namespace TuitionHub.Controllers
//{
//    public class TutorController : Controller
//    {
//        private TuitionDbContext db = new TuitionDbContext();

//        // ========== DASHBOARD ==========
//        public ActionResult Dashboard()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int userId = (int)Session["UserId"];

//            ViewBag.TotalStudents = db.HireRequests
//                                      .Where(h => h.TutorId == userId)
//                                      .Count();

//            ViewBag.NewRequests = db.HireRequests
//                                    .Where(h => h.TutorId == userId
//                                             && h.Status == "Pending")
//                                    .Count();

//            ViewBag.RecentStudents = db.HireRequests
//                                       .Where(h => h.TutorId == userId)
//                                       .Select(h => h.Student)
//                                       .Take(5)
//                                       .ToList();

//            return View();
//        }

//        // ========== STUDENTS LIST ==========
//        public ActionResult Students(string classFilter, string subject, string city)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            var students = db.Users.Where(u =>
//                u.Role == "Student" &&
//                u.IsActive == true);

//            if (!string.IsNullOrEmpty(classFilter))
//                students = students.Where(u => u.Class == classFilter);

//            if (!string.IsNullOrEmpty(subject))
//                students = students.Where(u => u.RequiredSubject.Contains(subject));

//            if (!string.IsNullOrEmpty(city))
//                students = students.Where(u => u.City.Contains(city));

//            ViewBag.Students = students.ToList();
//            ViewBag.TotalCount = students.Count();
//            return View();
//        }

//        // ========== PROFILE ==========
//        public new ActionResult Profile()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int userId = (int)Session["UserId"];
//            var tutor = db.Users.Find(userId);
//            ViewBag.Tutor = tutor;
//            return View();
//        }

//        // ========== PROFILE UPDATE ==========
//        //        [HttpPost]
//        //        [ValidateAntiForgeryToken]
//        //        public ActionResult UpdateProfile(string FullName, string Phone,
//        //                                          string City, string Gender,
//        //                                          string Qualification, int? Experience,
//        //                                          string Subjects, string Bio,
//        //                                          string NewPassword, string ConfirmPassword)
//        //        {
//        //            if (Session["UserId"] == null)
//        //                return RedirectToAction("Login", "Account");

//        //            int userId = (int)Session["UserId"];
//        //            var tutor = db.Users.Find(userId);

//        //            if (tutor != null)
//        //            {
//        //                tutor.FullName = FullName;
//        //                tutor.Phone = Phone;
//        //                tutor.City = City;
//        //                tutor.Gender = Gender;
//        //                tutor.Qualification = Qualification;
//        //                tutor.Experience = Experience;
//        //                tutor.Subjects = Subjects;
//        //                tutor.Bio = Bio;

//        //                if (!string.IsNullOrEmpty(NewPassword) &&
//        //                    NewPassword == ConfirmPassword)
//        //                {
//        //                    tutor.Password = NewPassword;
//        //                }

//        //                db.SaveChanges();
//        //                Session["Username"] = tutor.FullName;
//        //                TempData["SuccessMsg"] = "Profile update ho gayi! 😊";
//        //            }

//        //            return RedirectToAction("Profile");
//        //        }

//        //        // ========== CONTACT GET ==========
//        //        public ActionResult Contact()
//        //        {
//        //            if (Session["UserId"] == null)
//        //                return RedirectToAction("Login", "Account");
//        //            return View();
//        //        }

//        //        // ========== CONTACT POST ==========
//        //        [HttpPost]
//        //        [ValidateAntiForgeryToken]
//        //        public ActionResult Contact(string Reason, string Subject,
//        //                                    string Message, string Priority)
//        //        {
//        //            if (Session["UserId"] == null)
//        //                return RedirectToAction("Login", "Account");

//        //            if (string.IsNullOrEmpty(Subject))
//        //            {
//        //                TempData["ErrorMsg"] = "Subject zaroori hai!";
//        //                return RedirectToAction("Contact");
//        //            }
//        //            if (string.IsNullOrEmpty(Message))
//        //            {
//        //                TempData["ErrorMsg"] = "Message zaroori hai!";
//        //                return RedirectToAction("Contact");
//        //            }

//        //            int userId = (int)Session["UserId"];

//        //            var adminMsg = new AdminMessage
//        //            {
//        //                UserId = userId,
//        //                UserRole = "Tutor",
//        //                Reason = Reason,
//        //                Subject = Subject,
//        //                Message = Message,
//        //                Priority = Priority,
//        //                Status = "Pending",
//        //                Date = DateTime.Now,
//        //                SenderName = Session["Username"] != null
//        //                              ? Session["Username"].ToString() : "",
//        //                SenderEmail = Session["Email"] != null
//        //                              ? Session["Email"].ToString() : ""
//        //            };

//        //            db.AdminMessages.Add(adminMsg);
//        //            db.SaveChanges();

//        //            TempData["SuccessMsg"] = "Message admin tak pahunch gaya! 😊";
//        //            return RedirectToAction("Contact");
//        //        }
//        //    }
//        //}
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Contact(string Reason, string Subject,
//                                    string Message, string Priority)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            if (string.IsNullOrEmpty(Subject))
//            {
//                TempData["ErrorMsg"] = "Subject zaroori hai!";
//                return RedirectToAction("Contact");
//            }
//            if (string.IsNullOrEmpty(Message))
//            {
//                TempData["ErrorMsg"] = "Message zaroori hai!";
//                return RedirectToAction("Contact");
//            }
//            if (string.IsNullOrEmpty(Reason))
//                Reason = "Suggestion";
//            if (string.IsNullOrEmpty(Priority))
//                Priority = "Medium";

//            int userId = (int)Session["UserId"];

//            // Database se user ka email lo
//            var user = db.Users.Find(userId);

//            var adminMsg = new AdminMessage
//            {
//                UserId = userId,
//                UserRole = "Tutor",
//                Reason = Reason,
//                Subject = Subject,
//                Message = Message,
//                Priority = Priority,
//                Status = "Pending",
//                Date = DateTime.Now,
//                SenderName = user != null ? user.FullName : "",
//                SenderEmail = user != null ? user.Email : null  // ← null dalo
//            };

//            db.AdminMessages.Add(adminMsg);
//            db.SaveChanges();

//            TempData["SuccessMsg"] = "Message admin tak pahunch gaya! 😊";
//            return RedirectToAction("Contact");
//        }
//    }
//}
using System;
using System.Linq;
using System.Web.Mvc;
using TuitionHub.Models;

namespace TuitionHub.Controllers
{
    public class TutorController : Controller
    {
        private TuitionDbContext db = new TuitionDbContext();

        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult Students(string classFilter, string subject, string city)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            var students = db.Users.Where(u =>
                u.Role == "Student" &&
                u.IsActive == true).ToList();

            if (!string.IsNullOrEmpty(classFilter))
                students = students
                    .Where(u => u.Class == classFilter)
                    .ToList();

            if (!string.IsNullOrEmpty(subject))
                students = students
                    .Where(u => u.RequiredSubject != null &&
                                u.RequiredSubject.Contains(subject))
                    .ToList();

            if (!string.IsNullOrEmpty(city))
                students = students
                    .Where(u => u.City != null &&
                                u.City.Contains(city))
                    .ToList();

            ViewBag.Students = students;
            ViewBag.TotalCount = students.Count;
            return View();
        }

        public new ActionResult Profile()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = (int)Session["UserId"];
            var tutor = db.Users.Find(userId);
            ViewBag.Tutor = tutor;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string FullName, string Phone,
                                          string City, string Gender,
                                          string Qualification, int? Experience,
                                          string Subjects, string Bio,
                                          string NewPassword, string ConfirmPassword)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = (int)Session["UserId"];
            var tutor = db.Users.Find(userId);

            if (tutor != null)
            {
                tutor.FullName = FullName;
                tutor.Phone = Phone;
                tutor.City = City;
                tutor.Gender = Gender;
                tutor.Qualification = Qualification;
                tutor.Experience = Experience;
                tutor.Subjects = Subjects;
                tutor.Bio = Bio;

                if (!string.IsNullOrEmpty(NewPassword) &&
                    NewPassword == ConfirmPassword)
                {
                    tutor.Password = NewPassword;
                }

                db.SaveChanges();
                Session["Username"] = tutor.FullName;
                TempData["SuccessMsg"] = "Profile update ho gayi! 😊";
            }

            return RedirectToAction("Profile");
        }

        // ========== CONTACT GET ==========
        public ActionResult Contact()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        // ========== CONTACT POST ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(string Reason, string Subject,
                                    string Message, string Priority)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrEmpty(Subject))
            {
                TempData["ErrorMsg"] = "Subject zaroori hai!";
                return RedirectToAction("Contact");
            }
            if (string.IsNullOrEmpty(Message))
            {
                TempData["ErrorMsg"] = "Message zaroori hai!";
                return RedirectToAction("Contact");
            }
            if (string.IsNullOrEmpty(Reason))
                Reason = "Suggestion";
            if (string.IsNullOrEmpty(Priority))
                Priority = "Medium";

            int userId = (int)Session["UserId"];
            var user = db.Users.Find(userId);

            var adminMsg = new AdminMessage
            {
                UserId = userId,
                UserRole = "Tutor",
                Reason = Reason,
                Subject = Subject,
                Message = Message,
                Priority = Priority,
                Status = "Pending",
                Date = DateTime.Now,
                SenderName = user != null ? user.FullName : "",
                SenderEmail = user != null ? user.Email : null
            };

            db.AdminMessages.Add(adminMsg);
            db.SaveChanges();

            TempData["SuccessMsg"] = "Message admin tak pahunch gaya! 😊";
            return RedirectToAction("Contact");
        }
    }
}