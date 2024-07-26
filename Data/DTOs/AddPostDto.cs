using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.DTOs
{
    public class AddPostDto
    {
        [Required]
        public string Title {get; set;} = null!;

        [Required]
        public string Content {get; set;} = null!;
    }
}