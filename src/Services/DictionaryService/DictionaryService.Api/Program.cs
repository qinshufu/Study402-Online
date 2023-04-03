using Microsoft.EntityFrameworkCore;
using Study402Online.DictionaryService.Api.Instructure;
using Study402Online.Common.Configurations;

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

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(typeof(Program).Assembly));

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
