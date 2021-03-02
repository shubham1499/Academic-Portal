using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicPortalProject.Models
{
    public class AdminTable
    {
        //[Key]
        //public int AdminId { get; set; }
        //public string AdminName { get; set; }
        //public string AdminEmail { get; set; }
        //public string AdminPassword { get; set; }
        //public string AdminGender { get; set; }

        [Key]
        public int AdminId { get; set; }
        [Required]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 5 characters required")]
        [StringLength(20, ErrorMessage = "Maximum characters exceeded")]
        public string AdminName { get; set; }   
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminGender { get; set; }
    }
}