using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.SerializerAndDeserializer;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Bookstore.BL.Services
{
    public class KafkaConsumerBGService<TKey, TValue> : BackgroundService
    {
        private readonly IOptionsMonitor<KafkaSettings> _kafkaSettings;
        private readonly IConsumer<TKey, TValue> _consumer;

        public KafkaConsumerBGService(IOptionsMonitor<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;

            var config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset?)_kafkaSettings.CurrentValue.AutoOffsetReset,
                GroupId = _kafkaSettings.CurrentValue.GroupId
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config).SetKeyDeserializer(new MsgPackDeserializer<TKey>()).SetValueDeserializer(new MsgPackDeserializer<TValue>()).Build();

            _consumer.Subscribe("test2");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var receivedMessage = _consumer.Consume();

                    Console.WriteLine($"Received msg with key: {receivedMessage.Key} value: {receivedMessage.Value}");
                }
            });

            return Task.CompletedTask;
        }
    }
}
