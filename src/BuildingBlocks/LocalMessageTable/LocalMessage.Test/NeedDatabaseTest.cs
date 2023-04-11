namespace LocalMessage.Test;

/// <summary>
/// 需要数据库参与的测试
/// </summary>
/// <remarks>
/// 当测试时异步的时候，数据库资源会成为竞态资源，所以需要对其进行同步。该类抽象了
/// 数据库锁的获取与释放，适用于 xUint
/// </remarks>
public abstract class NeedDatabaseTest : IDisposable
{
    protected NeedDatabaseTest()
    {
        DbHelper.Aquire();
    }

    public void Dispose()
    {
        DbHelper.Release();
    }
}
