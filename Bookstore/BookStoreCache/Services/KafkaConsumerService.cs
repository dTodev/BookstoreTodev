using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.SerializerAndDeserializer;
using Bookstore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Bookstore.Cache.Services
{
    public class KafkaConsumerService<TKey, TValue> : IHostedService
    {
        private readonly IOptionsMonitor<KafkaSettings> _kafkaSettings;
        private readonly IConsumer<TKey, TValue> _consumer;
        public readonly Dictionary<TKey, TValue> _receivedMessagesCollection;

        public KafkaConsumerService(IOptionsMonitor<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;
            _receivedMessagesCollection = new Dictionary<TKey, TValue>();

            var config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset?)_kafkaSettings.CurrentValue.AutoOffsetReset,
                GroupId = _kafkaSettings.CurrentValue.GroupId
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config).SetKeyDeserializer(new MsgPackDeserializer<TKey>()).SetValueDeserializer(new MsgPackDeserializer<TValue>()).Build();

            _consumer.Subscribe($"{typeof(TValue).Name}.Cache");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var receivedMessage = _consumer.Consume();

                    if (_receivedMessagesCollection.ContainsKey(receivedMessage.Key))
                    {
                        continue;
                    }

                    _receivedMessagesCollection.Add(receivedMessage.Message.Key, receivedMessage.Message.Value);

                    Console.WriteLine($"Received msg with key: {receivedMessage.Key} value: {receivedMessage.Message.Value}");
                }
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

