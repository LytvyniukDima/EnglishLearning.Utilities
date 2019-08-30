using System;
using System.Collections.Generic;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration
{
    internal class KafkaConsumerOptionsBuilder: IKafkaConsumerOptionsBuilder
    {
        private int _partitionCount = 1;
        private readonly Dictionary<string, Type> _topicConsumerTypes;
        private readonly IServiceCollection _services;
        
        public KafkaConsumerOptionsBuilder(IServiceCollection services)
        {
            _topicConsumerTypes = new Dictionary<string, Type>();
            _services = services;
        }
        
        public int PartitionCount
        {
            get => _partitionCount;
            set
            {
                if (value <= 0 )
                    throw new ArgumentException($"{nameof(PartitionCount)} should be greater than 0"); 
                
                _partitionCount = value;   
            }
        }

        public Dictionary<string, Type> TopicConsumerTypes => _topicConsumerTypes;
        
        public void AddTopic<T>()
        {
            var topicName = typeof(T).FullName;
            AddTopic<T>(topicName);
        }

        public void AddTopic<T>(string topicName)
        {
            _topicConsumerTypes.Add(topicName, typeof(KafkaMessageConsumer<T>));
            
            _services.TryAddTransient<IKafkaMessageConsumer, KafkaMessageConsumer<T>>();
            _services.TryAddTransient<KafkaMessageConsumer<T>>();
        }
    }
}
