using Study402Online.Common.Model;

namespace Study402Online.ContentService.Api.Application.Services;

/// <summary>
/// 模板服务接口
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// 渲染模板
    /// </summary>
    /// <param name="name"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    Task<Result<Stream>> RenderAsync(string name, object context);
}
