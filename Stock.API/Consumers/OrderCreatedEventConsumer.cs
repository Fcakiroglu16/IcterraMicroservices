using MassTransit;
using Shread;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IPublishEndpoint _endpoint;

        public OrderCreatedEventConsumer(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {

            //veritaba gittim



            //



            // publish
            // publish


            Console.Write($"{context.Message.Id}-{context.Message.OrderCode}");

            return Task.CompletedTask;
        }
    }
}
