using Refit;
using Study402Online.StudyService.Api.HttpClients;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Options;
using Study402Online.StudyService.Api.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 配置订单服务
builder.Services.AddRefitClient<IOrderServiceClient>()
    .ConfigureHttpClient(orderClientOptions => orderClientOptions.BaseAddress = new Uri("http://127.0.0.1:5107"));

// 配置 MassTransit.RabbitMQ

builder.Services.AddMassTransit(x =>
{
    var entryAssembly = Assembly.GetEntryAssembly();

    x.AddConsumers(entryAssembly);

    x.UsingRabbitMq((context, configurator) =>
    {
        var options = context.GetRequiredService<IOptions<MassTransitOptions>>();

        configurator.Host(options.Value.Host, options.Value.VirtualHost, c =>
        {
            c.Username(options.Value.UserName);
            c.Password(options.Value.Password);
        });

        // consumer 的配置
        configurator.ReceiveEndpoint(endpointConfigurator =>
        {
            endpointConfigurator.Durable = true;
            endpointConfigurator.AutoDelete = false;
        });

        configurator.ConfigureEndpoints(context);
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