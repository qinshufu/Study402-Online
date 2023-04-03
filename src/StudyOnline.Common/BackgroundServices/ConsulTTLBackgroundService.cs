using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Study402Online.Common.Configurations;

namespace Study402Online.Common.BackgroundServices;

public class ConsulTTLBackgroundService : BackgroundService
{
    private readonly ConsulClient _client;
    private readonly ConsulOptions _options;

    public ConsulTTLBackgroundService(ConsulClient client, IOptions<ConsulOptions> options)
    {
        _client = client;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            await Task.Delay(TimeSpan.FromSeconds(_options.TTL));

            // 不需要结果
            _ = _client.Agent.PassTTL("service:" + _options.ServiceId, "心跳检查", stoppingToken);
        }
    }
}
