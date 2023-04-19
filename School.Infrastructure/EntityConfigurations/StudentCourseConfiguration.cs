using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Aggregates.StudentAggregate;

namespace School.Infrastructure.EntityConfigurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.Ignore(sc => sc.DomainEvents);
            builder.Property(s => s.CourseId).IsRequired();
            builder.Property(s => s.StudentId).IsRequired();
        }
    }
}
