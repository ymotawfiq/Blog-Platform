using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Data.Models.ResponseModel.GenericResponseModelReturn
{
    public static class StatusCodeReturn <T>
    {
        public static ApiResponse<T> _200_Success_(T data, string message="Success"){
            return new ApiResponse<T>{
                IsSuccess = true,
                StatusCode = 200,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> _201_Created_(T data, string message="Created"){
            return new ApiResponse<T>{
                IsSuccess = true,
                StatusCode = 201,
                Message = message,
                Data = data
            };
        }
        public static ApiResponse<T> _204_No_Content_(string message="No Content"){
            return new ApiResponse<T>{
                IsSuccess = true,
                StatusCode = 204,
                Message = message,
            };
        }
        public static ApiResponse<T> _404_Not_Found_(string message){
            return new ApiResponse<T>{
                IsSuccess = false,
                StatusCode = 404,
                Message = message,
            };
        }
        public static ApiResponse<T> _403_Forbidden_(string message="Forbidden"){
            return new ApiResponse<T>{
                IsSuccess = false,
                StatusCode = 403,
                Message = message,
            };
        } 
        public static ApiResponse<T> _401_Un_Authorized_(){
            return new ApiResponse<T>{
                IsSuccess = false,
                StatusCode = 401,
                Message = "UnAuthorized",
            };
        }     
        public static ApiResponse<T> _500_Internal_Server_Error_(string message="Internel Server Error"){
            return new ApiResponse<T>{
                IsSuccess = false,
                StatusCode = 500,
                Message = message,
            };
        }   
    }
}