using Kafka.PubSubQueue.Handler;
using Kafka.PubSubQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.PubSubQueue.Interfaces
{
    public class Queue
    {
        private Dictionary<string, TopicHandler> topicProcessor;

        public Queue()
        {
            topicProcessor = new Dictionary<string, TopicHandler>();
        }

        public Topic CreateTopic(string topicName)
        {
            Topic topic = new Topic(topicName, Guid.NewGuid().ToString());
            TopicHandler topicHandler = new TopicHandler(topic);
            if (!topicProcessor.ContainsKey(topicName))
            {
                topicProcessor.Add(topicName, topicHandler);
                Console.WriteLine($"Topic {topicName} created");
            }
            return topic;
        }

        public void PublishMessage(Topic topic, Message message)
        {
            topic.AddMessage(message);
            Console.WriteLine($"Message {message.Msg} added to topic: {topic.TopicName}");
            new Thread(()=>topicProcessor[topic.TopicName].Publish()).Start();
        }

        public void Subscribe(ISubscriber subscriber, Topic topic)
        {
            TopicSubscriber topicSubscriber = new TopicSubscriber(subscriber);
            topic.AddSubscriber(topicSubscriber);
            Console.WriteLine($"Subscriber {subscriber.GetId()} added to topic: {topic.TopicName}");
        }

        public void ResetOffset(Topic topic, ISubscriber subscriber, int newOffset)
        {
            foreach (TopicSubscriber topicSubscriber in topic.Subscribers)
            {
                if( topicSubscriber.Subscriber == subscriber)
                {
                    topicSubscriber.Offset = newOffset;
                    Console.WriteLine($"Subscriber {subscriber.GetId()} offset reset to {newOffset} for topic: {topic.TopicName}");
                    new Thread(()=> topicProcessor[topic.TopicName].StartSubscriberWorker(topicSubscriber)).Start();
                    break;
                }
            }
        }
    }
}
