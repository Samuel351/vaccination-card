using System.Net;

namespace VaccinationCard.Api.Responses
{
    public class ApiResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public int StatusCode { get; private set; }
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

        public List<string> Details { get; private set; } = [];

        public ApiResponse(bool sucess, string message, HttpStatusCode httpStatusCode) 
        {
            Success = sucess;
            Message = message;
            StatusCode = (int)httpStatusCode;
        }

        public ApiResponse(bool sucess, string message, HttpStatusCode httpStatusCode, List<string> details) : this(sucess, message, httpStatusCode)
        {
            Details = details;
        }
    }
}
