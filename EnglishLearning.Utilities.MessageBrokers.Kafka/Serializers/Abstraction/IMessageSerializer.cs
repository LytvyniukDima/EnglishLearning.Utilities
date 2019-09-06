using Confluent.Kafka;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers.Abstraction
{
    public interface IMessageSerializer<T> : ISerializer<T>, IDeserializer<T>
    {
        byte[] Serialize(T data);
        T Deserialize(byte[] data);
    }
}
