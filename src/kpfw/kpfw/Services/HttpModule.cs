using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.Services
{
    public class HttpModule
    {
        private readonly RequestDelegate _next;

        public HttpModule(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Remove("Server");

            await _next.Invoke(context);

            // Clean up.
        }
    }

    public static class HttpModuleExtensions
    {
        public static IApplicationBuilder UseHttpModule(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpModule>();
        }
    }
}
