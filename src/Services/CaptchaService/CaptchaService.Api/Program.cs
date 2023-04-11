using Hei.Captcha;
using StackExchange.Redis;
using Study402Online.Common.Configurations;
using Winton.Extensions.Configuration.Consul;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/// 添加 Redis 
builder.Services.AddSingleton(ctx => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("rides")!));

// 添加 MediatR
builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Program).Assembly));

// 添加验证码生成器
builder.Services.AddHeiCaptcha();

//添加 Consul
builder.Services.AddConsul();
builder.Services.AddOptions<ConsulOptions>().BindConfiguration("Consul");
builder.Configuration.AddConsul("Study402Online/CaptchaService/appsettings.json");

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
