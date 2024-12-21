using System.Net;

namespace CharityHub.Application.Base
{
    public class BaseResponseHandler
    {
        public BaseResponseHandler()
        {

        }

        public BaseResponse<T> Success<T>(T entity)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Successed = true,
                Data = entity,
                Message = "Successfully."
            };
        }

        public BaseResponse<T> BadRequest<T>(string? message = null)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Successed = false,
                Message = message == null ? "Bad Request." : message
            };
        }

        public BaseResponse<T> NotFound<T>(string? message = null)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Successed = false,
                Message = message == null ? "Not Found." : message
            };
        }

        public BaseResponse<T> Created<T>(T entity)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.Created,
                Successed = true,
                Data = entity,
                Message = "Added Successed"
            };
        }

        public BaseResponse<T> ServerError<T>(string? message = null)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Successed = false,
                Message = message == null ? "Internal Server Error." : message,
                Data = default(T)
            };
        }

        public BaseResponse<T> Unauthorized<T>(string? message = null)
        {
            return new BaseResponse<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Successed = false,
                Message = message == null ? "UnAuthorized" : message
            };
        }
    }
}
