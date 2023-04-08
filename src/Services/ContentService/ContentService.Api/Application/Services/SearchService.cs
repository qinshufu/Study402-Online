using Study402Online.Common.Model;
using Study402Online.ContentService.Model.DataModel;
using System.Net.Http.Json;

namespace Study402Online.ContentService.Api.Application.Services;

/// <summary>
/// 搜索服务
/// </summary>
public class SearchService : ISearchService
{
    /// <summary>
    /// Http 客户端名称
    /// </summary>
    public const string HttpClientName = "SearchService";

    private readonly HttpClient _client;

    /// <summary>
    /// 搜索服务
    /// </summary>
    /// <param name="clientFactory"></param>
    public SearchService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(HttpClientName);
    }

    /// <summary>
    /// 添加课程文档
    /// </summary>
    /// <param name="course"></param>
    /// <returns></returns>
    public async Task<UnitResult> AddCourseDocumentAsync(Course course)
    {
        var response = await _client.PostAsJsonAsync("/api/course-search/add-document", course);
        return await response.Content.ReadFromJsonAsync<UnitResult>();
    }

    /// <summary>
    /// 搜索课程
    /// </summary>
    /// <param name="keywords"></param>
    /// <returns></returns>
    public async Task<Result<Guid>> SearchCourseAsync(string[] keywords)
    {
        var response = await _client.GetAsync("/api/course-search/search?" + string.Join("&", keywords.Select(k => $"keywords={k}")));
        return await response.Content.ReadFromJsonAsync<Result<Guid>>();
    }
}
