using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using Study402Online.BuildingBlocks.LocalMessage.Storage;
using System.Text.Json;

namespace Study402Online.BuildingBlocks.LocalMessage;

/// <summary>
/// 消息发布器
/// </summary>
public class MessagePublisher : IMessagePublisher
{
    private readonly MessageLogDbContext _dbContext;

    public MessagePublisher(MessageLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage
    {
        var log = new MessageLog(message);

        await _dbContext.AddAsync(log);
        await _dbContext.SaveChangesAsync();
    }
}
