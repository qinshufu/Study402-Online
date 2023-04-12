using MediatR;
using Study402Online.Common.Model;

namespace UserService.Api.Application.Commands;

/// <summary>
/// 申请标识用户登录请求的 id
/// </summary>
public record WechatLoginIdentityApplyCommand : IRequest<Result<string>>;