using System;
using System.Threading;
using Confluent.Kafka;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Kafka
{
  public class KafkaProducer: IKafkaProducerAccessor
  {
    private readonly AutoResetEvent _closing = new AutoResetEvent(false);
    private IProducer<string, string> producer = null;
    private ProducerConfig producerConfig = null;

        public KafkaProducer(IOptions<KafkaSettings> config)
        {
            producerConfig = new ProducerConfig
            {
                BootstrapServers = config.Value.BootstrapServers
            };
        }

        // static void Main(string[] args){
        //   CreateProducer();
        //   SendMessage("testTopic", "This is a test42");
        //   Console.WriteLine("Press Ctrl+C to exit");
        //   Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
        //   _closing.WaitOne();
        // }
    public void CreateProducer() {
      var pb = new ProducerBuilder<string, string>(producerConfig);
      producer = pb.Build();
    }

    public async void SendMessage(string topic, string message) {
      var msg = new Message<string, string> {
          Key = null,
          Value = message
      };

      var delRep = await producer.ProduceAsync(topic, msg);
      var topicOffset = delRep.TopicPartitionOffset;

      Console.WriteLine($"Delivered '{delRep.Value}' to: {topicOffset}");
    }

    public void OnExit(object sender, ConsoleCancelEventArgs args)
    {
      Console.WriteLine("Exit");
      _closing.Set();
    }
    
  }
}