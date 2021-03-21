using System.IO;
using System.Threading.Tasks;
using EnglishLearning.Utilities.Speech.Configuration;
using EnglishLearning.Utilities.Speech.Contracts;
using Microsoft.CognitiveServices.Speech;

namespace EnglishLearning.Utilities.Speech
{
    internal class TextToSpeechService : ITextToSpeechService
    {
        private readonly SpeechConfiguration _speechConfiguration;

        public TextToSpeechService(SpeechConfiguration speechConfiguration)
        {
            _speechConfiguration = speechConfiguration;
        }
        
        public async Task<Stream> SpeakTextAsync(string text)
        {
            var config = SpeechConfig.FromSubscription(_speechConfiguration.SubscriptionKey, _speechConfiguration.Region);
            using var synthesizer = new SpeechSynthesizer(config, null);
            
            using var result = await synthesizer.SpeakTextAsync(text);
            var memoryStream = new MemoryStream(result.AudioData);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }
    }
}