using AutoMapper;
using Study402Online.ContentService.Model.DataModel;
using Study402Online.Study402Online.ContentService.Model.ViewModel;

namespace Study402Online.ContentService.Api.Application.MapperProfiles;

public class TeacherMapperProfile : Profile
{
    protected TeacherMapperProfile()
    {
        CreateMap<AddTeacherModel, CourseTeacherRelation>();
    }
}
