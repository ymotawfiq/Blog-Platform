
using BlogPlatform.Data;
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using BlogPlatform.Services.AuthenticationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services.FollowService
{
    public class FollowService : IFollowService
    {
        private readonly AppDbContext _dbContext;
        private readonly GenericUser _genericUser;
        public FollowService(AppDbContext dbContext, GenericUser genericUser)
        {
            _dbContext = dbContext;
            _genericUser = genericUser;
        }
        public async Task<ApiResponse<string>> FollowAsync(IdentityUser user, FollowDto followDto)
        {
            var followedUser = await _genericUser.FindUser(followDto.IdOrNameOrEmail);
            if(followedUser==null)
                return StatusCodeReturn<string>
                        ._404_Not_Found_("User you want to follow not found");
            var isFollowing = await _dbContext.Follows.Where(e=>e.User1Id==followedUser.Id)
                .Where(e=>e.User2Id==user.Id).FirstOrDefaultAsync();
            if(isFollowing!=null)
                return StatusCodeReturn<string>
                        ._403_Forbidden_("You are following this user");
            if(user.Id == followedUser.Id)
                return StatusCodeReturn<string>
                        ._403_Forbidden_();
            await _dbContext.Follows.AddAsync(new Follow{User1Id=followedUser.Id, User2Id = user.Id});
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>
                        ._200_Success_("Followed successfully");
        }

        public async Task<ApiResponse<IEnumerable<Follow>>> GetFollowersAsync(IdentityUser user)
        {
            var followers = await _dbContext.Follows.Where(e=>e.User1Id == user.Id).ToListAsync();
            if(followers==null||followers.Count==0)
                return StatusCodeReturn<IEnumerable<Follow>>._204_No_Content_();
            return StatusCodeReturn<IEnumerable<Follow>>._200_Success_(followers);
        }

        public async Task<ApiResponse<string>> UnFollowAsync(IdentityUser user, FollowDto followDto)
        {
            var followedUser = await _genericUser.FindUser(followDto.IdOrNameOrEmail);
            if(followedUser==null)
                return StatusCodeReturn<string>
                        ._404_Not_Found_("User you want to un follow not found");
            var isFollowing = await _dbContext.Follows.Where(e=>e.User1Id==followedUser.Id)
                .Where(e=>e.User2Id==user.Id).FirstOrDefaultAsync();
            if(isFollowing==null)
                return StatusCodeReturn<string>
                        ._403_Forbidden_("You are not following this user");
            if(user.Id == followedUser.Id)
                return StatusCodeReturn<string>
                        ._403_Forbidden_();
            _dbContext.Follows.Remove(isFollowing);
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>
                        ._204_No_Content_("Un Followed successfully");
        }
    }
}