using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage ="First name required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage ="Please  enter your email")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage ="Please enter vallied email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please  enter a strong passwoed")]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword",ErrorMessage ="Password doesn't match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please  Confirm password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
