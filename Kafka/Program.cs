// See https://aka.ms/new-console-template for more information

using Kafka.PubSubQueue;
using Kafka.PubSubQueue.Models;

namespace Declaring_Method
{
    class Program
    {
        static void Main(string[] args)
        {
            Kafka.PubSubQueue.Interfaces.Queue queue = new Kafka.PubSubQueue.Interfaces.Queue();
            Topic topic1 = queue.CreateTopic("t1");
            Topic topic2 = queue.CreateTopic("t2");
            SleepingSubscriber sub1 = new SleepingSubscriber("sub1", 10000);
            SleepingSubscriber sub2 = new SleepingSubscriber("sub2", 10000);
            queue.Subscribe(sub1, topic1);
            queue.Subscribe(sub2, topic1);

            SleepingSubscriber sub3 = new SleepingSubscriber("sub3", 5000);
            queue.Subscribe(sub3, topic2);

            queue.PublishMessage(topic1, new Message("m1"));
            queue.PublishMessage(topic1, new Message("m2"));

            queue.PublishMessage(topic2, new Message("m3"));

            Thread.Sleep(15000);
            queue.PublishMessage(topic2, new Message("m4"));
            queue.PublishMessage(topic1, new Message("m5"));

            queue.ResetOffset(topic1, sub1, 0);
        }
    }
}