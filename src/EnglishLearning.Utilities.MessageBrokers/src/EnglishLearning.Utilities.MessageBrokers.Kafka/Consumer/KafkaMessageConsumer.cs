using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Serializers.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Consumer
{
    internal class KafkaMessageConsumer<T> : IKafkaMessageConsumer
    {
        private const int RetryCount = 3;
        private const int RetryTimeout = 500;
        
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageSerializer<T> _serializer;
        
        public KafkaMessageConsumer(IServiceProvider serviceProvider, IMessageSerializer<T> serializer)
        {
            _serviceProvider = serviceProvider;
            _serializer = serializer;
        }
        
        public async Task<KafkaConsumerResultModel> ConsumeAsync(byte[] data)
        {
            T message = _serializer.Deserialize(data);
            
            var exception = default(Exception);
            
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider
                .GetServices<IKafkaMessageHandler<T>>()
                .ToList();

            if (!handlers.Any())
            {
                exception = new Exception($"Not found handler for {typeof(T)}");
            }
            
            foreach (var handler in handlers)
            {
                for (int i = 0; i < RetryCount; i++)
                {
                    try
                    {
                        await OnMessageAsync(scope.ServiceProvider, handler, message);
                        return KafkaConsumerResultModel.GetSuccessfulResultModel();
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                        Log.Error($"Retry message {typeof(T).Name}. Retry count {i + 1}. Exception {ex}");
                        await Task.Delay(RetryTimeout);
                    }   
                }
            }
            
            return KafkaConsumerResultModel.GetFailedResultModel(message, exception);
        }
        
        private async Task OnMessageAsync<THandler>(
            IServiceProvider serviceProvider,
            THandler handler,
            T message) 
            where THandler : IKafkaMessageHandler<T>
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = (IKafkaMessageHandler<T>)scope.ServiceProvider.GetRequiredService(handler.GetType());
                await service.OnMessageAsync(message);
            }
        }
    }
}
