using HomeService.Domain.Core.Entities.Configs;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HomeService.EndPoint.Api.Filters
{
    public class ApiKeyActionFilter : Attribute, IAsyncActionFilter
    {
        private readonly SiteSetting _settings;

        public ApiKeyActionFilter(SiteSetting settings)
        {
            _settings = settings;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          
            if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey) || string.IsNullOrEmpty(apiKey))
            {
                context.HttpContext.Response.StatusCode = 401; 
                await context.HttpContext.Response.WriteAsync("Access Denied: ApiKey is required", default);
                return; 
            }

          
            if (apiKey != _settings.ApiKey)
            {
                context.HttpContext.Response.StatusCode = 403; 
                await context.HttpContext.Response.WriteAsync("Access Denied: Invalid ApiKey", default);
                return; 
            }

            await next();
        }
    }
}