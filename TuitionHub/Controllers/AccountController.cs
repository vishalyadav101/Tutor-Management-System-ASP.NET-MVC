using System;
using System.Linq;
using System.Web.Mvc;
using TuitionHub.Models;

namespace TuitionHub.Controllers
{
    public class AccountController : Controller
    {
        private TuitionDbContext db = new TuitionDbContext();

        // ========== LOGIN GET ==========
        public ActionResult Login()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        // ========== LOGIN POST ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Email aur Password dono bharein!";
                return View();
            }

            var user = db.Users.FirstOrDefault(u =>
                u.Email == Email &&
                u.Password == Password &&
                u.IsActive == true);

            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.FullName;
                Session["Role"] = user.Role;

                if (user.Role == "Admin")
                    return RedirectToAction("Dashboard", "Admin");
                else if (user.Role == "Tutor")
                    return RedirectToAction("Dashboard", "Tutor");
                else
                    return RedirectToAction("Dashboard", "Student");
            }

            ViewBag.Error = "Email ya Password galat hai!";
            return View();
        }

        // ========== REGISTER GET ==========
        public ActionResult Register()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        // ========== REGISTER POST ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(
            string FullName,
            string Email,
            string Phone,
            string City,
            string Gender,
            string Role,
            string Password,
            string ConfirmPassword,
            string Qualification,
            int? Experience,
            string Subjects,
            string Bio,
            string Class,
            string RequiredSubject)
        {
            // Validation
            if (string.IsNullOrEmpty(FullName))
            {
                ViewBag.Error = "Naam zaroori hai!";
                return View();
            }
            if (string.IsNullOrEmpty(Email))
            {
                ViewBag.Error = "Email zaroori hai!";
                return View();
            }
            if (string.IsNullOrEmpty(Password))
            {
                ViewBag.Error = "Password zaroori hai!";
                return View();
            }
            if (Password != ConfirmPassword)
            {
                ViewBag.Error = "Password aur Confirm Password match nahi kar rahe!";
                return View();
            }
            if (Password.Length < 6)
            {
                ViewBag.Error = "Password kam se kam 6 characters ka hona chahiye!";
                return View();
            }

            // Email duplicate check
            var existing = db.Users.FirstOrDefault(u => u.Email == Email);
            if (existing != null)
            {
                ViewBag.Error = "Yeh email pehle se registered hai!";
                return View();
            }

            // Naya user object banao
            var newUser = new User
            {
                FullName = FullName,
                Email = Email,
                Phone = Phone,
                City = City,
                Gender = Gender,
                Role = Role,
                Password = Password,
                IsActive = true,
                IsVerified = (Role == "Student"),
                JoinDate = DateTime.Now
            };

            // Tutor fields
            if (Role == "Tutor")
            {
                newUser.Qualification = Qualification;
                newUser.Experience = Experience;
                newUser.Subjects = Subjects;
                newUser.Bio = Bio;
                newUser.IsVerified = false;
            }

            // Student fields
            if (Role == "Student")
            {
                newUser.Class = Class;
                newUser.RequiredSubject = RequiredSubject;
            }

            // Database mein save karo
            db.Users.Add(newUser);
            db.SaveChanges();

            TempData["SuccessMsg"] = "Registration successful! Ab login karein. 🎉";
            return RedirectToAction("Login");
        }

        // ========== LOGOUT ==========
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //        db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}