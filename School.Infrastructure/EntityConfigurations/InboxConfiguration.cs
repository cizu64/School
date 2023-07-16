using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Aggregates;

namespace School.Infrastructure.EntityConfigurations
{
    public class InboxConfiguration : IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.Ignore(b => b.DomainEvents);

            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.StudentId).IsRequired();
        }
    }
}
