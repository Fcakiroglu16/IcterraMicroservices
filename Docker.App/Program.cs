using Docker.App.Models;
using Docker.App.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ProductService>();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});


builder.Services.AddHttpClient<ProductService>(o =>
{
    o.BaseAddress = new Uri(builder.Configuration.GetSection("Microservices")["ProductsBaseUrl"]!);
});


builder.Services.AddMassTransit(configure =>
{


    configure.UsingRabbitMq((context, configurator) =>
    {

        configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"), "/", host =>
        {


        });



    });



});












var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
