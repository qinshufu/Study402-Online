using Microsoft.EntityFrameworkCore;
using Study402Online.ContentService.Model.DataModel;

namespace Study402Online.ContentService.Api.Infrastructure
{
    public class ContentDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseCategory> Categories { get; set; }

        public DbSet<CourseMarket> CourseMarkets { get; set; }

        public DbSet<CoursePublish> CoursePublishes { get; set; }

        public DbSet<CoursePublishPre> coursePublishPres { get; set; }

        public DbSet<CourseTeacherRelation> courseTeacherRelations { get; set; }

        public DbSet<TeachPlan> TeachPlans { get; set; }

        public DbSet<TeachPlanMedia> TeachPlanMedias { get; set; }

        public ContentDbContext(DbContextOptions<ContentDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseMarket>()
                .Property(e => e.Id)
                .ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }
    }
}
