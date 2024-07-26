using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models.ResponseModel;

namespace BlogPlatform.Services.AuthenticationService.UserAccountService
{
    public interface IUserAccountService
    {
        Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto register);
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto login);
        Task<ApiResponse<LoginResponseDto>> LoginWithOTPAsync(string otp, string userNameOrEmail);
    }
}