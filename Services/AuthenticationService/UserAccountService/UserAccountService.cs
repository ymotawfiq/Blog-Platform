
using System.IdentityModel.Tokens.Jwt;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService.RolesService;
using BlogPlatform.Services.AuthenticationService.TokenService;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.AuthenticationService.UserAccountService
{
    public class UserAccountService : IUserAccountService
    {
        private IRolesService _rolesService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GenericUser _genericUser;
        private readonly ITokenService _tokenService;
        public UserAccountService(IRolesService rolesService, UserManager<User> userManager, 
            GenericUser genericUser, SignInManager<User> signInManager, 
                ITokenService tokenService)
        {
            _userManager = userManager;
            _rolesService = rolesService;
            _genericUser = genericUser;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterDto register)
        {
            if(!(await ValidateUserNameAndEmailAsync(register)).IsSuccess)
                return await ValidateUserNameAndEmailAsync(register);
            var user = new User{
                Email = register.Email,
                UserName = register.UserName
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if(result.Succeeded){
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _rolesService.AssignRolesToUserAsync(user, null!);
                var data = new RegisterResponseDto{
                    Token = token,
                    User = user
                };
                return StatusCodeReturn<RegisterResponseDto>
                    ._201_Created_(data, "Registerd successfully");
            }
            return StatusCodeReturn<RegisterResponseDto>
                    ._500_Internal_Server_Error_("Failed to create user");
        }


        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto login)
        {
            var user = await _genericUser.FindUser(login.UserNameOrEmail);
            if(user==null)
                return StatusCodeReturn<LoginResponseDto>
                        ._500_Internal_Server_Error_("Invalid user name or password");
            else if(!user.EmailConfirmed)
                return StatusCodeReturn<LoginResponseDto>
                        ._403_Forbidden_("Please Confirm Your Email");
            if(user.TwoFactorEnabled){
                var trySignIn = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);    
                if(trySignIn.Succeeded){
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    return StatusCodeReturn<LoginResponseDto>
                            ._201_Created_(new LoginResponseDto{Token = token, Type="Bearer Token"});
                }
                return StatusCodeReturn<LoginResponseDto>
                        ._500_Internal_Server_Error_("Invalid user name or password");
            }
            var signIn = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if(signIn.Succeeded)
                return StatusCodeReturn<LoginResponseDto>
                        ._201_Created_(new LoginResponseDto{
                            Token = new JwtSecurityTokenHandler().WriteToken(
                                await _tokenService.GenerateUserToken(user)
                            ),
                            Type = "Bearer token", 
                            User = new User{
                                UserName = user.UserName,
                                Email = user.Email
                            }
                        });
            return StatusCodeReturn<LoginResponseDto>
                        ._500_Internal_Server_Error_("Invalid user name or password");
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginWithOTPAsync(string otp, string userNameOrEmail){
            var user = await _genericUser.FindUser(userNameOrEmail);
            if(user==null)
                return StatusCodeReturn<LoginResponseDto>
                    ._500_Internal_Server_Error_("invalid otp or email");
            var signIn = await _signInManager.TwoFactorSignInAsync("Email", otp, false, false);
            if(signIn.Succeeded)
                return StatusCodeReturn<LoginResponseDto>
                        ._201_Created_(new LoginResponseDto{
                            Token = new JwtSecurityTokenHandler().WriteToken(
                                await _tokenService.GenerateUserToken(user)
                            ),
                            Type = "Bearer token", User = user
                        });
            return StatusCodeReturn<LoginResponseDto>
                    ._500_Internal_Server_Error_("invalid otp or email");
        }

        private async Task<ApiResponse<RegisterResponseDto>> ValidateUserNameAndEmailAsync(RegisterDto register){
            var user = await _userManager.FindByEmailAsync(register.Email);
            if(user!=null){
                return StatusCodeReturn<RegisterResponseDto>
                        ._403_Forbidden_("Email already taken before");
            }
            user = await _userManager.FindByNameAsync(register.UserName);
            if(user!=null){
                return StatusCodeReturn<RegisterResponseDto>
                        ._403_Forbidden_("User Name already taken before");
            }
            return StatusCodeReturn<RegisterResponseDto>._200_Success_(null!);
        }

        
    }
}