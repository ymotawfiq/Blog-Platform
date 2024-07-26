using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.Controllers
{
    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet("404")]
        public IActionResult _404_Not_Found(){
            return StatusCode(StatusCodes.Status404NotFound, StatusCodeReturn<string>
                ._404_Not_Found_("Path not found"));
        }

        [HttpGet("403")]
        public IActionResult _403_Forbidden(){
            return StatusCode(StatusCodes.Status403Forbidden, StatusCodeReturn<string>
                ._403_Forbidden_());
        }

        [HttpGet("401")]
        public IActionResult _401_Un_Authorized(){
            return StatusCode(StatusCodes.Status401Unauthorized, StatusCodeReturn<string>
                ._401_Un_Authorized_());
        }
    }
}