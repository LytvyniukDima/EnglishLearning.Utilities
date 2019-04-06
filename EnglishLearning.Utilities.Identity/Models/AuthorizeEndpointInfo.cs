using EnglishLearning.Utilities.Identity.Abstractions;

namespace EnglishLearning.Utilities.Identity.Models
{
    public class AuthorizeEndpointInfo
    {
        public string RouteTemplate { get; set; }
        public AuthorizeRole[] Roles { get; set; }
        public string HttpMethod { get; set; }
        
        public bool IsAuthorizeNeeded(RequestEndpointInfo requestEndpointInfo)
        {
            if (requestEndpointInfo.HttpMethod == HttpMethod && requestEndpointInfo.RouteTemplate == RouteTemplate)
                return true;

            return false;
        }
    }
}
