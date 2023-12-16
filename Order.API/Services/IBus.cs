using Shread;

namespace Order.API.Services
{
    public interface IBus
    {


        public void SendOrderCreatedEvent(OrderCreatedEvent @event);
    }
}
