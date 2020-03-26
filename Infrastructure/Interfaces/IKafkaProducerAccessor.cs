using System;

namespace Infrastructure.Interfaces
{
    public interface IKafkaProducerAccessor
    {
        void SendMessage(string topic, string message);
        void OnExit(object sender, ConsoleCancelEventArgs args);
        void CreateProducer();
    }
}