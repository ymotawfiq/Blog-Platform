using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.Models.ResponseModel
{
    public class ApiResponse <T>
    {
        public int StatusCode {get; set;}
        public bool IsSuccess {get; set;}
        public string Message {get; set;} = null!;
        public T? Data {get; set;}        
    }
}