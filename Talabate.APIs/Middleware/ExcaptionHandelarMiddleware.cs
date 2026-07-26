using System.Text.Json;
using Talabate.APIs.Error;

namespace Talabate.APIs.Middleware
{
    public class ExcaptionHandelarMiddleware
    {
        public RequestDelegate _Next { get; }
        public ILogger<ExcaptionHandelarMiddleware> _Logger { get; }
        public IWebHostEnvironment _Env { get; }


        public ExcaptionHandelarMiddleware(RequestDelegate next ,ILogger<ExcaptionHandelarMiddleware> logger ,IWebHostEnvironment env)
        {
            _Next = next;
            _Logger = logger;
            _Env = env;
        }

  

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _Next.Invoke(httpContext);

            }
            catch (Exception ex)
            {

                _Logger.LogError(ex.Message);
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";


                var response= _Env.IsDevelopment()
                    ?new ApiExcaptionResponse(500, ex.Message, ex.StackTrace.ToString())
                    :new ApiExcaptionResponse(500);

                var options = new JsonSerializerOptions() {PropertyNamingPolicy =JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);
                
               await httpContext.Response.WriteAsync(json);
                
            }

        }
    }
}
