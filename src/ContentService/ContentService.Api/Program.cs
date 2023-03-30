using Microsoft.EntityFrameworkCore;
using Study402Online.ContentService.Api.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加数据库
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<ContentDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default"),
    builder =>
    {
        builder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
        builder.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    }));

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
