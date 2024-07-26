using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.Models
{
    public class Post
    {
        public string Id {get; set;} = null!;
        public string UserId {get; set;} = null!;
        public string Title {get; set;} = null!;
        public string Content {get; set;} = null!;
        public User? User{get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatededAt {get; set;}
        public List<PostComment>? PostComments{get; set;}
        public Post()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}