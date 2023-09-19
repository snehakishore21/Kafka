using Kafka.PubSubQueue.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.PubSubQueue.Models
{
    public class TopicSubscriber
    {
        private ISubscriber subscriber;

        public ISubscriber Subscriber { get => subscriber; }

        private int offset;

        public int Offset { get => offset; set => offset = value; }

        public TopicSubscriber(ISubscriber subscriberCurr)
        {
            this.subscriber = subscriberCurr;
            offset = 0;
        }
    }
}
