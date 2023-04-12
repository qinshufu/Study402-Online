using Microsoft.EntityFrameworkCore;
using Study402Online.StudyService.Api.Models.DataModels;

namespace Study402Online.StudyService.Api.Instructure;

/// <summary>
/// 数据库上下文
/// </summary>
public class StudyDbContext : DbContext
{
    /// <summary>
    /// 课程表
    /// </summary>
    public DbSet<ClassSchedule> ClassSchedules { get; set; }

    /// <summary>
    /// 选课记录
    /// </summary>
    public DbSet<CourseSelectionRecord> CourseSelectionRecords { get; set; }

    public StudyDbContext(DbContextOptions<StudyDbContext> options) : base(options) { }
}
