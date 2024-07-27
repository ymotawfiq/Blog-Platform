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
        private readonly UserManager<User> _userManager;
        public GenericUser(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> FindUser(string idOruserNameOrEmail){
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

        public async Task<User> SetUserToReturn(User user)
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