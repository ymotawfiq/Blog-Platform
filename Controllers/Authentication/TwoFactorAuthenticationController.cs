
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService;
using BlogPlatform.Services.AuthenticationService.TwoFactorAuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Controllers.Authentication
{
    [ApiController]
    public class TwoFactorAuthenticationController : ControllerBase
    {
        private readonly GenericUser _genericUser;
        private readonly ITwoFactorAuthenticationService _twoFactoAuthenticationService;
        public TwoFactorAuthenticationController(GenericUser genericUser,
            ITwoFactorAuthenticationService twoFactoAuthenticationService)
        {
            _genericUser = genericUser;
            _twoFactoAuthenticationService = twoFactoAuthenticationService;
        }

        [HttpPost("enable-2FA")]
        public async Task<IActionResult> EnableTwoFactorAuthenticationAsync()
        {
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null
                    && HttpContext.User.Identity.Name!=null){
                    var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                    if(user!=null){
                        var response = await _twoFactoAuthenticationService
                            .EnableTwoFactorAuthenticationAsync(user.Email!);
                        return Ok(response);
                    }
                    return StatusCode(StatusCodes.Status404NotFound, StatusCodeReturn<string>
                        ._404_Not_Found_("User not found"));
                }
                return StatusCode(StatusCodes.Status401Unauthorized, StatusCodeReturn<string>
                    ._401_Un_Authorized_());
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }

        [HttpPost("disable-2FA")]
        public async Task<IActionResult> DisableTwoFactorAuthenticationAsync()
        {
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null
                    && HttpContext.User.Identity.Name!=null){
                    var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                    if(user!=null){
                        var response = await _twoFactoAuthenticationService
                            .DisableTwoFactorAuthenticationAsync(user.Email!);
                        return Ok(response);
                    }
                    return StatusCode(StatusCodes.Status404NotFound, StatusCodeReturn<string>
                        ._404_Not_Found_("User not found"));
                }
                return StatusCode(StatusCodes.Status401Unauthorized, StatusCodeReturn<string>
                    ._401_Un_Authorized_());
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }   
    }
}