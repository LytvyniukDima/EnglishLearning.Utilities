using System;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration
{
    public interface IKafkaGeneralOptionsBuilder
    {
        void AddConsumer(Action<IKafkaConsumerOptionsBuilder> optionsBuilderAction);
    }
}
