namespace Study402Online.Common.Model;

/// <summary>
/// 用来表示操作执行结果
/// </summary>
/// <typeparam name="TResult"></typeparam>
public record struct Result<TResult>(TResult Value, object Error, bool Success);

/// <summary>
/// Result 工厂
/// </summary>
public static class ResultFactory
{
    public static Result<T> Success<T>(T value) => new Result<T>(value, default, true);

    public static Result<T> Fail<T>(object error) => new Result<T>(default, error, true);
}