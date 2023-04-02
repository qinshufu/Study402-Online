using AutoMapper;
using MediatR;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 添加教师命令处理器
/// </summary>
public class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, Result<CourseTeacher>>
{
    private readonly ContentDbContext _context;
    private readonly IMapper _mapper;

    public AddTeacherCommandHandler(ContentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CourseTeacher>> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = _mapper.Map<CourseTeacher>(request.Model);
        _ = _context.AddAsync(teacher);
        _ = await _context.SaveChangesAsync();

        return ResultFactory.Success(teacher);
    }
}
