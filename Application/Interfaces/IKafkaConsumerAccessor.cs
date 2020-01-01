using System;

namespace Application.Interfaces
{
    public interface IKafkaConsumerAccessor
    {
        void OnExit(object sender, ConsoleCancelEventArgs args);
        void CreateConsumerAndConsume();
        
    }
}