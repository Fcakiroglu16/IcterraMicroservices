
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shread;
using System.Text;
using System.Text.Json;

namespace Stock.API.Services
{
    public class OrderCreatedEventConsumer : BackgroundService
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private ILogger<OrderCreatedEventConsumer> _logger;

        public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.Uri = new Uri("amqps://yzdtypic:uhO311rKu17xccMp5ugen02_OS3CFnp3@fish.rmq.cloudamqp.com/yzdtypic");

            _connection = _connectionFactory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare("stock.api.order.created.event", true, false, false, null);

            _channel.QueueBind("stock.api.order.created.event", "order.created.event.exchange", string.Empty, null);

            _consumer= new EventingBasicConsumer(_channel);

            _channel.BasicConsume("stock.api.order.created.event", false, _consumer);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Dispose();
            _connection.Dispose();
            return base.StopAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _consumer.Received += _consumer_Received;



            return Task.CompletedTask; 

        }

        private void _consumer_Received(object? sender, BasicDeliverEventArgs e)
        {

            var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(Encoding.UTF8.GetString(e.Body.ToArray()));
            _logger.LogInformation($"{orderCreatedEvent.Id} {orderCreatedEvent.OrderCode}");   
        }
    }
}
