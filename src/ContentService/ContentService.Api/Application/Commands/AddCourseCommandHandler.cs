using AutoMapper;
using MediatR;
using Study402Online.Common.Model;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.Commands;

/// <summary>
/// 添加课程命令处理器
/// </summary>
public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, Result<CourseInformationModel>>
{
    private readonly ContentDbContext _context;
    private readonly IMapper _mapper;

    public AddCourseCommandHandler(ContentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CourseInformationModel>> Handle(AddCourseCommand command, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<Course>(command.Model);
        _context.Add(course);
        await _context.SaveChangesAsync();

        var couseMarket = _mapper.Map<CourseMarket>(command.Model);
        couseMarket.Id = course.Id;
        _context.Add(couseMarket);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<CourseInformationModel>(command);
        return ResultFactory.Success(result);
    }
}