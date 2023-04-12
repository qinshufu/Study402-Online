using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Study402Online.Common.Configurations;
using UserService.Api.Configurations;
using UserService.Api.Instructure;
using Winton.Extensions.Configuration.Consul;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => builder.Configuration.Bind("JWT", options));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<JwtOptions>().BindConfiguration("Token");

// 添加 MediatR
builder.Services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Program).Assembly); });

// 添加 consul 服务
builder.Services.AddConsul();
builder.Services.AddOptions<ConsulOptions>().BindConfiguration("Consul");
builder.Configuration.AddConsul("Study402Online/UserService/appsettings.json");

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(connectionString,
        options => options.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<UserDbContext>();

// 添加 Autofac 配置
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory(autofacBuilder =>
    {
        autofacBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }));

// 添加微信配置
builder.Services.AddOptions<WechatOptions>().BindConfiguration("Wechat");

// 添加 Redis
builder.Services.AddScoped<ConnectionMultiplexer>(ctx =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redis")!));

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

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