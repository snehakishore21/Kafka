using Kafka.PubSubQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.PubSubQueue.Interfaces
{
    public interface ISubscriber
    {
        public string GetId();

        public void Consume(Message message);

    }
}
