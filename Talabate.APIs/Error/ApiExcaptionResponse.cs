namespace Talabate.APIs.Error
{
    public class ApiExcaptionResponse :ApiResponse
    {
        public string? Details { get; set; }
        public ApiExcaptionResponse(int statusCode ,string?message=null , string? details = null) :base(statusCode,message)
        {
            Details = details;
        }
    }
}
