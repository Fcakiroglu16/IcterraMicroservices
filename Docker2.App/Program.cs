using Docker2.App.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<StockCreatedEventConsumer>();
    

    configure.UsingRabbitMq((context, configurator) =>
    {

        configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"), "/", host =>
        {


        });


        configurator.ReceiveEndpoint("docker2.app.stock.created.event.queue", receiveContext =>
        {



            receiveContext.ConfigureConsumer<StockCreatedEventConsumer>(context);
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
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
