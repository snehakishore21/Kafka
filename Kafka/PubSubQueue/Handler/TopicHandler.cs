using Kafka.PubSubQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.PubSubQueue.Handler
{
    internal class TopicHandler
    {
        private Topic topic;

        private Dictionary<string, SubscriberWorker> subscriberWorkers;

        public TopicHandler(Topic topic)
        {
            this.topic = topic;
            this.subscriberWorkers = new Dictionary<string, SubscriberWorker>();
        }

        public void Publish()
        {
            foreach (TopicSubscriber topicSubscriber in topic.Subscribers)
            {
                StartSubscriberWorker(topicSubscriber);
            }
        }

        public void StartSubscriberWorker(TopicSubscriber topicSubscriber)
        {
            string id = topicSubscriber.Subscriber.GetId();
            if(!subscriberWorkers.ContainsKey(id))
            {
                SubscriberWorker subscriberWorker1 = new SubscriberWorker(topic, topicSubscriber);
                subscriberWorkers.Add(id, subscriberWorker1);
                new Thread(subscriberWorker1.Run).Start();
            }
            SubscriberWorker subscriberWorker = subscriberWorkers[id];
            subscriberWorker.WakeUpIfNeeded();
        }
    }
}
