using MassTransit;
using Shread;
using Stock.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<OrderCreatedEventConsumer>();


builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<Stock.API.Consumers.OrderCreatedEventConsumer>();

    x.UsingRabbitMq((context, config) =>
    {
        config.UseMessageRetry(x => x.Immediate(5));
        config.UseMessageRetry(x=>x.Incremental(5,TimeSpan.FromSeconds(5),TimeSpan.FromSeconds(5)));

        
        config.UseDelayedRedelivery(x => x.Intervals(TimeSpan.FromMinutes(10),TimeSpan.FromMinutes(30)));
        config.UseInMemoryOutbox(context);
        config.Host("amqps://yzdtypic:uhO311rKu17xccMp5ugen02_OS3CFnp3@fish.rmq.cloudamqp.com/yzdtypic", configure =>
        {
            //configure.Username("guest");
            //configure.Password("guest");

        });

        config.ReceiveEndpoint("stock.api.masstransit.order.created.event.queue", endpoint =>
        {

            endpoint.ConfigureConsumer<Stock.API.Consumers.OrderCreatedEventConsumer>(context);

        });

    });





});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
