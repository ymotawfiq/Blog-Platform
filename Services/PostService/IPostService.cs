using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Services.PostService
{
    public interface IPostService
    {
        Task<ApiResponse<string>> AddPostAsync(IdentityUser user, AddPostDto postDto);
        Task<ApiResponse<string>> UpdatePostAsync(IdentityUser user, UpdatePostDto postDto);
        Task<ApiResponse<string>> DeletePostAsync(IdentityUser user, string postId);
        Task<ApiResponse<Post>> GetPostAsync(string postId);
        Task<ApiResponse<IEnumerable<Post>>> GetPostsAsync(IdentityUser user);
        Task<ApiResponse<IEnumerable<Post>>> GetPostsAsync();
    }
}