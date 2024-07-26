using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Data.DTOs.AuthenticateUser
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Please Enter UserName")]
        public string UserName {get; set;} = null!;

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        [RegularExpression("^\\S+@\\S+\\.\\S+$")]
        public string Email {get; set;} = null!;

        [Required]
        public string Password {get; set;} = null!;
        
        [Required]
        [Compare("Password", ErrorMessage = "Password and confirmed password doesn't match....")]
        public string ConfirmPassword {get; set;} = null!;
    }
}