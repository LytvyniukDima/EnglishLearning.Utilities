using System;
using EnglishLearning.Utilities.Identity.Interfaces;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.Utilities.Identity.Services
{
    internal class JwtSecretKeyProvider : IJwtSecretKeyProvider
    {
        private const string KeyOfSecretKey = "IdentityJwtSecretKey";
        
        private readonly IKeyValueRepository _keyValueRepository;

        public JwtSecretKeyProvider(IKeyValueRepository keyValueRepository)
        {
            _keyValueRepository = keyValueRepository;
        }

        public string GetSecretKey()
        {
            var secretKey = _keyValueRepository.GetStringValueByKey(KeyOfSecretKey);
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ApplicationException("Secret key not found");
            }

            return secretKey;
        }
    }
}