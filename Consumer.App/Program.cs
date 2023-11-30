// See https://aka.ms/new-console-template for more information
using Consumer.App;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

Console.WriteLine("Consumer");

var connectionFactory = new ConnectionFactory();
connectionFactory.Uri = new Uri("amqps://yzdtypic:uhO311rKu17xccMp5ugen02_OS3CFnp3@fish.rmq.cloudamqp.com/yzdtypic");




var connection = connectionFactory.CreateConnection();

var channel = connection.CreateModel();

channel.BasicQos(0, 5, false);

  var queue=channel.QueueDeclare(durable:true, exclusive:false,autoDelete:true);


channel.QueueBind(queue.QueueName,"topic-exchange", "#.critical", null);

var consumer = new EventingBasicConsumer(channel);

// event=> delegate => method
consumer.Received += (Object? sender, BasicDeliverEventArgs args) =>
{

	
        var messageAsByteArray = Encoding.UTF8.GetString(args.Body.ToArray());

        var userCreatedEvent = JsonSerializer.Deserialize<UserCreatedEvent>(messageAsByteArray);


    var properties = args.BasicProperties;

    if(properties.Headers.TryGetValue("eventId",out object? eventId))
    {
       
    }

        Console.WriteLine($"{userCreatedEvent.Id}- {userCreatedEvent.UserName} - {userCreatedEvent.Email}");

		channel.BasicAck(args.DeliveryTag, true);


    Thread.Sleep(3000);
    


};

channel.BasicConsume(queue.QueueName, false, consumer);

Console.ReadLine();