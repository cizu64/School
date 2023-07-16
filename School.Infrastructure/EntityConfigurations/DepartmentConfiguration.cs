using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Aggregates;

namespace School.Infrastructure.EntityConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Ignore(d => d.DomainEvents);

            builder.Property(c => c.Name).IsRequired();
        }
    }
}
