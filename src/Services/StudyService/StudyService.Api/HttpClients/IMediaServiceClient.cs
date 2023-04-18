using Refit;
using Study402Online.Common.Model;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.StudyService.Api.HttpClients;

/// <summary>
/// 媒体服务客户端
/// </summary>
public interface IMediaServiceClient
{
    /// <summary>
    /// 获取媒体文件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Get("/api/media/get")]
    Task<Result<MediaFile>> GetMediaFileAsync([Query] int id);
}
