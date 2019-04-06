using EnglishLearning.Utilities.Identity.Extensions;
using EnglishLearning.Utilities.Identity.Interfaces;
using EnglishLearning.Utilities.Identity.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnglishLearning.Utilities.Identity.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly IAuthInfoProvider _authInfoProvider;
        
        public LogActionFilter(IAuthInfoProvider authInfoProvider)
        {
            _authInfoProvider = authInfoProvider;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var endpointInfo = context.GetAuthorizeEndpointInfo();

            var authorizeRoles = _authInfoProvider.GetAuthorizeRoles(endpointInfo);
            if (authorizeRoles == null)
                return;
        }
    }
}
