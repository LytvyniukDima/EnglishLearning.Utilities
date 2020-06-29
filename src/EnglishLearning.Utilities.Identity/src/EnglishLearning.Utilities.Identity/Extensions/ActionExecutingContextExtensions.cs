using EnglishLearning.Utilities.Identity.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnglishLearning.Utilities.Identity.Extensions
{
    public static class ActionExecutingContextExtensions
    {
        public static RequestEndpointInfo GetAuthorizeEndpointInfo(this ActionExecutingContext context)
        {
            var template = context.ActionDescriptor.AttributeRouteInfo?.Template ?? "null";
            var httpMethod = context.HttpContext.Request.Method;

            return new RequestEndpointInfo
            {
                HttpMethod = httpMethod,
                RouteTemplate = template,
            };
        }
    }
}
