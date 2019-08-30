using System;
using Newtonsoft.Json;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling
{
    public class KafkaErrorMessage
    {
        public string Message { get; }
        public string Exception { get; }

        public KafkaErrorMessage(string message, string exception)
        {
            Message = message;
            Exception = exception;
        }

        public static KafkaErrorMessage CreateErrorMessage<T>(T message, Exception ex)
        {
            var serializedMessage = JsonConvert.SerializeObject(message);
            var serializedException = ex.ToString();
            
            return new KafkaErrorMessage(serializedMessage, serializedException);
        }
    }
}
