namespace Talabate.APIs.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Massage { get; set; }

        public ApiResponse(int statusCode ,string? massage=null)
        {
            StatusCode = statusCode;
            Massage = massage?? GetDefultMassageFromStatusCode(statusCode);
        }

        private string? GetDefultMassageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request was made",
                404 => "The resource you are looking for was not found",
                500 => "An internal server error occurred",
                _ => null
            };
        }
    }
}
