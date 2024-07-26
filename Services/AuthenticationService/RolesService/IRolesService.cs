using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.AuthenticationService.RolesService
{
    public interface IRolesService
    {
        Task<ApiResponse<List<string>>> AssignRolesToUserAsync(User user, List<string> roles);
    }
}