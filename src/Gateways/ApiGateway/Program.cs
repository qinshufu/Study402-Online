using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();

builder.Configuration.AddJsonFile("ocelot.json");

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();
