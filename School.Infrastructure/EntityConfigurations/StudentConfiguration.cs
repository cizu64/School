using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.EntityConfigurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Student.StudentCourses));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(s => s.StudentAddress, a =>
            {
                a.WithOwner();

                a.Property(a => a.City)
                .HasColumnName("City")
                    .IsRequired();

                a.Property(a => a.Street)
                .HasColumnName("Street")
                    .IsRequired();

                a.Property(a => a.State)
                .HasColumnName("State")
                    .HasMaxLength(60);

                a.Property(a => a.Country)
                .HasColumnName("Country")
                    .IsRequired();
            });

            builder.Navigation(x => x.StudentAddress).IsRequired();
        }
    }
}
