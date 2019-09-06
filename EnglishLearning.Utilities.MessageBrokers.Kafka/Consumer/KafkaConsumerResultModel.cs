using System;
using EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Consumer
{
    internal class KafkaConsumerResultModel
    {
        public KafkaConsumerResultModel(bool isSuccessful, KafkaErrorMessage errorMessage = null)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
        
        public bool IsSuccessful { get; }
        public KafkaErrorMessage ErrorMessage { get; }

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
