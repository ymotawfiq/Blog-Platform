using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.DTOs
{
    public class UpdatePostCommentDto
    {
        [Required]
        public string Id {get; set;} = null!;

        [Required]
        public string Comment {get; set;} = null!;
        
    }
}