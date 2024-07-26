
using Microsoft.AspNetCore.Identity;

namespace BlogPlatform.Data.Models
{
    public class User : IdentityUser
    {
        public List<Post>? Posts {get; set;}
        public List<PostComment>? PostComments{get; set;}
        public List<Follow>? Follows1 {get; set;}
        public List<Follow>? Follows2 {get; set;}
    }
}