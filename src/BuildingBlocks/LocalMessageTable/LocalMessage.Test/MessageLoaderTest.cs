using Microsoft.Extensions.DependencyInjection;
using Moq;
using Study402Online.BuildingBlocks.LocalMessage;
using Study402Online.BuildingBlocks.LocalMessage.DataModels;
using Xunit.Abstractions;

namespace LocalMessage.Test;

class TestMessageHandler : IMessageHandler<TestMessage>
{
    public Task HandleAsync(TestMessage message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

/// <summary>
/// 消息加载器测试
/// </summary>
public class MessageLoaderTest 
{
    /// <summary>
    /// 测试加载消息
    /// </summary>
    [Fact]
    public void TestLoadMessage()
    {
        var message = new TestMessage { Value = "1" };
        var log = new MessageLog(message);
        var loader = new MessageLoader(new Mock<IServiceProvider>().Object);

        Assert.Equal(message, loader.LoadMessage(log));
    }

    /// <summary>
    /// 测试加载消息处理器
    /// </summary>
    [Fact]
    public void TestLoadMessageHandler()
    {
        var message = new TestMessage { Value = "1" };
        var log = new MessageLog(message);
        var handler = new Mock<IMessageHandler<TestMessage>>().Object;

        var serviceProvider = new ServiceCollection().AddScoped(ctx => handler).BuildServiceProvider();
        var loader = new MessageLoader(serviceProvider);

        var result = loader.LoadMessageHandler(log);

        Assert.Same(handler, result);
    }
}
