using System.Linq;
using EnglishLearning.Utilities.Identity.Abstractions;
using EnglishLearning.Utilities.Identity.Models;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace EnglishLearning.Utilities.Identity.Extensions
{
    public static class ActionDescriptorExtensions
    {
        public static AuthorizeEndpointInfo GetAuthorizeEndpointInfo(this ActionDescriptor actionDescriptor)
        {
            var testAttribute = (EnglishLearningAuthorizeAttribute) actionDescriptor.EndpointMetadata.First(x => x is EnglishLearningAuthorizeAttribute);
            var template = actionDescriptor.AttributeRouteInfo?.Template ?? "null";
            var httpMethodAttribute = (HttpMethodMetadata) actionDescriptor.EndpointMetadata.First(x => x is HttpMethodMetadata);

            return new AuthorizeEndpointInfo
            {
                HttpMethod = httpMethodAttribute.HttpMethods.First(),
                Roles = testAttribute.Roles,
                RouteTemplate = template
            };
        }
    }
}
