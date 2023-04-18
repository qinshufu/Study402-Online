using MediatR;
using Study402Online.Common.Model;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Queries;

/// <summary>
/// 媒体文件查询
/// </summary>
public class MediaFileQuery : IRequest<Result<MediaFile>>
{
    /// <summary>
    /// 媒体文件 ID
    /// </summary>
    public int Id { get; set; }
}
