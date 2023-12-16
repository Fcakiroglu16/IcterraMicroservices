using MassTransit;

namespace Order.API.Services
{
    public class OrderService
    {

        private readonly IBus _bus;
        private readonly IPublishEndpoint _endpoint;
        public OrderService(IBus bus, IPublishEndpoint endpoint)
        {
            _bus = bus;
            _endpoint = endpoint;
        }

        public  void   CreateOrder()
        {

            _endpoint.Publish(new Shread.OrderCreatedEvent() { Id = 10, OrderCode = "1234", ProductCount = 10 });
          //  _bus.SendOrderCreatedEvent(new Shread.OrderCreatedEvent() { Id = 10, OrderCode = "1234", ProductCount = 10 });
        }

    }
}
