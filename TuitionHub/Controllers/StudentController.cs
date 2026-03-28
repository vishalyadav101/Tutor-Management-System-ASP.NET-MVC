//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using TuitionHub.Models;

//namespace TuitionHub.Controllers
//{
//    public class StudentController : Controller
//    {
//        // GET: Student
//        public ActionResult Dashboard() { return View(); }
//        public ActionResult FindTutor() { return View(); }
//        public ActionResult MyTutors() { return View(); }
//        public ActionResult UserProfile() { return View(); }
//        public ActionResult Contact() { return View(); }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Contact(string Reason, string Subject, string Message, string Priority)
//        {
//            TempData["SuccessMsg"] = "Message admin tak pahunch gaya!";
//            return RedirectToAction("Contact");
//        }
//    }
//}



//namespace TuitionHub.Controllers
//{
//    public class StudentController : Controller
//    {
//        private TuitionDbContext db = new TuitionDbContext();

//        // ========== DASHBOARD ==========
//        public ActionResult Dashboard()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");
//            return View();
//        }

//        // ========== FIND TUTOR ==========
//        public ActionResult FindTutor(string subject, string gender,
//                                       string qualification, string city)
//        {
//            var tutors = db.Users.Where(u =>
//                u.Role == "Tutor" &&
//                u.IsVerified == true &&
//                u.IsActive == true);

//            if (!string.IsNullOrEmpty(subject))
//                tutors = tutors.Where(u => u.Subjects.Contains(subject));

//            if (!string.IsNullOrEmpty(gender))
//                tutors = tutors.Where(u => u.Gender == gender);

//            if (!string.IsNullOrEmpty(qualification))
//                tutors = tutors.Where(u => u.Qualification == qualification);

//            if (!string.IsNullOrEmpty(city))
//                tutors = tutors.Where(u => u.City.Contains(city));

//            ViewBag.Tutors = tutors.ToList();
//            ViewBag.TotalCount = tutors.Count();
//            return View();
//        }

//        // ========== TUTOR DETAIL ==========
//        public ActionResult TutorDetail(int id)
//        {
//            var tutor = db.Users.Find(id);
//            if (tutor == null || tutor.Role != "Tutor")
//                return HttpNotFound();
//            ViewBag.Tutor = tutor;
//            return View();
//        }

//        // ========== HIRE TUTOR ==========
//        [HttpPost]
//        public ActionResult HireTutor(int tutorId)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int studentId = (int)Session["UserId"];

//            var existing = db.HireRequests.FirstOrDefault(h =>
//                h.StudentId == studentId &&
//                h.TutorId == tutorId);

//            if (existing == null)
//            {
//                var hire = new HireRequest
//                {
//                    StudentId = studentId,
//                    TutorId = tutorId,
//                    Status = "Pending",
//                    RequestDate = DateTime.Now
//                };
//                db.HireRequests.Add(hire);
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Tutor ko hire request bhej di gayi! 🎉";
//            }
//            else
//            {
//                TempData["ErrorMsg"] = "Aapne pehle se yeh tutor hire kar rakha hai!";
//            }

//            return RedirectToAction("FindTutor");
//        }

//        // ========== MY TUTORS ==========
//        public ActionResult MyTutors()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int studentId = (int)Session["UserId"];

//            var myTutors = db.HireRequests
//                             .Where(h => h.StudentId == studentId)
//                             .Select(h => h.Tutor)
//                             .ToList();

//            ViewBag.MyTutors = myTutors;
//            return View();
//        }

//        // ========== PROFILE ==========
//        public new ActionResult Profile()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int userId = (int)Session["UserId"];
//            var student = db.Users.Find(userId);
//            ViewBag.Student = student;
//            return View();
//        }

//        // ========== CONTACT GET ==========
//        public ActionResult Contact()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");
//            return View();
//        }

//        // ========== CONTACT POST ==========
//        //        [HttpPost]
//        //        [ValidateAntiForgeryToken]
//        //        public ActionResult Contact(string Reason, string Subject,
//        //                                    string Message, string Priority)
//        //        {
//        //            if (Session["UserId"] == null)
//        //                return RedirectToAction("Login", "Account");

//        //            // Validation
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

//        //            // Database mein save karo
//        //            var adminMsg = new AdminMessage
//        //            {
//        //                UserId = userId,
//        //                UserRole = "Student",
//        //                Reason = Reason,
//        //                Subject = Subject,
//        //                Message = Message,
//        //                Priority = Priority,
//        //                Status = "Pending",
//        //                Date = DateTime.Now,
//        //                SenderName = Session["Username"] != null
//        //                              ? Session["Username"].ToString()
//        //                              : "",
//        //                SenderEmail = Session["Email"] != null
//        //                              ? Session["Email"].ToString()
//        //                              : ""
//        //            };

