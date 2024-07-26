using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.PostCommentsService
{
    public interface IPostCommentService
    {
        Task<ApiResponse<string>> AddPostCommentAsync(IdentityUser user, AddPostCommentDto commentDto);
        Task<ApiResponse<string>> UpdatePostCommentAsync(IdentityUser user, UpdatePostCommentDto commentDto);
        Task<ApiResponse<string>> DeletePostCommentAsync(IdentityUser user, string commentId);
        Task<ApiResponse<PostComment>> GetPostCommentByIdAsync(string commentId);
        Task<ApiResponse<IEnumerable<PostComment>>> GetPostCommentsAsync(string postId);
    }
}