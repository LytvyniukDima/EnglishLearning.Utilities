using System.Threading.Tasks;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Consumer
{
    internal interface IKafkaMessageConsumer
    {
        Task<KafkaConsumerResultModel> ConsumeAsync(byte[] message);
    }
}
