using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.PubSubQueue.Models
{
    public class Message
    {
        private string msg;

        public string Msg { get => msg; }

        public Message(string msg)
        {
            this.msg = msg;
        }
    }
}
