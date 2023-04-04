using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Study402Online.MediaService.CodingConversionService;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Aliyun.OSS;
using Microsoft.Extensions.Options;

var builder = Host.CreateDefaultBuilder();

// 实际上执行视频转换任务可以使用开源的任务调度框架
// 这里直接自己写一个简单的命令行服务
builder.ConfigureServices(services =>
{
    var confBuilder = new ConfigurationBuilder();
    confBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    confBuilder.AddEnvironmentVariables();

    var conf = confBuilder.Build();

    services.AddSingleton<IConfiguration>(ctx => conf);

    services.AddOptions<OssOptions>().BindConfiguration("Oss");
    services.AddHostedService<VideoConversionWorker>();
    services.AddSingleton<DbConnection>(ctx => new SqlConnection(conf.GetConnectionString("default")));
    services.AddSingleton(ctx => ConnectionMultiplexer.Connect(conf.GetConnectionString("rides")!));
    services.AddSingleton(ctx =>
    {
        var opt = ctx.GetRequiredService<IOptions<OssOptions>>().Value;
        return new OssClient(opt.Endpoint, opt.AccessKey, opt.AccessKeySecret);
    });
});

var app = builder.Build();

app.Run();