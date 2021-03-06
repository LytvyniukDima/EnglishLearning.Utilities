﻿using System;
using System.Text.Json;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling
{
    public class KafkaErrorMessage
    {
        public KafkaErrorMessage(string message, string exception)
        {
            Message = message;
            Exception = exception;
        }
        
        public string Message { get; }
        public string Exception { get; }

        public static KafkaErrorMessage CreateErrorMessage<T>(T message, Exception ex)
        {
            var serializedMessage = JsonSerializer.Serialize(message);
            var serializedException = ex.ToString();
            
            return new KafkaErrorMessage(serializedMessage, serializedException);
        }
    }
}
