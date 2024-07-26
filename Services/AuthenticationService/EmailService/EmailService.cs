using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.AuthenticationService.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GenericUser _genericUser;
        public EmailService(UserManager<IdentityUser> userManager, GenericUser genericUser)
        {
            _userManager = userManager;
            _genericUser = genericUser;
        }
        public async Task<ApiResponse<string>> ConfirmEmail(string userNameOrEmail, string token)
        {
            var user = await _genericUser.FindUser(userNameOrEmail);
            if(user==null)
                return StatusCodeReturn<string>
                    ._404_Not_Found_("User not found");
            var confirmEmail = await _userManager.ConfirmEmailAsync(user, token);
            if(!confirmEmail.Succeeded)
                return StatusCodeReturn<string>
                    ._500_Internal_Server_Error_("Failed to confirm email");
            return StatusCodeReturn<string>
                    ._200_Success_("Email confirmed successfully");
        }
    }
}