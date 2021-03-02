using System;
using System.Collections.Generic;
using System.Data.Entity;
using AcademicPortalProject.Models;
using System.Linq;
using System.Web;

namespace AcademicPortalProject.Context
{
    public class AcademicPortalContext : DbContext
    {
        public AcademicPortalContext() : base("ConStr")
        {

        }
        
        public DbSet<StudentTable> Students { get; set; }
        public DbSet<TeacherTable> Teachers { get; set; }
        public DbSet<CourseTable> Courses { get; set; }
        public DbSet<AdminTable> Admins { get; set; }
    }
}