using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models;

namespace BlogPlatform.Data.Models
{
    public class PostComment
    {
        public string Id {get; set;} = null!;
        public string UserId {get; set;} = null!;
        public string PostId {get; set;} = null!;
        public string Comment {get; set;} = null!;
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatededAt {get; set;}
        public User? User{get; set;}
        public Post? Post{get; set;}
        public PostComment()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}