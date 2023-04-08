using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Study402Online.BuildingBlocks.LocalMessage.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息调度服务
/// </summary>
public class MessageScheduleService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly IOptions<LocalMessageOption> _options;

    public MessageScheduleService(IServiceProvider serviceProvider, IOptions<LocalMessageOption> options)
    {
        _serviceProvider = serviceProvider;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested is false)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scheduler = scope.ServiceProvider.GetRequiredService<IMessageScheduler>();
                var logFinder = scope.ServiceProvider.GetRequiredService<IMessageLogFinder>();

                var logs = await logFinder.FindAsync(_options.Value.PrefetchesNumber, stoppingToken);

                foreach (var log in logs)
                {
                    await scheduler.ScheduleAsync(log, stoppingToken);
                }
            }
        }
    }
}
