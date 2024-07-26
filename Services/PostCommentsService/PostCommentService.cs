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

namespace BlogPlatform.Services.PostCommentsService
{
    public class PostCommentService : IPostCommentService
    {
        private readonly AppDbContext _dbContext;
        public PostCommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse<string>> AddPostCommentAsync(IdentityUser user, AddPostCommentDto commentDto)
        {
            await _dbContext.PostComments.AddAsync(new PostComment{
                Comment = commentDto.Comment, UserId = user.Id, PostId = commentDto.PostId
            });
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>
                ._201_Created_("Commented successfully");
        }

        public async Task<ApiResponse<string>> DeletePostCommentAsync(IdentityUser user, string commentId)
        {
            var comment = await _dbContext.PostComments.Where(e=>e.Id == commentId)
                .Where(e=>e.UserId==user.Id).FirstOrDefaultAsync();
            if(comment==null)
                return StatusCodeReturn<string>._403_Forbidden_();
            _dbContext.PostComments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>
                ._204_No_Content_("Comment deleted successfully");
        }

        public async Task<ApiResponse<PostComment>> GetPostCommentByIdAsync(string commentId)
        {
            var comment = await _dbContext.PostComments.Where(e=>e.Id == commentId).FirstOrDefaultAsync();
            if(comment==null)
                return StatusCodeReturn<PostComment>._404_Not_Found_("Comment not found");
            return StatusCodeReturn<PostComment>
                ._200_Success_(comment);
        }

        public async Task<ApiResponse<IEnumerable<PostComment>>> GetPostCommentsAsync(string postId)
        {
            var postComments = await _dbContext.PostComments.Where(e=>e.PostId == postId).ToListAsync();
            if(postComments==null||postComments.Count==0)
                return StatusCodeReturn<IEnumerable<PostComment>>._204_No_Content_();
            return StatusCodeReturn<IEnumerable<PostComment>>._200_Success_(postComments);
        }

        public async Task<ApiResponse<string>> UpdatePostCommentAsync(IdentityUser user, UpdatePostCommentDto commentDto)
        {
            var comment = await _dbContext.PostComments.Where(e=>e.Id == commentDto.Id)
                .Where(e=>e.UserId==user.Id).FirstOrDefaultAsync();
            if(comment==null)
                return StatusCodeReturn<string>._403_Forbidden_();
            comment.Comment = commentDto.Comment;
            comment.UpdatededAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return StatusCodeReturn<string>
                ._200_Success_("Comment updated successfully");
        }
    }
}