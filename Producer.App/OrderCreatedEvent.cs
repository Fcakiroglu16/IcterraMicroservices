using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer.App
{
    internal class OrderCreatedEvent<T>
    {
        public Dictionary<string, string> Headers { get; set; } = new();

        public  T  Payload { get; set; }
    }
}
