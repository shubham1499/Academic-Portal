using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AcademicPortalProject.Context;
using AcademicPortalProject.Models;
using System.Security.Cryptography;

namespace AcademicPortalProject.Controllers
{
    public class TeacherController : Controller
    {
        private static string getHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        AcademicPortalContext db = new AcademicPortalContext();

        public ActionResult Homepage(int id)
        {
            if (Session["UserId"] != null)
            {
                return View(db.Teachers.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int id)
        {
            if (Session["UserId"] != null)
            {
                return View(db.Teachers.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DetailsOfCourse(int id)
        {
            if (Session["UserId"] != null)
            {
                return View(db.Courses.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Teacher/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["UserId"] != null)
            {
                return View(db.Teachers.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        public ActionResult Edit(TeacherTable teacher)
        {
            try
            {
                // TODO: Add update logic here
                if (teacher.TeacherPassword.Length < 20)
                    teacher.TeacherPassword = getHash(teacher.TeacherPassword);
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomePage", new { id = teacher.TeacherId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Courses(int id)
        {
            if (Session["UserId"] != null)
            {
                return View(db.Teachers.Find(id).Courses);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult StudentsEnrolled(int id)
        {
            if (Session["UserId"] != null)
            {
                return View(db.Courses.Find(id).Students);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
