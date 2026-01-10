using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.Wrappers
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorCode { get; set; }

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(T data, string message = "Operation completed successfully")
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public static ServiceResponse<T> Ok(T data, string message = "Success")
        { 
            return new ServiceResponse<T>(data, message);
        }

        public static ServiceResponse<T> Fail(string message, List<string>? errors = null, string? errorCode = null)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors,
                ErrorCode = errorCode
            };
        }

        public static ServiceResponse<T> Fail(string message, string error)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }
}
