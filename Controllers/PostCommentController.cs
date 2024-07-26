
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService;
using BlogPlatform.Services.PostCommentsService;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Controllers
{
    [ApiController]
    public class PostCommentController : ControllerBase
    {
        private readonly IPostCommentService _postCommentService;
        private readonly GenericUser _genericUser;
        public PostCommentController(IPostCommentService postCommentService, GenericUser genericUser)
        {
            _postCommentService = postCommentService;
            _genericUser = genericUser;
        }   
        [HttpPost("add-post-comment")]
        public async Task<IActionResult> AddPostCommentAsync([FromBody] AddPostCommentDto postCommentDto){
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null 
                    && HttpContext.User.Identity.Name != null){
                        var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                        if(user!=null)
                            return Ok(await _postCommentService.AddPostCommentAsync(user, postCommentDto));
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

        [HttpPut("update-post-comment")]
        public async Task<IActionResult> UpdatePostCommentAsync([FromBody] UpdatePostCommentDto postCommentDto){
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null 
                    && HttpContext.User.Identity.Name != null){
                        var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                        if(user!=null)
                            return Ok(await _postCommentService.UpdatePostCommentAsync(user, postCommentDto));
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

        [HttpGet("post-comment/{id}")]
        public async Task<IActionResult> GetPostCommentAsync([FromRoute] string id){
            try{
                return Ok(await _postCommentService.GetPostCommentByIdAsync(id));
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }
        
        [HttpDelete("delete-post-comment/{id}")]
        public async Task<IActionResult> DeletePostCommentAsync([FromRoute] string id){
            try{
                if(HttpContext.User!=null && HttpContext.User.Identity!=null 
                    && HttpContext.User.Identity.Name != null){
                        var user = await _genericUser.FindUser(HttpContext.User.Identity.Name);
                        if(user!=null)
                            return Ok(await _postCommentService.DeletePostCommentAsync(user, id));
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

        [HttpGet("post-comments/{postId}")]
        public async Task<IActionResult> GetPostCommentsAsync([FromRoute] string postId){
            try{
                return Ok(await _postCommentService.GetPostCommentsAsync(postId));
            }
            catch(Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, StatusCodeReturn<string>
                    ._500_Internal_Server_Error_(e.Message));
            }
        }
    }
}