using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot().AddConsul().AddConfigStoredInConsul();

builder.Configuration.AddJsonFile("ocelot.json");

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();
