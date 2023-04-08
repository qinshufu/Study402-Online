using Microsoft.AspNetCore.Razor.Language;
using RazorLight;
using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;
using System.Text;

namespace Study402Online.ContentService.Api.Application.Services;

/// <summary>
/// 模板服务
/// </summary>
public class TemplateService : ITemplateService
{
    static readonly string TemplateFolder = Path.Combine(AppContext.BaseDirectory, "Templates");

    private readonly RazorLightEngine _engine;

    public TemplateService(RazorLightEngine razorEngine)
    {
        _engine = razorEngine;
    }

    /// <summary>
    /// 渲染模板
    /// </summary>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result<Stream>> RenderAsync(string name, object model)
    {
        var previewPage = await _engine.CompileRenderStringAsync("Course", "Course.rz", model);
        var previewPageStream = new MemoryStream(Encoding.UTF8.GetBytes(previewPage));

        return ResultFactory.Success(previewPageStream as Stream);
    }
}
