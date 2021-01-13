using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnglishLearning.Utilities.Identity.Abstractions;
using EnglishLearning.Utilities.Identity.Extensions;
using EnglishLearning.Utilities.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EnglishLearning.Utilities.Identity.Filters
{
    public class JwtFilter : IActionFilter
    {
        private readonly IJwtInfoProvider _jwtInfoProvider;
        private readonly IJwtSecretKeyProvider _jwtSecretKeyProvider;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        
        public JwtFilter(IJwtInfoProvider jwtInfoProvider, IJwtSecretKeyProvider jwtSecretKeyProvider)
        {
            _jwtInfoProvider = jwtInfoProvider;
            _jwtSecretKeyProvider = jwtSecretKeyProvider;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var jwt = context.HttpContext.GetJwtToken();
            if (string.IsNullOrEmpty(jwt))
            {
                _jwtInfoProvider.IsAuthorized = false;
                return;
            }

            var secretKey = _jwtSecretKeyProvider.GetSecretKey();
            var encodedKey = Encoding.ASCII.GetBytes(secretKey); 
            
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(encodedKey),
            };
            
            var decodedToken = _jwtSecurityTokenHandler.ValidateToken(jwt, validationParameters, out var securityToken);
            var role = decodedToken.FindFirst(ClaimTypes.Role).Value;
            var id = decodedToken.FindFirst("Id").Value;

            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(decodedToken.Claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256Signature),
            };
            var refreshToken = _jwtSecurityTokenHandler.CreateToken(refreshTokenDescriptor);
            var refreshedJwt = _jwtSecurityTokenHandler.WriteToken(refreshToken);

            _jwtInfoProvider.IsAuthorized = true;
            _jwtInfoProvider.Jwt = refreshedJwt;
            _jwtInfoProvider.UserId = Guid.Parse(id);
            _jwtInfoProvider.Role = (AuthorizeRole)Enum.Parse(typeof(AuthorizeRole), role);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_jwtInfoProvider.IsAuthorized)
            {
                context.HttpContext.SetJwtToken(_jwtInfoProvider.Jwt);
            }
        }
    }
}
