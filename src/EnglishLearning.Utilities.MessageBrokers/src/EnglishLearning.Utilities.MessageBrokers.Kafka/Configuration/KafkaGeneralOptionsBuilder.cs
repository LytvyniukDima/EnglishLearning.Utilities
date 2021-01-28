using System;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration
{
    internal class KafkaGeneralOptionsBuilder : IKafkaGeneralOptionsBuilder
    {
        private readonly IServiceCollection _services;

        public KafkaGeneralOptionsBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public void AddConsumer(Action<IKafkaConsumerOptionsBuilder> optionsBuilderAction)
        {
            if (optionsBuilderAction == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilderAction));
            }

            _services.AddKafkaConsumer(optionsBuilderAction);
        }
    }
}
