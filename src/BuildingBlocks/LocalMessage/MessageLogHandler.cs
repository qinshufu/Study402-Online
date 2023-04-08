using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using Study402Online.BuildingBlocks.LocalMessage.Storage;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息日志处理器
/// </summary>
public class MessageLogHandler : IMessageLogHandler
{
    private readonly IMessageLoader _messageLoader;
    private readonly MessageLogDbContext _dbContext;

    public MessageLogHandler(IMessageLoader messageLoader, MessageLogDbContext dbContext)
    {
        _messageLoader = messageLoader;
        _dbContext = dbContext;
    }

    /// <summary>
    /// 处理日志消息
    /// </summary>
    /// <param name="log"></param>
    /// <returns></returns>
    public async Task HandleAsync(MessageLog log, CancellationToken cancellationToken = default)
    {
        MarkLogHandling(log);

        var message = _messageLoader.LoadMessage(log);
        var messageHandler = _messageLoader.LoadMessageHandler(log) as IMessageHandler<IMessage>;

        try
        {
            await messageHandler!.HandleAsync(message, cancellationToken);
            MarkLogSuccessful(log);
        }
        catch
        {
            MarkLogFailed(log);
        }
    }

    public void MarkLogHandling(MessageLog log)
    {
        using var tran = _dbContext.Database.BeginTransaction();

        log.Status = MessageLogStatus.Handling;
        log.LastHandleTime = DateTime.Now;

        _dbContext.Update(log);
        _dbContext.SaveChanges();
        _dbContext.Database.CommitTransaction();
    }

    public void MarkLogFailed(MessageLog log)
    {
        using var tran = _dbContext.Database.BeginTransaction();
        log.Status = MessageLogStatus.Failed;
        log.RetriesNumber += 1;

        _dbContext.Update(log);
        _dbContext.SaveChanges();
        _dbContext.Database.CommitTransaction();
    }

    public void MarkLogSuccessful(MessageLog log)
    {
        using var tran = _dbContext.Database.BeginTransaction();

        log.Status = MessageLogStatus.Successful;

        _dbContext.Update(log);
        _dbContext.SaveChanges();
        _dbContext.Database.CommitTransaction();
    }
}
