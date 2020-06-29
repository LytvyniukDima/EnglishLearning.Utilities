using System.Threading.Tasks;
using Confluent.Kafka;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling
{
    public interface IDeadLetterMessagesProducer
    {
        Task<DeliveryResult<Null, string>> Produce(string topicName, KafkaErrorMessage message);
    }
}