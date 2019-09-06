using System.Threading.Tasks;
using Confluent.Kafka;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers.Abstraction;
using Microsoft.Extensions.Options;
using Serilog;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Producer
{
    public class KafkaProducer<T>: IKafkaProducer<T>
    {
        private IProducer<Null, T> _producer;
        private string _topicName;
        private KafkaSettings _configuration;

        public KafkaProducer(IOptions<KafkaSettings> configuration, IMessageSerializer<T> serializer)
        {
            _configuration = configuration.Value;

            var config = new ProducerConfig { BootstrapServers = _configuration.ConnectionString };

            _producer = new ProducerBuilder<Null, T>(config)
                .SetValueSerializer(serializer)
                .Build();

            _topicName = typeof(T).FullName;
        }
        
        public Task<DeliveryResult<Null, T>> Produce(string topicName, T message)
        {
            Log.Information($"Send message {typeof(T).Name} to {topicName}");
            return _producer.ProduceAsync(topicName, new Message<Null, T> {  Value = message });
        }

        public Task<DeliveryResult<Null, T>> Produce(T message)
        {
            return Produce(_topicName, message);
        }
        
        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
