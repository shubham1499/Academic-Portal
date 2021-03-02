using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicPortalProject.Models
{
    public class CourseTable
    {
        //[Key]
        //public int CourseId { get; set; }

        //public string CourseName { get; set; }

        //public string CourseDescription { get; set; }

        //public int CourseCredits { get; set; }
        //public int TeacherId { get; set; }
        //public virtual TeacherTable TeacherTable { get; set; }
        //public virtual List<StudentTable> Students { get; set; }

        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int CourseCredits { get; set; }
        public int TeacherId { get; set; }
        public virtual TeacherTable TeacherTable { get; set; }
        public virtual List<StudentTable> Students { get; set; }
    }
}