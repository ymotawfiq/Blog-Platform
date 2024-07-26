using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services.AuthenticationService.RolesService
{
    public class RolesService : IRolesService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ApiResponse<List<string>>> AssignRolesToUserAsync(IdentityUser user, List<string> roles)
        {
            roles = NormalizeRoles(roles);
            if(roles.Contains("admin".ToUpper()))
                return await AssignRolesToAdminAsync(user);
            List<string> assignedRoles = new();
            for(int i=0; i<roles.Count; i++){
                if(await _roleManager.RoleExistsAsync(roles[i])){
                    if(!await _userManager.IsInRoleAsync(user, roles[i])){
                        await _userManager.AddToRoleAsync(user, roles[i]);
                        assignedRoles.Add(roles[i]);
                    }
                }
            }
            return StatusCodeReturn<List<string>>
                    ._201_Created_(assignedRoles, "Roles assigned to user successfully");
        }

        private List<string> NormalizeRoles(List<string> roles){
            if(roles == null || roles.Count == 0) return new List<string>{"USER"};
            for(int i=0; i<roles.Count; i++) roles[i] = roles[i].ToUpper();
            return roles;
        }

        private async Task<ApiResponse<List<string>>> AssignRolesToAdminAsync(IdentityUser user){
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            List<string> assignedRoles = new();
            foreach(var role in roles){
                if(!await _userManager.IsInRoleAsync(user, role.Name!)){
                    await _userManager.AddToRoleAsync(user, role.Name!);
                    assignedRoles.Add(role.Name!);
                }
            }
            return StatusCodeReturn<List<string>>
                    ._201_Created_(assignedRoles, "Roles assigned to user successfully");
        }

    }
}