using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data;
using BlogPlatform.Data.DTOs;
using BlogPlatform.Data.Models;
using BlogPlatform.Data.Models.ResponseModel;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPlatform.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _dbContext;
        public PostService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse<string>> AddPostAsync(IdentityUser user, AddPostDto postDto)
        {
            var existPost = await _dbContext.Post.Where(e=>e.Title.ToUpper()==postDto.Title.ToUpper()).FirstOrDefaultAsync();
            if(existPost!=null)
                return StatusCodeReturn<string>
                    ._403_Forbidden_("Post with this title already exists");
            await _dbContext.Post.AddAsync(new Post{
                Content = postDto.Content, Title = postDto.Title, UserId = user.Id
            });
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>
                    ._201_Created_("Post created successfully");
        }

        public async Task<ApiResponse<string>> DeletePostAsync(IdentityUser user, string postId)
        {
            var existPost1 = await _dbContext.Post.Where(e=>e.Id == postId)
                .Where(e=>e.UserId == user.Id).FirstOrDefaultAsync();
            if(existPost1==null)
                return StatusCodeReturn<string>
                    ._403_Forbidden_();
            _dbContext.Post.Remove(existPost1);
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>._204_No_Content_("Post deleted successfully");
        }

        public async Task<ApiResponse<Post>> GetPostAsync(string postId)
        {
            var post = await _dbContext.Post.Where(e=>e.Id==postId).FirstOrDefaultAsync();
            if(post==null)
                return StatusCodeReturn<Post>._404_Not_Found_("Post not found");
            return StatusCodeReturn<Post>._200_Success_(post);
        }

        public async Task<ApiResponse<IEnumerable<Post>>> GetPostsAsync(IdentityUser user)
        {
            var posts = await _dbContext.Post.Where(e=>e.UserId == user.Id).ToListAsync();
            if(posts==null||posts.Count==0)
                return StatusCodeReturn<IEnumerable<Post>>._204_No_Content_();
            return StatusCodeReturn<IEnumerable<Post>>._200_Success_(posts);
        }

        public async Task<ApiResponse<IEnumerable<Post>>> GetPostsAsync()
        {
            var posts = await _dbContext.Post.ToListAsync();
            if(posts==null||posts.Count==0)
                return StatusCodeReturn<IEnumerable<Post>>._204_No_Content_();
            return StatusCodeReturn<IEnumerable<Post>>._200_Success_(posts);
        }

        public async Task<ApiResponse<string>> UpdatePostAsync(IdentityUser user, UpdatePostDto postDto)
        {
            var existPost1 = await _dbContext.Post.Where(e=>e.Id == postDto.Id)
                .Where(e=>e.UserId == user.Id).FirstOrDefaultAsync();
            if(existPost1==null)
                return StatusCodeReturn<string>
                    ._403_Forbidden_();
            var existPost2 = await _dbContext.Post
                .Where(e=>e.Title.ToUpper()==postDto.Title.ToUpper()).FirstOrDefaultAsync();
            if(existPost2!=null)
                return StatusCodeReturn<string>
                    ._403_Forbidden_("Post with this title already exists");
            existPost1.Title = postDto.Title;
            existPost1.Content = postDto.Content;
            existPost1.UpdatededAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>
                    ._200_Success_("Post updated successfully");
        }
    }
}