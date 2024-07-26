
using BlogPlatform.Data.DTOs.AuthenticateUser;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService;
using BlogPlatform.Services.AuthenticationService.SettingsService;
using BlogPlatform.Services.SendEmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Controllers.Authentication
{
    [ApiController]
   
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        private readonly GenericUser _genericUser;
        private readonly ISendEmailService _sendEmailService;
        private readonly UserManager<IdentityUser> _userManager;
        public SettingsController(ISettingsService settingsService, GenericUser genericUser, 
            ISendEmailService sendEmailService, UserManager<IdentityUser> userManager)
        {
            _settingsService = settingsService;
            _genericUser = genericUser;
            _sendEmailService = sendEmailService;
            _userManager = userManager;
        }


        [HttpPost("reset-user-name")]
        public async Task<IActionResult> ResetUserNameAsync(string newUserName){
            try{
                if(HttpContext.User!=null&&HttpContext.User.Identity!=null
                &&HttpContext.User.Identity.Name!=null){
                    var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                    if(user!=null){
                        var isExistUser = await _genericUser.FindUser(newUserName);
                        if(isExistUser==null){
                            user.UserName = newUserName;
                            await _userManager.UpdateAsync(user);
                            return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<string>
                                ._200_Success_("User Name updated successfully"));
                        }
                        return StatusCode(StatusCodes.Status401Unauthorized, StatusCodeReturn<string>
                            ._403_Forbidden_("User Name taken before"));
                    }
                }
                return StatusCode(StatusCodes.Status401Unauthorized, StatusCodeReturn<string>
                    ._401_Un_Authorized_());
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPasswordAsync(string userNameOrEmail){
            try{
                var response = await _settingsService.ForgetPasswordAsync(userNameOrEmail);
                if(response.IsSuccess && response.Data!=null){
                    var url = Url.Action(nameof(GenerateResetPasswordObject), "Settings",
                        new{
                            token = response.Data.Token,
                            email = response.Data.User.Email
                        }, Request.Scheme);
                    var message = new MessageDto(new string[] { response.Data.User.Email! },
                            "Forget password", url!);
                    _sendEmailService.SendEmail(message);
                    return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<string>
                        ._200_Success_("Check your inbox"));
                } 
                return Ok(response);
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPasswordDto){
            try{
                return Ok(await _settingsService.ResetPasswordAsync(resetPasswordDto));
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("generatePasswordResetObject")]
        public ActionResult<object> GenerateResetPasswordObject(string email,string token)
        {
            var resetPasswordObject = new ResetPasswordDto
            {
                Email = email,
                Token = token
            };
            return StatusCode(StatusCodes.Status200OK, StatusCodeReturn<ResetPasswordDto>
                ._200_Success_(resetPasswordObject, "Reset password object created"));
        }

    }
}