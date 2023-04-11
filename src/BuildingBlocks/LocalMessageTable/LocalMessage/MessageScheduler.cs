using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using Study402Online.BuildingBlocks.LocalMessage.Storage;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 调度器，用来调度消息处理
/// </summary>
public class MessageScheduler : IMessageScheduler
{
    private readonly LocalMessageOption _options;
    private readonly MessageLogDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly Semaphore _semaphore;

    public MessageScheduler(
        MessageLogDbContext dbContext,
        IOptions<LocalMessageOption> options,
        IServiceProvider serviceProvider)
    {
        _options = options.Value;
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
        _semaphore = new Semaphore(_options.ParallelismsNumber, _options.ParallelismsNumber);
    }

    /// <summary>
    /// 调度消息
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ScheduleAsync(MessageLog messageLog, CancellationToken cancellationToken)
    {
            var success = _semaphore.WaitOne();
            if (success is false)
                throw new InvalidOperationException("无法获取信号量");

            ThreadPool.QueueUserWorkItem(_ =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var logHandler = scope.ServiceProvider.GetRequiredService<IMessageLogHandler>();
                    logHandler.HandleAsync(messageLog, cancellationToken).ContinueWith(_ => _semaphore.Release()).Wait();
                }
            });

        return Task.CompletedTask;
    }
}
