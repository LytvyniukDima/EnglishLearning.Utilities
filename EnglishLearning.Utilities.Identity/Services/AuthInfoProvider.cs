using System.Collections.Generic;
using System.Linq;
using EnglishLearning.Utilities.Identity.Abstractions;
using EnglishLearning.Utilities.Identity.Extensions;
using EnglishLearning.Utilities.Identity.Interfaces;
using EnglishLearning.Utilities.Identity.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EnglishLearning.Utilities.Identity.Services
{
    internal class AuthInfoProvider: IAuthInfoProvider
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorProvider;
        private List<AuthorizeEndpointInfo> _authRoutesTemplates;
        
        public AuthInfoProvider(IActionDescriptorCollectionProvider actionDescriptorProvider)
        {
            _actionDescriptorProvider = actionDescriptorProvider;
            FillAuthRoutesTemplates();
        }

        public AuthorizeRole[] GetAuthorizeRoles(RequestEndpointInfo requestEndpointInfo)
        {
            var actionEndpointInfo = _authRoutesTemplates.SingleOrDefault(x => x.IsAuthorizeNeeded(requestEndpointInfo));
            
            return actionEndpointInfo?.Roles;
        }
        
        private void FillAuthRoutesTemplates()
        {            
            _authRoutesTemplates = _actionDescriptorProvider.ActionDescriptors.Items
                .Where(x => x.EndpointMetadata.Any(y => y is EnglishLearningAuthorizeAttribute))
                .Select(x => x.GetAuthorizeEndpointInfo())
                .ToList();
        }
    }
}
