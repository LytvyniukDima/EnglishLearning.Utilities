using System.Linq;
using System.Security.Authentication;
using EnglishLearning.Utilities.Identity.Abstractions;
using EnglishLearning.Utilities.Identity.Extensions;
using EnglishLearning.Utilities.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnglishLearning.Utilities.Identity.Filters
{
    public class EndpointAccessFilter : IActionFilter
    {
        private readonly IAuthInfoProvider _authInfoProvider;
        private readonly IJwtInfoProvider _jwtInfoProvider;
        
        public EndpointAccessFilter(IAuthInfoProvider authInfoProvider, IJwtInfoProvider jwtInfoProvider)
        {
            _authInfoProvider = authInfoProvider;
            _jwtInfoProvider = jwtInfoProvider;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Don't require check after action executed
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var endpointInfo = context.GetAuthorizeEndpointInfo();

            var authorizeRoles = _authInfoProvider.GetAuthorizeRoles(endpointInfo);
            if (authorizeRoles == null)
            {
                return;
            }

            if (!_jwtInfoProvider.IsAuthorized || !authorizeRoles.Contains(_jwtInfoProvider.Role))
            {
                throw new AuthenticationException();
            }
        }
    }
}
