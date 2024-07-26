using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.DTOs.AuthenticateUser
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Please Enter UserName Or Email")]
        public string UserNameOrEmail {get; set;} = null!;

        [Required]
        public string Password {get; set;} = null!;
    }
}