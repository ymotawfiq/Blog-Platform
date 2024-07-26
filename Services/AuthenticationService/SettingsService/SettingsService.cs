using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.AuthenticationService.SettingsService
{
    public class SettingsService : ISettingsService
    {
        private readonly GenericUser _genericUser;
        private readonly UserManager<IdentityUser> _userManager;
        public SettingsService(GenericUser genericUser, UserManager<IdentityUser> userManager)
        {
            _genericUser = genericUser;
            _userManager = userManager;
        }
        public async Task<ApiResponse<ForgetPasswordResponseDto>> ForgetPasswordAsync(string UserNameOrEmail)
        {
            var user = await _genericUser.FindUser(UserNameOrEmail);
            if(user==null)
                return StatusCodeReturn<ForgetPasswordResponseDto>
                    ._404_Not_Found_("User not found");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return StatusCodeReturn<ForgetPasswordResponseDto>
                    ._201_Created_(new ForgetPasswordResponseDto{
                        Token = token,
                        Type="PasswordResetToken",
                        User = user
                    }, "Check your inbox");
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return StatusCodeReturn<string>
                    ._404_Not_Found_("User not found");
            var result = await _userManager.ResetPasswordAsync(user,
                resetPasswordDto.Token, resetPasswordDto.Password);
            if (result.Succeeded)
                return StatusCodeReturn<string>
                    ._200_Success_("Password reset successfully");
            return StatusCodeReturn<string>
                ._500_Internal_Server_Error_("Failed to reset password");
        }
    }
}