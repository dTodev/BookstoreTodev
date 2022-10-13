using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.BL.Interfaces;
using Bookstore.BL.SerializerAndDeserializer;
using Bookstore.Models.Models;
using Bookstore.Models.Models.Users;
using Confluent.Kafka;

namespace Bookstore.BL.Services
{
    public class KafkaProducerService<TKey, TValue>
    {
        private readonly IProducer<TKey, TValue> _producer;

        public KafkaProducerService()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<TKey, TValue>(config).SetKeySerializer(new MsgPackSerializer<TKey>()).SetValueSerializer(new MsgPackSerializer<TValue>()).Build();
        }

        public async Task ProduceMessage(TKey key, TValue value)
        {
            try
            {
                var msg = new Message<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                };

                var result = await _producer.ProduceAsync("test2", msg);

                if (result != null)
                {
                    Console.WriteLine($"Delivered: {result.Message} to {result.TopicPartitionOffset}");
                }
            }
            catch (ProduceException<int, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
