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
                    { new Department("Computer Science" )},
                    { new Department("Law" ) }
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
                                { new Course (dept.Id, "Data analytics" ) },
                                { new Course (dept.Id, "Software engineering" ) },
                                { new Course (dept.Id, "Artificial intelligence" ) },
                            });
                        }
                        else
                        {
                            schoolContext.Courses.AddRange(new List<Course>
                            {
                                { new Course (dept.Id,  "Corporate Law" ) }
                            });
                        }
                    }
                    await schoolContext.SaveChangesAsync();
                }
                if (!schoolContext.Students.Any())
                {
                    schoolContext.Students.Add(new Student("user@user.com", "password".Hash(), "John", "Doe", 21, "Male", new Address("Berlin", "Berlin", "Berlin", "Germany"), new List<StudentCourse>()));
                    await schoolContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var log = loggerFactory.CreateLogger<SchoolContextSeed>();
                log.LogError(ex, ex.Message);
            }
        }
    }
}
