using System;
using System.Linq;
using System.Web.Mvc;
using TuitionHub.Models;

namespace TuitionHub.Controllers
{
    public class HomeController : Controller
    {
        private TuitionDbContext db = new TuitionDbContext();

        public ActionResult Index()
        {
            // Top tutors - verified, active, highest rating
            var topTutors = db.Users
                              .Where(u => u.Role == "Tutor" &&
                                          u.IsVerified == true &&
                                          u.IsActive == true)
                              .OrderByDescending(u => u.Rating)
                              .Take(4)
                              .ToList();

            ViewBag.TopTutors = topTutors;
            ViewBag.HeroTutors = topTutors.Take(3).ToList();

            ViewBag.TotalTutors = db.Users.Count(u => u.Role == "Tutor" && u.IsVerified == true);
            ViewBag.TotalStudents = db.Users.Count(u => u.Role == "Student");

            return View();
        }

        public ActionResult About() { return View(); }
        public ActionResult Faq() { return View(); }

        public ActionResult Contact() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(string Name, string Email,
                                    string Phone, string Type,
                                    string Subject, string Message)
        {
            if (string.IsNullOrEmpty(Name))
            {
                TempData["ErrorMsg"] = "Naam zaroori hai!";
                return RedirectToAction("Contact");
            }
            if (string.IsNullOrEmpty(Email))
            {
                TempData["ErrorMsg"] = "Email zaroori hai!";
                return RedirectToAction("Contact");
            }
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

            var contact = new ContactMessage
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Type = Type,
                Subject = Subject,
                Message = Message,
                Date = DateTime.Now,
                IsRead = false
            };
            db.ContactMessages.Add(contact);
            db.SaveChanges();

            TempData["SuccessMsg"] = "Aapka message mil gaya! Hum jald contact karenge.";
            return RedirectToAction("Contact");
        }
    }
}