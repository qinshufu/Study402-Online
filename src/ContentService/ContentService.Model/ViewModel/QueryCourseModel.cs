using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Model.ViewModel
{
    public class QueryCourseModel
    {
        public AuditStatus? AuditStatus { get; set; }

        public string? CourseName { get; set; }

        public PublishStatus? PublishStatus { get; set; }
    }
}
