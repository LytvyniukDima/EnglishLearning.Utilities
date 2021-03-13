using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Utilities.Identity.DelegationHandlers
{
    public class JwtInfoHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtInfoHeaderHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwtInfoProvider = _httpContextAccessor
                .HttpContext
                .RequestServices
                .GetRequiredService<IJwtInfoProvider>();
            
            request.Headers.Add("Authorization", $"Bearer {jwtInfoProvider.Jwt}");
            
            return base.SendAsync(request, cancellationToken);
        }
    }
}