namespace Study402Online.OrderService.Api.Application.Configurations;

/// <summary>
/// MassTransit.RabbitMQ 配置
/// </summary>
public record MassTransitOptions()
{
    /// <summary>
    /// 主机地址
    /// </summary>
    public string Host { get; init; }

    /// <summary>
    /// 虚拟主机
    /// </summary>

    public string VirtualHost { get; init; }

    /// <summary>
    /// Rabbit 用户名
    /// </summary>

    public string UserName { get; init; }

    /// <summary>
    /// RabbitMQ 密码
    /// </summary>

    public string Password { get; init; }
}
