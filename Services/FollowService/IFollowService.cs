using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.FollowService
{
    public interface IFollowService
    {
        Task<ApiResponse<string>> FollowAsync(User user, FollowDto followDto);
        Task<ApiResponse<string>> UnFollowAsync(User user, FollowDto followDto);
        Task<ApiResponse<IEnumerable<Follow>>> GetFollowersAsync(User user);
        Task<ApiResponse<IEnumerable<Follow>>> GetFolloweingAsync(User user);
    }
}