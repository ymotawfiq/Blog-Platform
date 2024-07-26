using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService;
using BlogPlatform.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly GenericUser _genericUser;
        public PostController(IPostService postService, GenericUser genericUser)
        {
            _genericUser = genericUser;
            _postService = postService;
        }

        [HttpPost("add-post")]
        public async Task<IActionResult> AddPostAsync([FromBody] AddPostDto addPostDto){
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null 
                    && HttpContext.User.Identity.Name != null){
                        var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                        if(user!=null)
                            return Ok(await _postService.AddPostAsync(user, addPostDto));
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

        [HttpPut("update-post")]
        public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostDto postDto){
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null 
                    && HttpContext.User.Identity.Name != null){
                        var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                        if(user!=null)
                            return Ok(await _postService.UpdatePostAsync(user, postDto));
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

        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetPostAsync([FromRoute] string id){
            try{
                return Ok(await _postService.GetPostAsync(id));
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }
        
        [HttpDelete("delete-post/{id}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] string id){
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null 
                    && HttpContext.User.Identity.Name != null){
                        var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                        if(user!=null)
                            return Ok(await _postService.DeletePostAsync(user, id));
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

        [HttpGet("posts/{userNameOrid}")]
        public async Task<IActionResult> GetUserPostsAsync([FromRoute] string userNameOrid){
            try{
                var user = await _genericUser.FindUser(userNameOrid);
                if(user!=null)
                    return Ok(await _postService.GetPostsAsync(user));
                return StatusCode(StatusCodes.Status404NotFound, StatusCodeReturn<string>
                            ._404_Not_Found_("User not found"));
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetPostsAsync(){
            try{
                return Ok(await _postService.GetPostsAsync());
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }

    }
}