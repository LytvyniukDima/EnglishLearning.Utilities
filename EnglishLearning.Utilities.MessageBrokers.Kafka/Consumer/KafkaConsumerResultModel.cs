using System;
using EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Consumer
{
    internal class KafkaConsumerResultModel
    {
        public bool IsSuccessful { get; }
        public KafkaErrorMessage ErrorMessage { get; }

        public KafkaConsumerResultModel(bool isSuccessful, KafkaErrorMessage errorMessage = null)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }

        public static KafkaConsumerResultModel GetSuccessfulResultModel()
        {
            return new KafkaConsumerResultModel(true);
        }

        public static KafkaConsumerResultModel GetFailedResultModel<T>(T message, Exception ex)
        {
            var errorMessage = KafkaErrorMessage.CreateErrorMessage(message, ex);
            return new KafkaConsumerResultModel(false, errorMessage);
        }
    }
}
