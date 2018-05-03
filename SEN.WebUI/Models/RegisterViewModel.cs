using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEN.WebUI.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Username ")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1}")]
        [RegularExpression(pattern: @"^[\w ]+$", ErrorMessage = "This is invalid username")]
        public string FirstName { get; set; }

        [Display(Name = "Username ")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1}")]
        [RegularExpression(pattern: @"^[\w ]+$", ErrorMessage = "This is invalid username")]
        public string LastName { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} cannot longer than {1}")]
        [RegularExpression(pattern: "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "This is invalid email")]
        public string Email { get; set; }

        public string Mobile { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} must be between {2} and {1}")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "{0} must matched !")]
        public string ConfirmPassword { get; set; }
    }
}