//        //            db.AdminMessages.Add(adminMsg);
//        //            db.SaveChanges();

//        //            TempData["SuccessMsg"] = "Message admin tak pahunch gaya! Jald reply milega. 😊";
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
//                UserRole = "Student",
//                Reason = Reason,
//                Subject = Subject,
//                Message = Message,
//                Priority = Priority,
//                Status = "Pending",
//                Date = DateTime.Now,
//                SenderName = user != null ? user.FullName : "",
//                SenderEmail = user != null ? user.Email : null  // ← null dalo empty string nahi
//            };

//            db.AdminMessages.Add(adminMsg);
//            db.SaveChanges();

//            TempData["SuccessMsg"] = "Message admin tak pahunch gaya! 😊";
//            return RedirectToAction("Contact");
//        }
//    }
//}
//using System;
//using System.Linq;
//using System.Web.Mvc;
//using TuitionHub.Models;

//namespace TuitionHub.Controllers
//{
//    public class StudentController : Controller
//    {
//        private TuitionDbContext db = new TuitionDbContext();

//        public ActionResult Dashboard()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");
//            return View();
//        }

//        public ActionResult FindTutor(string subject, string gender,
//                                       string qualification, string city)
//        {
//            var tutors = db.Users.Where(u =>
//                u.Role == "Tutor" &&
//                u.IsVerified == true &&
//                u.IsActive == true);

//            if (!string.IsNullOrEmpty(subject))
//                tutors = tutors.Where(u => u.Subjects.Contains(subject));

//            if (!string.IsNullOrEmpty(gender))
//                tutors = tutors.Where(u => u.Gender == gender);

//            if (!string.IsNullOrEmpty(qualification))
//                tutors = tutors.Where(u => u.Qualification == qualification);

//            if (!string.IsNullOrEmpty(city))
//                tutors = tutors.Where(u => u.City.Contains(city));

//            ViewBag.Tutors = tutors.ToList();
//            ViewBag.TotalCount = tutors.Count();
//            return View();
//        }

//        public ActionResult TutorDetail(int id)
//        {
//            var tutor = db.Users.Find(id);
//            if (tutor == null || tutor.Role != "Tutor")
//                return HttpNotFound();
//            ViewBag.Tutor = tutor;
//            return View();
//        }

//        [HttpPost]
//        public ActionResult HireTutor(int tutorId)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int studentId = (int)Session["UserId"];

//            var existing = db.HireRequests.FirstOrDefault(h =>
//                h.StudentId == studentId &&
//                h.TutorId == tutorId);

//            if (existing == null)
//            {
//                var hire = new HireRequest
//                {
//                    StudentId = studentId,
//                    TutorId = tutorId,
//                    Status = "Pending",
//                    RequestDate = DateTime.Now
//                };
//                db.HireRequests.Add(hire);
//                db.SaveChanges();
//                TempData["SuccessMsg"] = "Tutor ko hire request bhej di gayi! 🎉";
//            }
//            else
//            {
//                TempData["ErrorMsg"] = "Aapne pehle se yeh tutor hire kar rakha hai!";
//            }

//            return RedirectToAction("FindTutor");
//        }

//        public ActionResult MyTutors()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int studentId = (int)Session["UserId"];

//            var myTutors = db.HireRequests
//                             .Where(h => h.StudentId == studentId)
//                             .Select(h => h.Tutor)
//                             .ToList();

//            ViewBag.MyTutors = myTutors;
//            return View();
//        }

//        public new ActionResult Profile()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int userId = (int)Session["UserId"];
//            var student = db.Users.Find(userId);
//            ViewBag.Student = student;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult UpdateProfile(string FullName, string Phone,
//                                          string City, string Gender,
//                                          string Class, string RequiredSubject,
//                                          string NewPassword, string ConfirmPassword)
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");

//            int userId = (int)Session["UserId"];
//            var student = db.Users.Find(userId);

//            if (student != null)
//            {
//                student.FullName = FullName;
//                student.Phone = Phone;
//                student.City = City;
//                student.Gender = Gender;
//                student.Class = Class;
//                student.RequiredSubject = RequiredSubject;

//                if (!string.IsNullOrEmpty(NewPassword) &&
//                    NewPassword == ConfirmPassword)
//                {
//                    student.Password = NewPassword;
//                }

//                db.SaveChanges();
//                Session["Username"] = student.FullName;
//                TempData["SuccessMsg"] = "Profile update ho gayi! 😊";
//            }

//            return RedirectToAction("Profile");
//        }

//        public ActionResult Contact()
//        {
//            if (Session["UserId"] == null)
//                return RedirectToAction("Login", "Account");
//            return View();
//        }

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
//            var user = db.Users.Find(userId);

