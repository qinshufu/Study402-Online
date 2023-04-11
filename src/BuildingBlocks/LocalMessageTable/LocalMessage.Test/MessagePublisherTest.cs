using Microsoft.EntityFrameworkCore;
using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using Study402Online.BuildingBlocks.LocalMessage.Storage;
using System.Text.Json;

namespace LocalMessage.Test;

/// <summary>
/// 消息发布测试
/// </summary>
public class MessagePublisherTest : NeedDatabaseTest
{
    private readonly MessageLogDbContext _dbcontext;

    private readonly MessagePublisher _publisher;

    public MessagePublisherTest()
    {
        _dbcontext = DbHelper.CreateDbContext();
        _publisher = new MessagePublisher(_dbcontext);
    }

    /// <summary>
    /// 测试发布
    /// </summary>
    [Fact]
    public async Task PublishTest()
    {
        var message = new TestMessage { Value = "1" };

        await _publisher.PublishAsync(message);

        var log = await _dbcontext.MessageLogs.SingleOrDefaultAsync();

        Assert.NotNull(log);
        Assert.True(DateTime.Now - log.CreateTime < TimeSpan.FromSeconds(0.5));
        Assert.Equal(JsonSerializer.Serialize(message), log.Value);
        Assert.Equal(typeof(TestMessage).AssemblyQualifiedName, log.Type);
        Assert.Equal(MessageLogStatus.Created, log.Status);
    }
}
