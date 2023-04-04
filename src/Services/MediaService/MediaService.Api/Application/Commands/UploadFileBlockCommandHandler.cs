using Aliyun.OSS;
using MediatR;
using Microsoft.Extensions.Options;
using Study402Online.Common.Model;
using Study402Online.MediaService.Api.Application.Configurations;
using Study402Online.MediaService.Api.Infrastructure;
using Study402Online.MediaService.Model.DataModel;

namespace Study402Online.MediaService.Api.Application.Commands;

/// <summary>
/// �ϴ��ļ����������
/// </summary>
public class UploadFileBlockCommandHandler : IRequestHandler<UploadFileBlockCommand, Result<bool>>
{
    private readonly MediaServiceDbContext _context;
    private readonly OssOptions _options;
    private readonly OssClient _client;

    public UploadFileBlockCommandHandler(MediaServiceDbContext context, IOptions<OssOptions> options, OssClient client)
    {
        _context = context;
        _options = options.Value;
        _client = client;
    }

    public async Task<Result<bool>> Handle(UploadFileBlockCommand request, CancellationToken cancellationToken)
    {
        var objName = $"{request.BlockHash[0..2]}/{request.BlockHash[2..4]}/{request.BlockHash}.block";
        using var fileStream = request.FileBlock.OpenReadStream();

        _ = _client.PutObject(new(_options.BigFileBucket, objName, fileStream));

        // TODO ������ݿ�ļ�¼ֻ��Ҫ����һ��ʱ�䣬ʹ�� Redis ��ʵ������
        await _context.AddAsync(new FileBlock { BlockHash = request.BlockHash, FileHash = request.FileHash, ObjectKey = objName }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ResultFactory.Success(true);
    }
}