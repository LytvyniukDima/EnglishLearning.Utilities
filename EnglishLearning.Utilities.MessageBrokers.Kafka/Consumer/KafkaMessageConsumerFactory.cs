using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Consumer
{
    internal class KafkaMessageConsumerFactory: IKafkaMessageConsumerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly Lazy<Dictionary<string, IKafkaMessageConsumer>> _consumerCache;

        private readonly Dictionary<string, Type> _topicConsumerTypes;

        public IReadOnlyDictionary<string, Type> TopicConsumerTypes  => _topicConsumerTypes;
        
        public KafkaMessageConsumerFactory(
            IServiceProvider serviceProvider, 
            Dictionary<string, Type> topicConsumerTypes)
        {
            _serviceProvider = serviceProvider;
            _topicConsumerTypes = topicConsumerTypes;
            _consumerCache = new Lazy<Dictionary<string, IKafkaMessageConsumer>>(GetConsumersCache);
        }

        public IKafkaMessageConsumer GetMessageConsumer(string topic)
        {
            return _consumerCache.Value[topic];
        }

        public void AddTopicConsumerTypes(IReadOnlyDictionary<string, Type> topicConsumerTypes)
        {
            foreach (var topicConsumerType in topicConsumerTypes)
            {
                if (!topicConsumerTypes.ContainsKey(topicConsumerType.Key))
                    _topicConsumerTypes.Add(topicConsumerType.Key, topicConsumerType.Value);
            }
        }

        private Dictionary<string, IKafkaMessageConsumer> GetConsumersCache()
        {
            Log.Error("In initializer");
            var consumerCache = new Dictionary<string, IKafkaMessageConsumer>();
            foreach (var topic in TopicConsumerTypes.Keys)
            {
                consumerCache[topic] = (IKafkaMessageConsumer) _serviceProvider.GetRequiredService(TopicConsumerTypes[topic]);
            }

            return consumerCache;
        }
    }
}
