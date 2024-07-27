
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService;
using BlogPlatform.Services.FollowService;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Controllers
{
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly GenericUser _genericUser;
        public FollowController(IFollowService followService, GenericUser genericUser)
        {
            _followService = followService;
            _genericUser = genericUser;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> FollowAsync([FromBody] FollowDto followDto){
            try{
                if(HttpContext.User!=null&&HttpContext.User.Identity!=null&&
                HttpContext.User.Identity.Name!=null){
                    var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                    if(user!=null)
                        return Ok(await _followService.FollowAsync(user, followDto));
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

        [HttpPost("un-follow")]
        public async Task<IActionResult> UnFollowAsync([FromBody] FollowDto followDto){
            try{
                if(HttpContext.User!=null&&HttpContext.User.Identity!=null&&
                HttpContext.User.Identity.Name!=null){
                    var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                    if(user!=null)
                        return Ok(await _followService.UnFollowAsync(user, followDto));
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

        [HttpGet("followers")]
        public async Task<IActionResult> GetFollowersAsync(){
            try{
                if(HttpContext.User!=null&&HttpContext.User.Identity!=null&&
                HttpContext.User.Identity.Name!=null){
                    var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                    if(user!=null)
                        return Ok(await _followService.GetFollowersAsync(user));
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

        [HttpGet("following")]
        public async Task<IActionResult> GetFolloweingAsync(){
            try{
                if(HttpContext.User!=null&&HttpContext.User.Identity!=null&&
                HttpContext.User.Identity.Name!=null){
                    var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                    if(user!=null)
                        return Ok(await _followService.GetFolloweingAsync(user));
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