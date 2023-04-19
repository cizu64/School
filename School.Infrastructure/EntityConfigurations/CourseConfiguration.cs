using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Aggregates;

namespace School.Infrastructure.EntityConfigurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Ignore(b => b.DomainEvents);

            builder.Property(c => c.Name).IsRequired();
            builder.HasOne(c => c.Department).WithMany().HasForeignKey(c => c.DepartmentId);
        }
    }
}
