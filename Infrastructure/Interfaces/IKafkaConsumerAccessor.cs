using System;

namespace Infrastructure.Interfaces
{
    public interface IKafkaConsumerAccessor
    {
        void OnExit(object sender, ConsoleCancelEventArgs args);
        void CreateConsumerAndConsume();
        
    }
}