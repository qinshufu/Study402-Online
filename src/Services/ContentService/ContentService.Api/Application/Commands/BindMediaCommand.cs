using MediatR;
using Study402Online.Common.Model;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 绑定媒资命令
/// </summary>
public record BindMediaCommand(
    string MediaFile,
    int TeachPlan,
    string MediaFileName) : IRequest<UnitResult>;