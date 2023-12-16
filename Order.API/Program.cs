using MassTransit;
using Order.API.Services;
using IBus = Order.API.Services.IBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBus, BusWithRabbitMq>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, config) =>
    {
       
        config.Host("amqps://yzdtypic:uhO311rKu17xccMp5ugen02_OS3CFnp3@fish.rmq.cloudamqp.com/yzdtypic", configure =>
        {
            //configure.Username("guest");
            //configure.Password("guest");

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
