using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Services.AuthenticationService
{
    public class GenericUser
    {
        private readonly UserManager<IdentityUser> _userManager;
        public GenericUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> FindUser(string idOruserNameOrEmail){
            var userByEmail = await _userManager.FindByEmailAsync(idOruserNameOrEmail);
            var userByName = await _userManager.FindByNameAsync(idOruserNameOrEmail);
            var userById = await _userManager.FindByIdAsync(idOruserNameOrEmail);
            if (userByName != null)
                return userByName;
            else if (userByEmail != null)
                return userByEmail;
            else if (userById != null)
                return userById;
            return null!;
        }

        public User SetUserToReturn(User user)
        {
            if (user != null)
            {
                return new User
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
            return null!;
        }


    }
}