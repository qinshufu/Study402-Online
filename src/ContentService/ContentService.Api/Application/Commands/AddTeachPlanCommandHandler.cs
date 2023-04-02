using AutoMapper;
using MediatR;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 添加课程计划命令处理器
/// </summary>
public class AddTeachPlanCommandHandler : IRequestHandler<AddTeachPlanCommand, TeachPlan>
{
    private readonly ContentDbContext _context;
    private readonly IMapper _mapper;

    public AddTeachPlanCommandHandler(ContentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TeachPlan> Handle(AddTeachPlanCommand request, CancellationToken cancellationToken)
    {
        var teachPlan = _mapper.Map<TeachPlan>(request.Model);
        await _context.TeachPlans.AddAsync(teachPlan);

        return teachPlan;
    }
}
