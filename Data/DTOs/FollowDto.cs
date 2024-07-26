using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.DTOs
{
    public class FollowDto
    {
        [Required]
        public string IdOrNameOrEmail {get; set;} = null!;
    }
}