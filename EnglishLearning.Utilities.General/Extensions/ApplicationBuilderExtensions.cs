using EnglishLearning.Utilities.General.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EnglishLearning.Utilities.General.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEnglishLearningExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<EnglishLearningExceptionMiddleware>();
        }
    }
}
