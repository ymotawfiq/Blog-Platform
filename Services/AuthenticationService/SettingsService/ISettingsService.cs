using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models.ResponseModel;

namespace BlogPlatform.Services.AuthenticationService.SettingsService
{
    public interface ISettingsService
    {
        Task<ApiResponse<ForgetPasswordResponseDto>> ForgetPasswordAsync(string UserNameOrEmail);
        Task<ApiResponse<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}