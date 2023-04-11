using Microsoft.EntityFrameworkCore;
using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using Study402Online.BuildingBlocks.LocalMessage.Storage;

namespace Study402Online.BuildingBlocks.LocalMessage;


/// <summary>
/// 消息日志查找器
/// </summary>
public class MessageLogFinder : IMessageLogFinder
{
    private readonly MessageLogDbContext _dbContext;

    public MessageLogFinder(MessageLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<MessageLog>> FindAsync(int number, CancellationToken cancellationToken)
    {
        var logsQuery = from log in _dbContext.MessageLogs
                        where log.Status == MessageLogStatus.Created
                        where log.Status == MessageLogStatus.Failed
                        orderby log.CreateTime
                        select log;

        return logsQuery.Take(number).ToListAsync(cancellationToken);
    }
}
