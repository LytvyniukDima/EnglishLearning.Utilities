﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration;
using EnglishLearning.Utilities.MessageBrokers.Kafka.ErrorHandling;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EnglishLearning.Utilities.MessageBrokers.Kafka.Consumer
{
    internal class BackgroundPerPartitionConsumer : BackgroundService
    {
        private static int _consumerCount = 0;
        
        private readonly KafkaSettings _configuration;
        private readonly IKafkaMessageConsumerFactory _consumerFactory;
        private readonly IReadOnlyList<string> _subscribedTopics;
        private readonly IDeadLetterMessagesProducer _deadLetterMessagesProducer;
        
        private readonly int _consumerId;
        
        public BackgroundPerPartitionConsumer(
            KafkaSettings configuration,
            IKafkaMessageConsumerFactory consumerFactory,
            IDeadLetterMessagesProducer deadLetterMessagesProducer,
            IReadOnlyList<string> subscribedTopics)
        {
            _configuration = configuration;
            _consumerFactory = consumerFactory;
            _deadLetterMessagesProducer = deadLetterMessagesProducer;
            _subscribedTopics = subscribedTopics;

            _consumerCount++;
            _consumerId = _consumerCount;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            
            var conf = new ConsumerConfig
            { 
                GroupId = _configuration.GroupId,
                BootstrapServers = _configuration.ConnectionString,
                
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            
            using (var c = new ConsumerBuilder<Ignore, byte[]>(conf).Build())
            {
                c.Subscribe(_subscribedTopics);

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var cr = c.Consume(stoppingToken);
                            
                            Log.Information($"Consuming message from {cr.Topic}. Partition {cr.Partition.Value}. ConsumerId {_consumerId}");
                            var consumer = _consumerFactory.GetMessageConsumer(cr.Topic);
                            var consumerResult = await consumer.ConsumeAsync(cr.Message.Value);
                            if (!consumerResult.IsSuccessful)
                            {
                                await _deadLetterMessagesProducer.Produce(cr.Topic, consumerResult.ErrorMessage);
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Failed consumer {ex}");
                    
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}
