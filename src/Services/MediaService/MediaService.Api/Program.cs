using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Study402Online.Common.Configurations;
using System.Reflection;
using Autofac;
using Study402Online.MediaService.Api.Infrastructure;
using Aliyun.OSS;
using Microsoft.Extensions.Options;
using Study402Online.MediaService.Api.Application.Configurations;
using Winton.Extensions.Configuration.Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => builder.Configuration.Bind("JWT", options));

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

// 添加 consul 服务
builder.Services.AddConsul();
builder.Services.AddOptions<ConsulOptions>().BindConfiguration("Consul");
builder.Configuration.AddConsul("Study402Online/MediaService/appsettings.json");

builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

/// 添加 Oss 服务
builder.Services.AddScoped(ctx =>
{
    var options = ctx.GetRequiredService<IOptions<OssOptions>>().Value;
    return new OssClient(options.Endpoint, options.AccessKey, options.AccessKeySecret);
});

// 添加 Oss 选项
builder.Services.AddOptions<OssOptions>().BindConfiguration("Oss");

// 添加数据库
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<MediaServiceDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default"),
    builder =>
    {
        builder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
        builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    }));

// 配置 swagger
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
