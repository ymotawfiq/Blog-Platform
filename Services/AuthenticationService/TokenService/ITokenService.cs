using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.AuthenticationService.TokenService
{
    public interface ITokenService
    {
        public Task<JwtSecurityToken> GenerateUserToken(IdentityUser user);
        public JwtSecurityToken GetToken(List<Claim> claims);
        public Task<ApiResponse<ResetPasswordDto>> GenerateResetPasswordTokenAsync(string email);
        public Task<ApiResponse<EmailConfirmationDto>> GenerateEmailConfirmationTokenAsync(string userNameOrEmail);
    }
}