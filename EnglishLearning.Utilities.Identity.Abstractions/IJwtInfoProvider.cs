using System;

namespace EnglishLearning.Utilities.Identity.Abstractions
{
    public interface IJwtInfoProvider
    {
        bool IsAuthorized { get; set; }
        string Jwt { get; set; }
        Guid UserId { get; set; }
        AuthorizeRole Role { get; set; }
    }
}
