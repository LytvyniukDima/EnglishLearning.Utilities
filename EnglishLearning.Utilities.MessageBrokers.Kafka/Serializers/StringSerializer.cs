using System;
using System.Text;
using Confluent.Kafka;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers.Abstraction;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers
{
    public class StringSerializer : IMessageSerializer<string>
    {
        public byte[] Serialize(string data, SerializationContext context)
        {
            if (data == null)
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(data);
        }

        public string Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
            {
                return null;
            }

            return Encoding.UTF8.GetString(data.ToArray());
        }

        public byte[] Serialize(string data)
        {
            if (data == null)
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(data);
        }

        public string Deserialize(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(data);
        }
    }
}
