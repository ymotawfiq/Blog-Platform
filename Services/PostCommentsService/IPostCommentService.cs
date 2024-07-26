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
        Task<ApiResponse<string>> AddPostCommentAsync(User user, AddPostCommentDto commentDto);
        Task<ApiResponse<string>> UpdatePostCommentAsync(User user, UpdatePostCommentDto commentDto);
        Task<ApiResponse<string>> DeletePostCommentAsync(User user, string commentId);
        Task<ApiResponse<PostComment>> GetPostCommentByIdAsync(string commentId);
        Task<ApiResponse<IEnumerable<PostComment>>> GetPostCommentsAsync(string postId);
    }
}