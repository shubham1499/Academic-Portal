using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AcademicPortalProject.Context;
using System.Security.Cryptography;

namespace AcademicPortalProject.Controllers
{
    public class HomeController : Controller
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

        // GET: Home
        public ActionResult Index()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Student", Value = "1" });
            items.Add(new SelectListItem { Text = "Teacher", Value = "2" });
            items.Add(new SelectListItem { Text = "Admin", Value = "3" });

            ViewBag.Token = items;
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
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

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                using (AcademicPortalContext db = new AcademicPortalContext())
                {
                    String email = form["EmailId"];
                    String Password = getHash(form["Password"]);
                    if (int.Parse(form["Token"]) == 1)
                    {
                        var obj = db.Students.Where(a => a.StudentEmail.Equals(email) && a.StudentPassword.Equals(Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserId"] = obj.StudentId.ToString();
                            Session["Token"] = "1";
                            return RedirectToAction("Homepage", "Student", new { id = obj.StudentId });
                        }
                        else
                            return RedirectToAction("Index");
                    }
                    else if (int.Parse(form["Token"]) == 2)
                    {
                        var obj = db.Teachers.Where(a => a.TeacherEmail.Equals(email) && a.TeacherPassword.Equals(Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserId"] = obj.TeacherId.ToString();
                            Session["Token"] = "2";
                            return RedirectToAction("Homepage", "Teacher", new { id = obj.TeacherId });
                        }
                        else
                            return RedirectToAction("Index");
                    }
                    else
                    {
                        var obj = db.Admins.Where(a => a.AdminEmail.Equals(email) && a.AdminPassword.Equals(Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserId"] = obj.AdminId.ToString();
                            Session["Token"] = "3";
                            return RedirectToAction("Homepage", "Admin", new { id = obj.AdminId });
                        }
                        else
                            return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}
