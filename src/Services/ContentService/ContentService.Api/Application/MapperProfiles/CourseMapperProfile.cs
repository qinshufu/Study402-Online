using AutoMapper;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application;

/// <summary>
/// Mapper 配置
/// </summary>
public class CourseMapperProfile : Profile
{
    public CourseMapperProfile()
    {
        CreateMap<AddCourseModel, Course>();
        CreateMap<AddCourseModel, CourseMarket>();
        CreateMap<AddCourseModel, CourseInformationModel>();
        CreateMap<AddTeachPlanModel, TeachPlan>();
        CreateMap<CoursePublishPre, CoursePublish>().AfterMap((src, dist) => dist.Id = 0);
    }
}