//            var adminMsg = new AdminMessage
//            {
//                UserId = userId,
//                UserRole = "Student",
//                Reason = Reason,
//                Subject = Subject,
//                Message = Message,
//                Priority = Priority,
//                Status = "Pending",
//                Date = DateTime.Now,
//                SenderName = user != null ? user.FullName : "",
//                SenderEmail = user != null ? user.Email : null
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
    public class StudentController : Controller
    {
        private TuitionDbContext db = new TuitionDbContext();

        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        //public ActionResult FindTutor(string subject, string gender,
        //                               string qualification, string city)
        //{
        //    var tutors = db.Users.Where(u =>
        //        u.Role == "Tutor" &&
        //        u.IsVerified == true &&
        //        u.IsActive == true);

        //    if (!string.IsNullOrEmpty(subject))
        //        tutors = tutors.Where(u => u.Subjects.Contains(subject));

        //    if (!string.IsNullOrEmpty(gender))
        //        tutors = tutors.Where(u => u.Gender == gender);

        //    if (!string.IsNullOrEmpty(qualification))
        //        tutors = tutors.Where(u => u.Qualification == qualification);

        //    if (!string.IsNullOrEmpty(city))
        //        tutors = tutors.Where(u => u.City.Contains(city));

        //    ViewBag.Tutors = tutors.ToList();
        //    ViewBag.TotalCount = tutors.Count();
        //    return View();
        //}
        public ActionResult FindTutor(string subject, string gender,
                               string qualification, string city)
        {
            // Pehle list banao
            var tutorsList = db.Users.Where(u =>
                u.Role == "Tutor" &&
                u.IsVerified == true &&
                u.IsActive == true).ToList();

            // Filters apply karo
            if (!string.IsNullOrEmpty(subject))
                tutorsList = tutorsList
                    .Where(u => u.Subjects != null &&
                                u.Subjects.Contains(subject))
                    .ToList();

            if (!string.IsNullOrEmpty(gender))
                tutorsList = tutorsList
                    .Where(u => u.Gender == gender)
                    .ToList();

            if (!string.IsNullOrEmpty(qualification))
                tutorsList = tutorsList
                    .Where(u => u.Qualification == qualification)
                    .ToList();

            if (!string.IsNullOrEmpty(city))
                tutorsList = tutorsList
                    .Where(u => u.City != null &&
                                u.City.Contains(city))
                    .ToList();

            ViewBag.Tutors = tutorsList;
            ViewBag.TotalCount = tutorsList.Count;
            return View();
        }

        public ActionResult TutorDetail(int id)
        {
            var tutor = db.Users.Find(id);
            if (tutor == null || tutor.Role != "Tutor")
                return HttpNotFound();
            ViewBag.Tutor = tutor;
            return View();
        }

        // ========== HIRE TUTOR ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HireTutor(int tutorId)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int studentId = (int)Session["UserId"];

            var existing = db.HireRequests.FirstOrDefault(h =>
                h.StudentId == studentId &&
                h.TutorId == tutorId);

            if (existing == null)
            {
                var hire = new HireRequest
                {
                    StudentId = studentId,
                    TutorId = tutorId,
                    Status = "Pending",
                    RequestDate = DateTime.Now
                };
                db.HireRequests.Add(hire);
                db.SaveChanges();
                TempData["SuccessMsg"] = "Tutor ko hire request bhej di gayi! 🎉";
            }
            else
            {
                TempData["ErrorMsg"] = "Aapne pehle se yeh tutor hire kar rakha hai!";
            }

            return RedirectToAction("FindTutor");
        }

        public ActionResult MyTutors()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int studentId = (int)Session["UserId"];

            var myTutors = db.HireRequests
                             .Where(h => h.StudentId == studentId)
                             .Select(h => h.Tutor)
                             .ToList();

            ViewBag.MyTutors = myTutors;
            return View();
        }

        public new ActionResult Profile()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = (int)Session["UserId"];
            var student = db.Users.Find(userId);
            ViewBag.Student = student;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(string FullName, string Phone,
                                          string City, string Gender,
                                          string Class, string RequiredSubject,
                                          string NewPassword, string ConfirmPassword)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            int userId = (int)Session["UserId"];
            var student = db.Users.Find(userId);

            if (student != null)
            {
                student.FullName = FullName;
                student.Phone = Phone;
                student.City = City;
                student.Gender = Gender;
                student.Class = Class;
                student.RequiredSubject = RequiredSubject;

                if (!string.IsNullOrEmpty(NewPassword) &&
                    NewPassword == ConfirmPassword)
                {
                    student.Password = NewPassword;
                }

                db.SaveChanges();
                Session["Username"] = student.FullName;
                TempData["SuccessMsg"] = "Profile update ho gayi! 😊";
            }

            return RedirectToAction("Profile");
        }

        public ActionResult Contact()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

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
                UserRole = "Student",
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