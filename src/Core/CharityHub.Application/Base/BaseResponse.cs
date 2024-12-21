using System.Net; 

namespace CharityHub.Application.Base
{
    public class BaseResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Successed { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
        public IReadOnlyList<string> Errors { get; set; }
        //public object meta

        public BaseResponse()
        {

        }

        public BaseResponse(T data, string message = null)
        {
            Successed = true;
            Data = data;
            Message = message;
        }
        public BaseResponse(string message)
        {
            Successed = false;
            Message = message;
        }

        public BaseResponse(string message, bool successed)
        {
            Message = message;
            Successed = successed;
        }
    }
}
