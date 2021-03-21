using System.IO;
using System.Threading.Tasks;

namespace EnglishLearning.Utilities.Speech.Contracts
{
    public interface ITextToSpeechService
    {
        Task<Stream> SpeakTextAsync(string text);
    }
}