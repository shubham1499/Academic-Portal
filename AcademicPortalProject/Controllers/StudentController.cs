using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicPortalProject.Models;
using AcademicPortalProject.Context;
using System.Data.Entity;
using System.Security.Cryptography;

namespace AcademicPortalProject.Controllers
{
    public class StudentController : Controller
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

        public ActionResult Homepage(int id)
        {
            //if (Session["UserId"] != null)
            //{
                return View(db.Students.Find(id));
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}         
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
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

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
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
                return View(db.Students.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentTable student)
        {
            try
            {
                // TODO: Add update logic here
                if (student.StudentPassword.Length < 20)
                    student.StudentPassword = getHash(student.StudentPassword);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Homepage", new { id = student.StudentId });
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
                ViewBag.StudentId = id;
                return View(db.Students.Find(id).Courses);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CourseEnrollment(int id)
        {
            if (Session["UserId"] != null)
            {
                List<CourseTable> allCourses = db.Courses.ToList();
                List<CourseTable> enrolledCourses = db.Students.Find(id).Courses.ToList();
                List<SelectListItem> items = new List<SelectListItem>();

                foreach (CourseTable course in allCourses)
                {
                    var flg = 1;
                    foreach (CourseTable mycourse in enrolledCourses)
                    {
                        if (course.CourseId == mycourse.CourseId)
                        {
                            flg = 0;
                            break;
                        }
                    }
                    if (flg == 1)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = course.CourseName,
                            Value = course.CourseId.ToString()
                        });
                    }
                }
                ViewBag.CourseId = items;
                ViewBag.StudentId = id;
                return View(db.Students.Find(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CourseEnrollment(FormCollection form)
        {
            CourseTable course = db.Courses.Find(int.Parse(form["CourseId"]));
            db.Students.Find(int.Parse(form["StudentId"])).Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Courses", new { id = Int32.Parse(form["StudentId"]) });
        }
    }
}
