using Microsoft.Data.SqlClient;
using Polly;
using Polly.CircuitBreaker;
using Polly.Registry;
using Study402Online.Common.Model;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Services;

/// <summary>
/// 媒体服务
/// </summary>
public class MediaService : IMediaService
{
    /// <summary>
    /// 使用的 HttpClient 名称
    /// </summary>
    public const string HttpClientName = "MediaService";

    private readonly HttpClient _client;
    private readonly IPolicyRegistry<string> _policyRegistry;

    public MediaService(IHttpClientFactory clientFactroy, IPolicyRegistry<string> policyRegistry)
    {
        _client = clientFactroy.CreateClient(HttpClientName);
        _policyRegistry = policyRegistry;
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="fileHash"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<Result<MediaFile>> UploadFile(string fileHash, IFormFile file)
    {
        var stream = file.OpenReadStream();
        var form = new MultipartFormDataContent();

        form.Add(new StringContent(fileHash), nameof(fileHash));
        form.Add(new StreamContent(stream), nameof(file));

        var response = await _client.PostAsync("/api/media/upload", form);

        var circuitBreak = _policyRegistry.Get<IAsyncPolicy>(_client.BaseAddress!.ToString()) as AsyncCircuitBreakerPolicy;
        if (circuitBreak!.CircuitState == CircuitState.Open)
        {
            Console.WriteLine("上传文件失败，可能是媒体服务出错");
        }

        return await response.Content.ReadFromJsonAsync<Result<MediaFile>>();
    }
}
