using Microsoft.Extensions.Logging;
using School.Domain.Aggregates;
using School.Domain.Aggregates.StudentAggregate;
using School.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Infrastructure
{
    public class SchoolContextSeed
    {
        public static async Task SeedAsync(SchoolContext schoolContext,
          ILoggerFactory loggerFactory)
        {
            try
            {
                if (!schoolContext.Departments.Any())
                {
                    schoolContext.Departments.AddRange(new List<Department>
                {
                    { new Department { Name = "Computer Science" } },
                    { new Department { Name = "Law" } }
                });
                    await schoolContext.SaveChangesAsync();
                }
                if (!schoolContext.Courses.Any())
                {
                    foreach (var dept in schoolContext.Departments)
                    {
                        if (dept.Name == "Computer Science")
                        {
                            schoolContext.Courses.AddRange(new List<Course>
                            {
                                { new Course { DepartmentId = dept.Id, Name = "Data analytics" } },
                                { new Course { DepartmentId = dept.Id, Name = "Software engineering" } },
                                { new Course { DepartmentId = dept.Id, Name = "Artificial intelligence" } },
                            });
                        }
                        else
                        {
                            schoolContext.Courses.AddRange(new List<Course>
                            {
                                { new Course { DepartmentId = dept.Id, Name = "Corporate Law" } }
                            });
                        }
                    }
                    await schoolContext.SaveChangesAsync();
                }
                if (!schoolContext.Students.Any())
                {
                    schoolContext.Students.Add(new Student("user@user.com","password".Hash(),"John", "Doe", 21, "Male", new Address("Berlin", "Berlin", "Berlin", "Germany"), new List<StudentCourse>()));
                    await schoolContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var log = loggerFactory.CreateLogger<SchoolContextSeed>();
                log.LogError(ex,ex.Message);
            }
        }
    }
}
