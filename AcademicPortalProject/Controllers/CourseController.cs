using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicPortalProject.Models;
using AcademicPortalProject.Context;
using System.Data.Entity;

namespace AcademicPortalProject.Controllers
{

    public class CourseController : Controller
    {
        public AcademicPortalContext db = new AcademicPortalContext();

        // GET: Course/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Courses.Find(id));
        }


        // GET: Course/Edit/5
        public ActionResult Edit(int id)
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
            return View(db.Courses.Find(id));
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(CourseTable course)
        {
            try
            {
                // TODO: Add update logic here
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult StudentsEnrolled(int id)
        {
            return View(db.Courses.Find(id).Students);
        }
    }
}
