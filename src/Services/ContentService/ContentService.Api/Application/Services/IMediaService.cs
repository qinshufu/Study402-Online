using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Services;

/// <summary>
/// 媒体服务接口
/// </summary>
public interface IMediaService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="fileHash"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    Task<Result<MediaFile>> UploadFileAsync(string fileHash, Stream file);
}
