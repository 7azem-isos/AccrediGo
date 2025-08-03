namespace AccrediGo.Models.Common
{
    public class PaginatedResponse<T> : ApiResponse<List<T>>
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public static PaginatedResponse<T> Success(List<T> data, int totalCount, int pageNumber, int pageSize, string message = null)
        {
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            
            return new PaginatedResponse<T> 
            { 
                Data = data, 
                State = ResponseState.Success, 
                Message = message, 
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < totalPages
            };
        }

        public static new PaginatedResponse<T> Error(string message, Exception ex = null)
        {
            return new PaginatedResponse<T>
            {
                Data = default,
                State = ResponseState.Error,
                Message = message,
#if DEBUG
                DeveloperMessage = ex?.InnerException?.ToString() ?? ex?.Message
#endif
            };
        }

        public static new PaginatedResponse<T> NotFound(string message)
        {
            return new PaginatedResponse<T> { Data = default, State = ResponseState.NotFound, Message = message };
        }

        public static new PaginatedResponse<T> Unauthorized(string message)
        {
            return new PaginatedResponse<T> { Data = default, State = ResponseState.Unauthorized, Message = message };
        }

        public static new PaginatedResponse<T> Forbidden(string message)
        {
            return new PaginatedResponse<T> { Data = default, State = ResponseState.Forbidden, Message = message };
        }

        public static new PaginatedResponse<T> BadRequest(string message)
        {
            return new PaginatedResponse<T> { Data = default, State = ResponseState.BadRequest, Message = message };
        }

        public static new PaginatedResponse<T> ValidationError(string message)
        {
            return new PaginatedResponse<T> { Data = default, State = ResponseState.ValidationError, Message = message };
        }
    }
} 