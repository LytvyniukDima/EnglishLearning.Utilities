using System.Threading.Tasks;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction
{
    public interface IKafkaMessageHandler<T>
    {
        Task OnMessageAsync(T message);
    }
}