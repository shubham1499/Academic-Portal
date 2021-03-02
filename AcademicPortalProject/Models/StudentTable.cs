using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicPortalProject.Models
{
    public class StudentTable
    {
        //[Key]
        //public int StudentId { get; set; }
        //public string StudentName { get; set; }

        //[Required]
        //public string StudentEmail { get; set; }

        //[Required]
        //public string StudentPassword { get; set; }
        //public string StudentGender { get; set; }
        //public string StudentCity { get; set; }
        //public virtual List<CourseTable> Courses { get; set; }

        [Key]
        public int StudentId { get; set; }
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 5 characters required")]
        [StringLength(20, ErrorMessage = "Maximum characters exceeded")]
        public string StudentName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string StudentEmail { get; set; }
        public string StudentPassword { get; set; }
        public string StudentGender { get; set; }
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 5 characters required")]
        [StringLength(20, ErrorMessage = "Maximum characters exceeded")]
        public string StudentCity { get; set; }
        public virtual List<CourseTable> Courses { get; set; }
    }
}