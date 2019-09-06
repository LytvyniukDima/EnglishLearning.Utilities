using System;
using EnglishLearning.Utilities.Identity.Abstractions;

namespace EnglishLearning.Utilities.Identity.Services
{
    public class JwtInfoProvider : IJwtInfoProvider
    {
        public bool IsAuthorized { get; set; }
        public string Jwt { get; set; }
        public Guid UserId { get; set; }
        public AuthorizeRole Role { get; set; }
    }
}
