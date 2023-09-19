using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kafka.PubSubQueue.Models;

namespace Kafka.PubSubQueue.Handler
{
    internal class SubscriberWorker
    {
        private Topic topic;

        private TopicSubscriber topicSubscriber;

        public Topic Topic { get => topic;}

        public TopicSubscriber TopicSubscriber { get => topicSubscriber;}

        public SubscriberWorker(Topic topic, TopicSubscriber topicSubscriber)
        {
            this.topic = topic;
            this.topicSubscriber = topicSubscriber;
        }
        
        public void Run()
        {
               /* The lock statement is used to create a critical section of code. Only one thread can 
                * enter this section at a time, ensuring that multiple threads don't interfere with each other when accessing shared resources.*/
            lock (topicSubscriber)
            {
                while (true)
                {
                    int curOffset = topicSubscriber.Offset;
                    while (curOffset >= topic.Messages.Count)
                    {
                        //release the lock on an object and blocks the current thread until it reacquires the lock.
                        Monitor.Wait(topicSubscriber);
                    }
                    Message message = topic.Messages[curOffset];
                    topicSubscriber.Subscriber.Consume(message);

                    // We cannot just increment here since subscriber offset can be reset while it is consuming.
                    // So, after consuming we need to increase only if it was the previous one.
                    int newOffset = curOffset + 1;
                    if (topicSubscriber.Offset == curOffset)
                    {
                        topicSubscriber.Offset = newOffset;

                        // PulseAll wakes up all threads that are waiting on the object's monitor.
                        Monitor.PulseAll(topicSubscriber);
                    }
                }
            }
        }

        public void WakeUpIfNeeded()
        {
            lock (topicSubscriber)
            {
                // PulseAll wakes up all threads that are waiting on the object's monitor.
                Monitor.PulseAll(topicSubscriber);
            }
        }
    }
}
