using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling
{
    internal sealed class DeadLetterMessagesProducer : IDeadLetterMessagesProducer, IDisposable
    {
        private readonly IProducer<Null, string> _producer;
        private readonly KafkaSettings _configuration;

        public DeadLetterMessagesProducer(IOptions<KafkaSettings> configuration)
        {
            _configuration = configuration.Value;
            var config = new ProducerConfig
            {
                BootstrapServers = _configuration.ConnectionString,
            };
            
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public Task<DeliveryResult<Null, string>> Produce(string topicName, KafkaErrorMessage message)
        {
            var serializedObject = JsonConvert.SerializeObject(message);
            return _producer.ProduceAsync($"{topicName}_error", new Message<Null, string>() { Value = serializedObject });
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
