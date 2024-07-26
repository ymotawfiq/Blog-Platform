using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.DTOs
{
    public class AddPostCommentDto
    {
        [Required]
        public string Comment {get; set;} = null!;
        [Required]
        public string PostId {get; set;} = null!;
    }
}