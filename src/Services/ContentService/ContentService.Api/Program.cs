using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.Common.Configurations;
using System.Reflection;
using Autofac;
using Winton.Extensions.Configuration.Consul;
using Microsoft.Extensions.Options;
using Study402Online.Common.BackgroundServices;
using RazorLight;
using Aliyun.OSS;
using Study402Online.ContentService.Api.Application.Configurations;
using Autofac.Core;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

/// 添加 Redis 
builder.Services.AddSingleton(ctx => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("rides")!));

// 添加模板引擎
builder.Services.AddSingleton(ctx =>
{
    var engine = new RazorLightEngineBuilder()
    .UseFileSystemProject("Application/Templates")
    .UseMemoryCachingProvider()
    .Build();

    return engine;
});

/// 添加 Oss 服务
builder.Services.AddScoped(ctx =>
{
    var options = ctx.GetRequiredService<IOptions<OssOptions>>().Value;
    return new OssClient(options.Endpoint, options.AccessKey, options.AccessKeySecret);
});

// 添加 Oss 选项
builder.Services.AddOptions<OssOptions>().BindConfiguration("Oss");


/// 添加 AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 适用 AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterAssemblyTypes().AsImplementedInterfaces();
        builder.RegisterAssemblyModules(typeof(Program).Assembly);
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//添加 Consul
builder.Services.AddConsul();
builder.Services.AddOptions<ConsulOptions>().BindConfiguration("Consul");
builder.Configuration.AddConsul("Study402Online/ContentService/appsettings.json");

builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// 添加数据库
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<ContentDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default"),
    builder =>
    {
        builder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
        builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    }));

// 配置 swagger
builder.Services.AddSwaggerGenDefault();

var app = builder.Build();

app.UseConsul(consul =>
{
    var options = app.Services.GetRequiredService<IOptions<ConsulOptions>>().Value;

    consul.Agent.ServiceRegister(new Consul.AgentServiceRegistration
    {
        ID = options.ServiceId,
        Name = options.ServiceName,
        Address = options.ServiceAddress,
        Port = options.ServicePort,
        Check = new Consul.AgentServiceCheck
        {
            TTL = TimeSpan.FromSeconds(options.TTL)
        }
    });
});

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
