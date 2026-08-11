using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabate.Core.IServices;

namespace Talabate.APIs.Helper
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLifeInSecound;

        public CachedAttribute(int timeToLiveInSecound)
        {
            _timeToLifeInSecound = timeToLiveInSecound;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCachServices= context.HttpContext.RequestServices.GetRequiredService<IResponseCachServices>();
            var CachKey = GenerateCachKeyFromRequest(context.HttpContext.Request);
            var response=await responseCachServices.GetCachResponseAsync(CachKey);
            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType="application/json",
                    StatusCode=200
                };

                context.Result = result;
                return;
            }

            var excutedActionContext= await next.Invoke();
            if(excutedActionContext.Result is OkObjectResult okObjectResult&& okObjectResult.Value is not null)
            {
                await responseCachServices.CachResponseAsync(CachKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLifeInSecound));

            }



        }

        private string GenerateCachKeyFromRequest(HttpRequest request)
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            foreach (var (key,value) in request.Query)
            {
                Key.Append($"|{key}-{value}");
            }
            return Key.ToString();

        }
    }
}
