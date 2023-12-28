using MassTransit;
using Shread;

namespace Docker2.App.Consumers
{
    public class StockCreatedEventConsumer:IConsumer<StockCreatedEvent>
    {
        public Task Consume(ConsumeContext<StockCreatedEvent> context)
        {
            Console.WriteLine($"StockCreatedEvent geldi : Id :{context.Message.StockId}");

            return Task.CompletedTask;
        }
    }
}
