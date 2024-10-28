﻿using Microsoft.IdentityModel.Tokens;

namespace WebShop.Middleware
{
    public class ExpiredTokenMiddleware
    {
        private readonly RequestDelegate next;

        public ExpiredTokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (SecurityTokenExpiredException)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token has expired");
                }
            }
        }
    }
}
