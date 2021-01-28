using System;
using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers.Abstraction;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers
{
    public class KafkaJsonSerializer<T> : IMessageSerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (data == null)
            {
                return Array.Empty<byte>();
            }

            var serializedData = JsonSerializer.Serialize(data);
            
            return Encoding.UTF8.GetBytes(serializedData);
        }

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
            {
                return default(T);
            }

            var jsonString = Encoding.UTF8.GetString(data.ToArray());

            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public byte[] Serialize(T data)
        {
            if (data == null)
            {
                return Array.Empty<byte>();
            }

            var parsedData = JsonSerializer.Serialize(data);
            
            return Encoding.UTF8.GetBytes(parsedData);
        }

        public T Deserialize(byte[] data)
        {
            if (data == null)
            {
                return default(T);
            }

            var jsonString = Encoding.UTF8.GetString(data); 
            
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}