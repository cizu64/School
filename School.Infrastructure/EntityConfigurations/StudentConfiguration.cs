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

            builder.OwnsOne(a => a.StudentAddress);
        }
    }
}
