using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdGen;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Refit;
using Study402Online.OrderService.Api.Application;
using Study402Online.OrderService.Api.Application.Clients;
using Study402Online.OrderService.Api.Instructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加 MedaitR 服务
builder.Services.AddMediatR(mediatrConfiguration =>
    mediatrConfiguration.RegisterServicesFromAssembly(typeof(Program).Assembly));

// 添加 ef core
builder.Services.AddDbContext<OrderServiceDbContext>(dbBuilder =>
    dbBuilder.UseSqlServer(builder.Configuration.GetConnectionString("default")));

// 添加微信支付接口客户端
builder.Services.AddScoped<IWechatPayApiClient>(ctx =>
    RestService.For<IWechatPayApiClient>("https://api.mch.weixin.qq.com"));

// 雪花 ID 生成器
// 实际的分布是环境，机器 ID 可以通过 Redis 生成，这里就不写了
builder.Services.AddSingleton<IdGenerator>(ctx => new IdGenerator(0));

// 二维码生成器，实际上 API 可以简单封装一下
builder.Services.AddSingleton<QRCodeGenerator>();

// 需要通过 IOC 访问 HttpContext
builder.Services.AddHttpContextAccessor();

// 设置 refit
builder.Services
    .AddHttpClient<IWechatPayApiClient>(client => client.BaseAddress = new Uri("https://api.mch.weixin.qq.com"))
    .AddHttpMessageHandler<WechatPayRequestSigntureHandler>();

// autofac
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory(autofacOptions =>
        autofacOptions.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces()
            .InstancePerLifetimeScope()));

// automapper
builder.Services.AddAutoMapper(automapperOptions => automapperOptions.AddProfile(typeof(MapperProfile)));

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