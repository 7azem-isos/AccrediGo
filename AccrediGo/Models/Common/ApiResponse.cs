using System.Diagnostics;

namespace AccrediGo.Models.Common
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public ResponseState State { get; set; }
        public string Message { get; set; }

        // Developer-specific error details
        public string DeveloperMessage { get; set; }

        public static ApiResponse<T> Success(T data, string message = null)
        {
            return new ApiResponse<T> { Data = data, State = ResponseState.Success, Message = message };
        }

        public static ApiResponse<T> Error(string message, Exception ex = null)
        {
            return new ApiResponse<T>
            {
                Data = default,
                State = ResponseState.Error,
                Message = message,
#if DEBUG
                DeveloperMessage = ex?.InnerException?.ToString() ?? ex?.Message
#endif
            };
        }

        public static ApiResponse<T> NotFound(string message)
        {
            return new ApiResponse<T> { Data = default, State = ResponseState.NotFound, Message = message };
        }

        public static ApiResponse<T> Unauthorized(string message)
        {
            return new ApiResponse<T> { Data = default, State = ResponseState.Unauthorized, Message = message };
        }

        public static ApiResponse<T> Forbidden(string message)
        {
            return new ApiResponse<T> { Data = default, State = ResponseState.Forbidden, Message = message };
        }

        public static ApiResponse<T> BadRequest(string message)
        {
            return new ApiResponse<T> { Data = default, State = ResponseState.BadRequest, Message = message };
        }

        public static ApiResponse<T> ValidationError(string message)
        {
            return new ApiResponse<T> { Data = default, State = ResponseState.ValidationError, Message = message };
        }
    }
} 