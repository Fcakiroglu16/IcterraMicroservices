// See https://aka.ms/new-console-template for more information

using Producer.App;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

Console.WriteLine("Producer");


var connectionFactory= new ConnectionFactory();
connectionFactory.Uri=new Uri("amqps://yzdtypic:uhO311rKu17xccMp5ugen02_OS3CFnp3@fish.rmq.cloudamqp.com/yzdtypic");




var connection= connectionFactory.CreateConnection();

var channel = connection.CreateModel();
channel.ConfirmSelect();

channel.ExchangeDeclare("topic-exchange", ExchangeType.Topic, true, false, null);





Enumerable.Range(1, 20).ToList().ForEach(x =>
{
    try
    {
        var newUserCreatedEvent = new UserCreatedEvent() { Id = x, UserName = "ahmet", Email = "ahmet@outlook.com" };

        var newUserCreatedEventAsJson = JsonSerializer.Serialize(newUserCreatedEvent);

        var newUserCreatedEventAsByteArray = Encoding.UTF8.GetBytes(newUserCreatedEventAsJson);

        var properties = channel.CreateBasicProperties();

        properties.Persistent = true;


       var headers= new Dictionary<string, Object>();

        headers.Add("eventId", Guid.NewGuid().ToString());
        properties.Headers = headers;
    ;


        channel.BasicPublish("topic-exchange", "warning.error.critical", properties, newUserCreatedEventAsByteArray);

        channel.WaitForConfirmsOrDie(TimeSpan.FromMilliseconds(500));
    }
    catch(Exception ex) 
    {
        Console.WriteLine(ex.Message);
    }
   
});


Console.WriteLine("Mesaj gönderildi.");


