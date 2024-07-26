using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BlogPlatform.Services.AuthenticationService.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly GenericUser _genericUser;
        public TokenService(UserManager<IdentityUser> userManager, IConfiguration configuration, 
            GenericUser genericUser)
        {
            _userManager = userManager;
            _configuration = configuration;
            _genericUser = genericUser;
        }
        public async Task<ApiResponse<EmailConfirmationDto>> GenerateEmailConfirmationTokenAsync(string userNameOrEmail)
        {
            var user = await _genericUser.FindUser(userNameOrEmail);
            if(user==null)
                return StatusCodeReturn<EmailConfirmationDto>
                        ._404_Not_Found_("User not found");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return StatusCodeReturn<EmailConfirmationDto>
                    ._201_Created_(new EmailConfirmationDto {Token = token, User = user});
        }

        public async Task<ApiResponse<ResetPasswordDto>> GenerateResetPasswordTokenAsync(string email)
        {
            var user = await _genericUser.FindUser(email);
            if(user==null)
                return StatusCodeReturn<ResetPasswordDto>
                        ._404_Not_Found_("User not found");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return StatusCodeReturn<ResetPasswordDto>
                    ._201_Created_(new ResetPasswordDto {Token = token, Email = email});
        }

        public async Task<JwtSecurityToken> GenerateUserToken(IdentityUser user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach(var role in userRoles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            return GetToken(claims);
        }

        public JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authKet = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var expirationTime = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);
            var localTimeZone = TimeZoneInfo.Local;
            var expirationTimeInLocalTimeZone = TimeZoneInfo.ConvertTimeFromUtc(expirationTime, localTimeZone);
            return new JwtSecurityToken(
                issuer : _configuration["JWT:ValidIssuer"],
                audience : _configuration["JWT:ValidAudience"],
                expires: expirationTimeInLocalTimeZone,
                claims : claims,
                signingCredentials : new SigningCredentials(authKet, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}