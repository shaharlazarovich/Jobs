using System;
using System.Threading;
using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Infrastructure.Kafka
{
    public class KafkaConsumer: IKafkaConsumerAccessor
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private ConsumerConfig consumerConfig = null;

        public KafkaConsumer(IOptions<KafkaSettings> config)
        {
            consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config.Value.BootstrapServers,
                GroupId = config.Value.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        // static void Main(string[] args)
        // {
        //     CreateConsumerAndConsume();
        // }

        public void CreateConsumerAndConsume()
        {

            var cb = new ConsumerBuilder<string, string>(consumerConfig);
            Console.WriteLine("Press Ctrl+C to exit");
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            using (var consumer = cb.Build())
            {
                consumer.Subscribe("first_topic");
                try
                {
                    while (!cts.IsCancellationRequested)
                    {
                        var cr = consumer.Consume(cts.Token);
                        var offset = cr.TopicPartitionOffset;
                      Console.WriteLine($"Message '{cr.Value}' at: '{offset}'.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    consumer.Close();
                }
            }
        }

        public void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            Console.WriteLine("In OnExit");
            cts.Cancel();

        }
    }
}
