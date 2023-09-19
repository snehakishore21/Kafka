using Kafka.PubSubQueue.Interfaces;
using Kafka.PubSubQueue.Models;

namespace Kafka.PubSubQueue
{
    public class SleepingSubscriber : ISubscriber
    {
        private string id;

        private int sleepinMs;

        public SleepingSubscriber(string id, int sleepInMs)
        {
            this.id = id;
            this.sleepinMs = sleepInMs;
        }

        public void Consume(Message message)
        {
            Console.WriteLine($"Start: Subscriber {id} consumed message {message.Msg}");
            Thread.Sleep(sleepinMs);
            Console.WriteLine($"End: Subscriber {id} consumed message {message.Msg}");
        }

        public string GetId()
        {
            return id;
        }
    }
}
