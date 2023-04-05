namespace Study402Online.Common.Model;

/// <summary>
/// 空的执行结果
/// </summary>
/// <param name="Error"></param>
/// <param name="Success"></param>
public record struct UnitResult(object Error, bool Success);

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
    /// <summary>
    /// 创建成功的执行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Result<T> Success<T>(T value) => new Result<T>(value, default, true);

    /// <summary>
    /// 创建失败的执行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<T> Fail<T>(object error) => new Result<T>(default, error, true);

    /// <summary>
    /// 创建空的成功执行结果
    /// </summary>
    /// <returns></returns>
    public static UnitResult Success() => new UnitResult(default, false);

    /// <summary>
    /// 创建空的失败的执行结果
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static UnitResult Fail(object error) => new UnitResult(error, true);
}