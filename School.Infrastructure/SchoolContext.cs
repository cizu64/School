using Microsoft.EntityFrameworkCore;
using School.Domain.Aggregates;
using School.Domain.Aggregates.StudentAggregate;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using School.Infrastructure.Extensions;
using School.Domain.SeedWork;

namespace School.Infrastructure
{
    public class SchoolContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }
        public SchoolContext(DbContextOptions<SchoolContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            //dispatch domain events from mediatorExtension class to their respective event handlers
            await _mediator.DispatchDomainEventsAsync(this);

            await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
