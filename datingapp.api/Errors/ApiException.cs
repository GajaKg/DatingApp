
namespace datingapp.api.Errors
{
    public class ApiException(int statusCode, string message, string details)
    {
        // public ApiException(int statusCode, string message, string details)
        // {
        //     StatusCode = statusCode;
        //     Message = message;
        //     Details = details;
        // }
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public string Details { get; set; } = details;
    }
}