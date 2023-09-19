using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.PubSubQueue.Models
{
    public class Topic
    {
        private string topicName;
        private string topicId;

        public string TopicName { get => topicName; }
        public string TopicId { get => topicId; }

        private List<Message> messages;

        private List<TopicSubscriber> subscribers;

        public List<Message> Messages { get => messages; }

        public List<TopicSubscriber> Subscribers { get => subscribers; }

        public Topic(string topicName, string topicId)
        {
            this.topicName = topicName;
            this.topicId = topicId;
            messages = new List<Message>();
            subscribers = new List<TopicSubscriber>();
        }

        public void AddMessage(Message message)
        {
           messages.Add(message);
        }

        public void AddSubscriber(TopicSubscriber subscriber)
        {
           subscribers.Add(subscriber);
        }
    }
}
