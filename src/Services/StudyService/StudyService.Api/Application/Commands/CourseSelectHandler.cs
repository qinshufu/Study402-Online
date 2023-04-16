using MediatR;
using Study402Online.Common.Model;
using Study402Online.OrderService.Api.Application.Commands;
using Study402Online.OrderService.Model.DateModels;
using Study402Online.OrderService.Model.ViewModels;
using Study402Online.StudyService.Api.HttpClients;
using Study402Online.StudyService.Api.Instructure;
using Study402Online.StudyService.Api.Models.DataModels;

namespace Study402Online.StudyService.Api.Application.Commands;

/// <summary>
/// 课程选择命令处理器
/// </summary>
public class CourseSelectHandler : IRequestHandler<CourseSelectCommand, Result<int>>
{
    private readonly StudyDbContext _dbContext;

    private readonly ICourseServiceClient _courseServiceClient;

    private readonly IOrderServiceClient _orderServiceClient;

    private readonly HttpContext _httpCotnext;

    public CourseSelectHandler(
        StudyDbContext dbContext,
        ICourseServiceClient courseServiceClient,
        IOrderServiceClient orderServiceClient,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _courseServiceClient = courseServiceClient;
        _orderServiceClient = orderServiceClient;
        _httpCotnext = httpContextAccessor.HttpContext!;
    }

    public async Task<Result<int>> Handle(CourseSelectCommand request, CancellationToken cancellationToken)
    {
        var requestResult = await _courseServiceClient.GetCourseInfoAsync(request.Course);

        if (requestResult.Success is false)
            return ResultFactory.Fail<int>("获取课程信息失败（调用课程服务）");

        var course = requestResult.Value;

        // TODO 实际上还需要判断课程是否已经上架

        if (course.Price == 0)
            return ResultFactory.Fail<int>("免费课程可以直接加入课程表");

        var selectionRecord = new CourseSelectionRecord()
        {
            IsPaidCourse = true,
            CompanyId = course.CompanyId,
            CourseId = course.Id,
            UserId = 1, // TODO: 用户 ID 应该从 token 获取
            CreateTime = DateTime.Now,
            ExpirationTime = DateTime.Now.AddDays(1),
            ValidDays = 365,
            CourseName = course.Name,
            UserName = "test",
            Status = CourseSelectionStatus.Unpaid
        };

        _dbContext.Add(selectionRecord);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var order = new OrderCreateCommand()
        {
            ExternalBusinessId = selectionRecord.Id.ToString(),
            ClientIp = _httpCotnext.Connection.RemoteIpAddress!.ToString(),
            OrderItems = new[]
            {
                new OrderItemModel()
                    { GoodsId = course.Id, Price = course.Price, GoodsName = course.Name, GoodsType = 1 }
            },
            OrderName = "购买课程",
            OrderType = OrderType.Course,
            TotalPrice = course.Price
        };

        var createOrderResult = await _orderServiceClient.CreateOrderAsync(order);
        if (createOrderResult.Success is false)
            return ResultFactory.Fail<int>("创建订单失败");

        return ResultFactory.Success(createOrderResult.Value);
    }
}