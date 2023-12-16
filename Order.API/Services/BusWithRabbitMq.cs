using RabbitMQ.Client;
using Shread;
using System.Text;
using System.Text.Json;

namespace Order.API.Services
{
    public class BusWithRabbitMq : IBus
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;


        public BusWithRabbitMq()
        {
            
            _connectionFactory=new ConnectionFactory();
            _connectionFactory.Uri = new Uri("amqps://yzdtypic:uhO311rKu17xccMp5ugen02_OS3CFnp3@fish.rmq.cloudamqp.com/yzdtypic");

            _connection = _connectionFactory.CreateConnection();

            _channel= _connection.CreateModel();

            _channel.ExchangeDeclare("order.created.event.exchange", "fanout", true, false, null);

          

        }






        public void SendOrderCreatedEvent(OrderCreatedEvent @event)
        {

            _channel.BasicPublish("order.created.event.exchange", string.Empty, null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event)));

           

        }
    }
}
