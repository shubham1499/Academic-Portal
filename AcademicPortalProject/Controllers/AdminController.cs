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
    public class AdminController : Controller
    {
        public AcademicPortalContext db = new AcademicPortalContext();

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

        // GET: Homepage
        public ActionResult Homepage(int id)
        {
            if (Session["UserId"]!=null)
            {
                return View(db.Admins.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]
        public ActionResult SignUp(AdminTable admin)
        {
            try
            {
                // TODO: Add insert logic here
                admin.AdminPassword = getHash(admin.AdminPassword);
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Teacher/Create
        public ActionResult CreateTeacher()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }         
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Teacher/Create
        [HttpPost]
        public ActionResult CreateTeacher(TeacherTable teacher)
        {
            try
            {
                // TODO: Add insert logic here
                teacher.TeacherPassword = getHash(teacher.TeacherPassword);
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("TeacherIndex");
            }
            catch
            {
                return View();
            }
        }


        // GET: Teacher
        public ActionResult TeacherIndex()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Teachers.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }          
        }

        // GET: Student
        public ActionResult StudentIndex()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Students.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Student/Create
        public ActionResult CreateStudent()
        {
            if (Session["UserId"] != null)
            {
                List<CourseTable> list = db.Courses.ToList();
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (CourseTable course in list)
                {
                    items.Add(new SelectListItem
                    {
                        Text = course.CourseName,
                        Value = course.CourseId.ToString()
                    });
                }
                ViewBag.CourseId = items;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult CreateStudent(StudentTable student)
        {
            try
            {
                // TODO: Add insert logic here
                student.StudentPassword = getHash(student.StudentPassword);
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("StudentIndex");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course
        public ActionResult CourseIndex()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Courses.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Course/Create
        public ActionResult CreateCourse()
        {
            if (Session["UserId"] != null)
            {
                List<TeacherTable> list = db.Teachers.ToList();
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (TeacherTable teacher in list)
                {
                    items.Add(new SelectListItem
                    {
                        Text = teacher.TeacherName,
                        Value = teacher.TeacherId.ToString()
                    });
                }
                ViewBag.TeacherId = items;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult CreateCourse(CourseTable course)
        {
            try
            {
                // TODO: Add insert logic here
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("CourseIndex");
            }
            catch
            {
                return View();
            }
        }

        // GET: Teacher/Delete/5
        public ActionResult DeleteTeacher(int id)
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

        // POST: Teacher/Delete/5
        [HttpPost]
        public ActionResult DeleteTeacher(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                db.Teachers.Remove(db.Teachers.Find(id));
                db.SaveChanges();
                return RedirectToAction("TeacherIndex");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult DeleteStudent(int id)
        {
            if (Session["UserId"] != null)
            {
                return View(db.Students.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult DeleteStudent(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                db.Students.Remove(db.Students.Find(id));
                db.SaveChanges();
                return RedirectToAction("StudentIndex");
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Delete/5
        public ActionResult DeleteCourse(int id)
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

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult DeleteCourse(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                db.Courses.Remove(db.Courses.Find(id));
                db.SaveChanges();
                return RedirectToAction("CourseIndex");
            }
            catch
            {
                return View();
            }
        }
    }
}
