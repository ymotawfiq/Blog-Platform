using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models.ResponseModel;

namespace BlogPlatform.Services.AuthenticationService.TwoFactorAuthenticationService
{
    public interface ITwoFactorAuthenticationService
    {
        Task<ApiResponse<string>> EnableTwoFactorAuthenticationAsync(string email);
        Task<ApiResponse<string>> DisableTwoFactorAuthenticationAsync(string email);
    }
}