using System;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;
using EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration
{
    public static class KafkaGeneralConfigurationExtensions
    {
        public static IServiceCollection AddKafka(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<IKafkaGeneralOptionsBuilder> optionsBuilderAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (optionsBuilderAction == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilderAction));
            }

            services.Configure<KafkaSettings>(configuration.GetSection("Kafka"));
            services.AddSingleton(typeof(IKafkaProducer<>), typeof(KafkaProducer<>));
            services.AddSingleton<IDeadLetterMessagesProducer, DeadLetterMessagesProducer>();
            
            var options = new KafkaGeneralOptionsBuilder(services);
            optionsBuilderAction(options);
            
            return services;
        }

        public static IServiceCollection AddMessageHandler<TEvent, TMessageHandler>(this IServiceCollection services)
            where TMessageHandler : class, IKafkaMessageHandler<TEvent>
        {
            services.AddTransient<IKafkaMessageHandler<TEvent>, TMessageHandler>();
            services.AddTransient<TMessageHandler>();
            
            return services;
        }
    }
}
