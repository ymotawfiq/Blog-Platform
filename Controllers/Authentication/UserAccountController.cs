using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService;
using BlogPlatform.Services.AuthenticationService.EmailService;
using BlogPlatform.Services.AuthenticationService.RolesService;
using BlogPlatform.Services.AuthenticationService.UserAccountService;
using BlogPlatform.Services.SendEmailService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Controllers.Authentication
{
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IRolesService _rolesService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISendEmailService _sendEmailService;
        private readonly IEmailService _emailService;
        private readonly GenericUser _genericUser;
        public UserAccountController(IUserAccountService userAccountService, IRolesService rolesService,
            UserManager<IdentityUser> userManager, ISendEmailService sendEmailService,
                IEmailService emailService, GenericUser genericUser)
        {
            _userAccountService = userAccountService;
            _rolesService = rolesService;
            _userManager = userManager;
            _sendEmailService = sendEmailService;
            _emailService = emailService;
            _genericUser = genericUser;
        }
        
        [AllowAnonymous]
        [HttpPost("resend-confirmation-email-link")]
        public async Task<IActionResult> ResendEmailConfirmationLinkAsync(string userNameOrEmail)
        {
            try
            {
                var user = await _genericUser.FindUser(userNameOrEmail);
                if(user==null)
                    return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<string>
                        ._200_Success_("Check your inbox"));
                else if(user.EmailConfirmed)
                    return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<string>
                        ._403_Forbidden_("Email already confirmed"));
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "UserAccount",
                            new
                            {
                                token = token,
                                userNameOrEmail = userNameOrEmail
                            }, Request.Scheme);

                        var message = new MessageDto(new string[] { user.Email! }
                        , "Confirm email link", confirmationLink!);

                        _sendEmailService.SendEmail(message);
                        return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<string>
                            ._200_Success_("Email confirmation link resent successfully"));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    StatusCodeReturn<string>._500_Internal_Server_Error_(ex.Message));
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            try
            {
                var tokenResponse = await _userAccountService.RegisterAsync(registerDto);
                if (tokenResponse.IsSuccess && tokenResponse.Data != null)
                {
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "UserAccount",
                        new
                        {
                            token = tokenResponse.Data.Token,
                            userNameOrEmail = tokenResponse.Data.User.UserName
                        }, Request.Scheme);

                    var message = new MessageDto(new string[] { registerDto.Email },
                        "Confirmation Email Link", confirmationLink!);
                    _sendEmailService.SendEmail(message);
                    string msg = $"Email confirmation link sent to your email please check your inbox and confirm your email";
                    return StatusCode(StatusCodes.Status200OK, 
                        StatusCodeReturn<string>._200_Success_(msg));
                }
                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    StatusCodeReturn<string>._500_Internal_Server_Error_(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userNameOrEmail, string token)
        {
            var result = await _emailService.ConfirmEmail(userNameOrEmail, token);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto login){
            try{
                var loginData = await _userAccountService.LoginAsync(login);
                if(loginData!=null&&loginData.Data!=null){
                    var user = loginData.Data.User; 
                    if(user.TwoFactorEnabled){
                        var message = new MessageDto(new string[]{user.Email!}, 
                            "OTP Code", loginData.Data.Token);
                        _sendEmailService.SendEmail(message);
                        return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<string>
                                ._200_Success_("OTP sent successfully to your email"));
                    }
                }
                return Ok(loginData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    StatusCodeReturn<string>._500_Internal_Server_Error_(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("login-2FA")]
        public async Task<IActionResult> LoginTwoFactorAuthenticationAsync(string otp, string userNameOrEmail)
        {
            try
            {
                var response = await _userAccountService.LoginWithOTPAsync(otp, userNameOrEmail);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    StatusCodeReturn<string>._500_Internal_Server_Error_(ex.Message));
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            if (HttpContext.User != null && HttpContext.User.Identity != null
                    && HttpContext.User.Identity.Name != null)
            {
                await HttpContext.SignOutAsync();
                return StatusCode(StatusCodes.Status400BadRequest, StatusCodeReturn<string>
                    ._200_Success_("Logged out successfully"));
            }
            return StatusCode(StatusCodes.Status400BadRequest, StatusCodeReturn<string>
                ._403_Forbidden_("You are not logged in"));
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (HttpContext.User != null && HttpContext.User.Identity != null &&
                HttpContext.User.Identity.Name != null)
            {
                var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (currentUser != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new ApiResponse<object>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Message = "User founded successfully",
                        Data = new
                        {
                            Email = currentUser.Email,
                            PhoneNumber = currentUser.PhoneNumber,
                            UserName = currentUser.UserName
                        }
                    });
                }
                return StatusCode(StatusCodes.Status404NotFound, 
                    StatusCodeReturn<string>._404_Not_Found_("User not found"));
            }
            return StatusCode(StatusCodes.Status401Unauthorized,
                StatusCodeReturn<string>._401_Un_Authorized_());
        }

        [HttpGet("u/{userName}")]
        public async Task<IActionResult> GetUserByUserNameAsync([FromRoute] string userName)
        {
            try
            {
                var userByUserName = await _userManager.FindByNameAsync(userName);
                if (userByUserName == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, StatusCodeReturn<string>
                        ._404_Not_Found_("User not found"));
                }
                if (HttpContext.User != null && HttpContext.User.Identity != null &&
                HttpContext.User.Identity.Name != null)
                {
                    var loggedInUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                    if (loggedInUser != null)
                    {
                        
                        if(userByUserName.UserName == loggedInUser.UserName)
                        {
                            var Object1 = new
                            {
                                Email = loggedInUser.Email,
                                PhoneNumber = loggedInUser.PhoneNumber,
                                UserName = loggedInUser.UserName,
                            };
                            return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<object>
                                ._200_Success_(Object1, "User found successfully"));
                        }
                    }
                }
                var Object = new
                {
                    UserName = userByUserName.UserName
                };
                return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<object>
                                ._200_Success_(Object, "User found successfully"));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    StatusCodeReturn<string>._500_Internal_Server_Error_(ex.Message));
            }
        }
    }
}