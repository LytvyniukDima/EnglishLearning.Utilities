using System;

namespace EnglishLearning.Utilities.Identity.Abstractions
{
    public class EnglishLearningAuthorizeAttribute : Attribute
    {
        public EnglishLearningAuthorizeAttribute()
        {
        }

        public EnglishLearningAuthorizeAttribute(params AuthorizeRole[] roles)
        {
            Roles = roles;
        }

        public AuthorizeRole[] Roles { get; set; } = new[]
        {
            AuthorizeRole.BaseCustomer,
            AuthorizeRole.Admin,
            AuthorizeRole.SubscribedCustomer,
        };
    }
}