using Moq;
using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using Study402Online.BuildingBlocks.LocalMessage.Storage;

namespace LocalMessage.Test;

/// <summary>
/// 消息日志处理器测试
/// </summary>
public class MessageLogHandlerTest : NeedDatabaseTest
{
    MessageLogDbContext DbContext { get; }

    public MessageLogHandlerTest()
    {
        DbContext = DbHelper.CreateDbContext();
    }

    /// <summary>
    /// 测试消息处理器失败
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task MessageHandlerErrorTest()
    {
        var message = new TestMessage { Value = "1" };
        var messageLog = new MessageLog(message);

        DbContext.MessageLogs.Add(messageLog);
        await DbContext.SaveChangesAsync();
        //await DbContext.Database.CommitTransactionAsync();

        var messageHandlerMock = new Mock<IMessageHandler<IMessage>>();
        messageHandlerMock.Setup(handler => handler.HandleAsync(message, default)).Throws<InvalidOperationException>();

        var messageLoaderMock = new Mock<IMessageLoader>();
        messageLoaderMock.Setup(loader => loader.LoadMessage(messageLog)).Returns(message);
        messageLoaderMock.Setup(loader => loader.LoadMessageHandler(messageLog)).Returns(messageHandlerMock.Object);

        var messageLogHandler = new MessageLogHandler(messageLoaderMock.Object, DbContext);
        await messageLogHandler.HandleAsync(messageLog);

        await DbContext.Entry(messageLog).ReloadAsync();

        Assert.Equal(MessageLogStatus.Failed, messageLog.Status);
        Assert.True(DateTime.UtcNow - messageLog.LastHandleTime < TimeSpan.FromMicroseconds(500));
        Assert.Equal(1, messageLog.RetriesNumber);
    }

    /// <summary>
    /// 当消息处理器成功
    /// </summary>
    /// <returns></returns>
    public async Task MessageHandleSuccessfulTest()
    {
        // TODO
    }

    /// <summary>
    /// 当为重试的消息时
    /// </summary>
    /// <returns></returns>
    public async Task MessageHandleRetryTest()
    {
        // TOOD
    }
}
