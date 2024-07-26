using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Data.DTOs.AuthenticateUser
{
    public class EmailConfirmationDto
    {
        public IdentityUser User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}