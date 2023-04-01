using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.Common.Configurations;
using System.Reflection;
using Autofac;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

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
