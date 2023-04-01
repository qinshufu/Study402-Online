using AutoMapper;
using MediatR;
using Study402Online.ContentService.Api.Controllers;
using Study402Online.ContentService.Api.Infrastructure;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.RequestHandlers;

/// <summary>
/// 添加课程命令处理器
/// </summary>
public class AddCourseHandler : IRequestHandler<AddCourseCommand, AddCourseResultModel>
{
    private readonly ContentDbContext _context;
    private readonly IMapper _mapper;

    public AddCourseHandler(ContentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddCourseResultModel> Handle(AddCourseCommand command, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<Course>(command.Model);
        _context.Add(course);
        await _context.SaveChangesAsync();

        var couseMarket = _mapper.Map<CourseMarket>(command.Model);
        couseMarket.Id = course.Id;
        _context.Add(couseMarket);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<AddCourseResultModel>(command);
        return result;
    }
}