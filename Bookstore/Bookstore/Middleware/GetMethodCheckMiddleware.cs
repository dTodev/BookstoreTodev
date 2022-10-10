using Newtonsoft.Json;
using System.Net;

namespace Bookstore.Middleware
{
    public class GetMethodCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GetMethodCheckMiddleware> _logger;

        public GetMethodCheckMiddleware(RequestDelegate next, ILogger<GetMethodCheckMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.StartsWith("GET"))
            {
                Console.WriteLine("The requested method is of type Get");
                await _next(context);
            }
        }
    }
}
