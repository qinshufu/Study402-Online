using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.Common.Configurations;
using System.Reflection;
using Autofac;
using Winton.Extensions.Configuration.Consul;
using Microsoft.Extensions.Options;
using RazorLight;
using Aliyun.OSS;
using Study402Online.ContentService.Api.Application.Configurations;
using StackExchange.Redis;
using Study402Online.ContentService.Api.Application.Services;
using Polly;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Refit;
using Study402Online.BuildingBlocks.LocalMessage;

var builder = WebApplication.CreateBuilder(args);

// 使用本地消息表
builder.Services.AddLocalMessage();
builder.Services.AddLocalMessageHandlers(typeof(Program).Assembly);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters.ValidAudience = "api-client";
        options.TokenValidationParameters.ValidIssuer = "identity";
        options.TokenValidationParameters.RequireExpirationTime = true;
        options.TokenValidationParameters.IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-secret-123456789"));
    });

// 添加媒体服务配置
builder.Services.AddOptions<MediaServiceOptions>().BindConfiguration("MediaService");

// 添加 HttpClient (用于请求 MedaiService 服务)
builder.Services.AddHttpClient(MediaService.HttpClientName,
        (ctx, client) => { client.BaseAddress = ctx.GetRequiredService<IOptions<MediaServiceOptions>>().Value.Uri; })
    .AddTransientHttpErrorPolicy(policy =>
        policy.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromSeconds(Random.Shared.NextSingle()) * retryNumber))
    .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

/// 添加 Redis 
builder.Services.AddSingleton(ctx =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("rides")!));

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
    .AddDbContext<ContentDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("default"),
        builder =>
        {
            builder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
            builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// 配置 swagger
builder.Services.AddSwaggerGenDefault();

var app = builder.Build();

app.UseDefaultConsul();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();