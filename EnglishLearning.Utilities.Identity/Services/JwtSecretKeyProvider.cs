using System;
using EnglishLearning.Utilities.Identity.Interfaces;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.Utilities.Identity.Services
{
    internal class JwtSecretKeyProvider : IJwtSecretKeyProvider
    {
        private const string keyOfSecretKey = "IdentityJwtSecretKey";
        
        private readonly IKeyValueRepository _keyValueRepository;

        public JwtSecretKeyProvider(IKeyValueRepository keyValueRepository)
        {
            _keyValueRepository = keyValueRepository;
        }

        public string GetSecretKey()
        {
            var secretKey = _keyValueRepository.GetStringValueByKey(keyOfSecretKey);
            if (String.IsNullOrEmpty(secretKey))
                throw new Exception("Secret key not found");
            
            return secretKey;
        }
    }
}