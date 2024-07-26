using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.Models
{
    public class Follow
    {
        public string Id {get; set;}
        public string User1Id {get; set;} = null!;
        public string User2Id {get; set;} = null!;
        public User? User1{get; set;}
        public User? User2 {get; set;}
        public Follow()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}