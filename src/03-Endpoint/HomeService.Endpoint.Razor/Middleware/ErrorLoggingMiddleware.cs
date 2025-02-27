using Serilog;

namespace HomeService.Endpoint.Razor.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred: {ex.Message}";

                Log.Error(ex, message);

                // پاسخ بدون خطا
                context.Response.Redirect("/");
                return;
            }
        }
    }

    public static class Extensions
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();
            return app;
        }
    }
}
