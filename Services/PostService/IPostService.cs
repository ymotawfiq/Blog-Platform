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
        Task<ApiResponse<string>> AddPostAsync(User user, AddPostDto postDto);
        Task<ApiResponse<string>> UpdatePostAsync(User user, UpdatePostDto postDto);
        Task<ApiResponse<string>> DeletePostAsync(User user, string postId);
        Task<ApiResponse<Post>> GetPostByIdAsync(string postId);
        Task<ApiResponse<Post>> GetPostByTitleAsync(string title);
        Task<ApiResponse<IEnumerable<Post>>> GetPostsAsync(User user);
        Task<ApiResponse<IEnumerable<Post>>> GetPostsAsync();
    }
}