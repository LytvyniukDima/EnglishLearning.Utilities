using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace EnglishLearning.Utilities.General.Middlewares
{
    public class EnglishLearningExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        
        public EnglishLearningExceptionMiddleware(RequestDelegate next)
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
                if (ex is AuthenticationException || ex is SecurityTokenException)
                {
                    Log.Error($"Authorization exception: {ex}");      
                    
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "@text/plain";
                    await context.Response.WriteAsync(ex.Message);
                }
                else
                {
                    Log.Error($"Internal server error: {ex}");      
                    
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "@text/plain";
                    await context.Response.WriteAsync(ex.Message);
                }
            }
        }
    }
}
