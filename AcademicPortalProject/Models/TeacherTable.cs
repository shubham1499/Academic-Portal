using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace AcademicPortalProject.Models
{
    public class TeacherTable
    {
        //[Key]
        //public int TeacherId { get; set; }       
        //public string TeacherName { get; set; }       
        //public string TeacherEmail { get; set; }
        //public string TeacherPassword { get; set; }
        //public string TeacherGender { get; set; }
        //public string TeacherCity { get; set; }
        //public virtual List<CourseTable> Courses { get; set; }

        [Key]
        public int TeacherId { get; set; }
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 5 characters required")]
        [StringLength(20, ErrorMessage = "Maximum characters exceeded")]
        public string TeacherName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string TeacherEmail { get; set; }
        public string TeacherPassword { get; set; }
        public string TeacherGender { get; set; }
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 5 characters required")]
        [StringLength(20, ErrorMessage = "Maximum characters exceeded")]
        public string TeacherCity { get; set; }
        public virtual List<CourseTable> Courses { get; set; }
    }
}