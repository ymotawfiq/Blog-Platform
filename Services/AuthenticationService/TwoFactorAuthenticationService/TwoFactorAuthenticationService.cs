using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.AuthenticationService.TwoFactorAuthenticationService
{
    public class TwoFactorAuthenticationService : ITwoFactorAuthenticationService
    {
        private readonly GenericUser _genericUser;
        private readonly UserManager<User> _userManager;
        public TwoFactorAuthenticationService(GenericUser genericUser, UserManager<User> userManager)
        {
            _genericUser = genericUser;
            _userManager = userManager;
        }
        public async Task<ApiResponse<string>> EnableTwoFactorAuthenticationAsync(string emailOrUserName)
        {
            var user = await _genericUser.FindUser(emailOrUserName);
            if (user == null)
                return StatusCodeReturn<string>
                    ._404_Not_Found_("User not found");
            if(user.TwoFactorEnabled)
                return StatusCodeReturn<string>
                    ._403_Forbidden_("Two factor authentication active");    
            await _userManager.SetTwoFactorEnabledAsync(user, true);
            await _userManager.UpdateAsync(user);
            return StatusCodeReturn<string>
                ._200_Success_("Two factor authentication enabled successfully");
        }

        public async Task<ApiResponse<string>> DisableTwoFactorAuthenticationAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return StatusCodeReturn<string>
                    ._404_Not_Found_("User not found");
            
            if(!user.TwoFactorEnabled)
                return StatusCodeReturn<string>
                    ._403_Forbidden_("Two factor authentication not active");    
            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.UpdateAsync(user);
            return StatusCodeReturn<string>
                ._200_Success_("Two factor authentication enabled successfully");
        }
    }
}