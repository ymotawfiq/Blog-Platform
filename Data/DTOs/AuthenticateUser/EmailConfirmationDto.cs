using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Data.DTOs.AuthenticateUser
{
    public class EmailConfirmationDto
    {
        public User User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}