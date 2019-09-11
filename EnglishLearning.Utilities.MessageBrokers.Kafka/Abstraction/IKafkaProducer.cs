using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction
{
    public interface IKafkaProducer<T>
    {
        Task<DeliveryResult<Null, T>> Produce(string topicName, T message);
        Task<DeliveryResult<Null, T>> Produce(T message);
    }
}
