using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace EnglishLearning.Utilities.Identity.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetJwtToken(this HttpContext context)
        {
            var headers = context.Request.Headers;

            if (!headers.TryGetValue("Authorization", out StringValues token))
            {
                return String.Empty;
            }
            
            var splitedToken = token.ToString().Split(' ');
            if (splitedToken.Length != 2)
                throw new AuthenticationException("Incorrect Authorization header");

            var jwt = splitedToken[1];
            
            return jwt;
        }

        public static void SetJwtToken(this HttpContext context, string jwt)
        {
            var tokenHeader = $"Bearer {jwt}";
            context.Response.Headers["Authorization"] = tokenHeader;
        }
    }
}
