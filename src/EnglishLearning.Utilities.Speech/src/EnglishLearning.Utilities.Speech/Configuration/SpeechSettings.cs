using EnglishLearning.Utilities.Speech.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Utilities.Speech.Configuration
{
    public static class SpeechSettings
    {
        public static IServiceCollection AddEnglishLearningSpeech(this IServiceCollection services, IConfiguration configuration)
        {
            var speechConfiguration = new SpeechConfiguration
            {
                Region = configuration.GetValue<string>("TEXT_SPEECH_REGION"),
                SubscriptionKey = configuration.GetValue<string>("TEXT_SPEECH_TOKEN"),
            };

            services.AddSingleton(speechConfiguration);
            services.AddSingleton<ITextToSpeechService, TextToSpeechService>();
            
            return services;
        }
    }
}