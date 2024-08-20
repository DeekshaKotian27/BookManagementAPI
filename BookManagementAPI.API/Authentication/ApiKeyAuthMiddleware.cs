namespace BookManagementAPI.API.Authentication
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context,IConfiguration configuration)
        {
            var api_Key= configuration.GetValue<string>("APIKey");
            var api_value= configuration.GetValue<string>("APIValue");
            if(!context.Request.Headers.TryGetValue(api_Key,out var extractedkey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key is not found");
                return;
            }
            if (!api_value.Equals(extractedkey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unathorized client");
                return;
            }
            await _next(context);
        }
    }
}
