using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Aggregates;

namespace School.Infrastructure.EntityConfigurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.Ignore(t => t.DomainEvents);
            builder.Property(t => t.StudentId).IsRequired();
            builder.Property(t => t.Name).HasMaxLength(20).IsRequired();
            builder.Property(t => t.Description).HasMaxLength(200).IsRequired();
        }
    }
}
