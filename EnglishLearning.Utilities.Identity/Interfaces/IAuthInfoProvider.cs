using EnglishLearning.Utilities.Identity.Abstractions;
using EnglishLearning.Utilities.Identity.Models;

namespace EnglishLearning.Utilities.Identity.Interfaces
{
    public interface IAuthInfoProvider
    {
        AuthorizeRole[] GetAuthorizeRoles(RequestEndpointInfo requestEndpointInfo);
    }
}
