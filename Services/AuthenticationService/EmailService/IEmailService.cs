using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models.ResponseModel;

namespace BlogPlatform.Services.AuthenticationService.EmailService
{
    public interface IEmailService
    {
        Task<ApiResponse<string>> ConfirmEmail(string userNameOrEmail, string token);
    }
}