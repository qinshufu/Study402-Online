using Microsoft.EntityFrameworkCore;
using Study402Online.DictionaryService.Api.Instructure;
using Study402Online.Common.Configurations;
using Winton.Extensions.Configuration.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer()
    .AddSqlServer<DataDictionaryContext>(builder.Configuration.GetConnectionString("default"),
    builder =>
    {
        builder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
        builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    });

// 添加 consul 服务
builder.Services.AddConsul();
builder.Services.AddOptions<ConsulOptions>().BindConfiguration("Consul");
builder.Configuration.AddConsul("Study402Online/DictionaryService/appsettings.json");

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddSwaggerGenDefault();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultConsul();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
