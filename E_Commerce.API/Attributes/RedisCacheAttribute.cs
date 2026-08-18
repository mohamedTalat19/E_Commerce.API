using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSeconds;
        public RedisCacheAttribute(int durationInSeconds)
        {
            _durationInSeconds = durationInSeconds;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
            //Get Caching Service From DI Container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            //Get For Data If Exists Return it Without Action Execution
            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            var data = await cacheService.GetDataAsync(cacheKey);
            if (!string.IsNullOrWhiteSpace(data))
            {
                context.Result = new ContentResult()
                {
                    Content = data,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // If doesn't Exist Run Action And Cache the data
            var executedContext = await next.Invoke();
            if(executedContext.Result is OkObjectResult { Value : not null} result )
            {
                await cacheService.SetDataAsync(cacheKey, result.Value, TimeSpan.FromSeconds(_durationInSeconds));
            }
        }

        //https://localhost:7169/api/products?PageNumber=1&PageSize=6

        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder(request.Path);
            if (request.Query.Any())
            {
                key.Append("?");
                foreach (var query in request.Query.OrderBy(q => q.Key))
                    key.Append(query.Key).Append('=').Append(query.Value).Append('&');
            }
            return key.ToString();
        }
    }
}
