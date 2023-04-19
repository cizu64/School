using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Application.CQRS.Commands;
using School.API.Application.CQRS.Queries;
using School.API.DTO;
using School.API.Helpers;
using School.API.Service;
using School.API.ViewModel;
using School.Domain.Aggregates;
using School.Domain.Aggregates.StudentAggregate;
using School.Domain.SeedWork;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService studentService;
        private readonly IRestClient client;
        private readonly IConfiguration configuration;
        private readonly IGenericRepository<Course> courseRepository;
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IStudentQueries _studentQueries;
        private readonly IMediator _mediator;
        private readonly User user;
        public StudentController(ILogger<StudentController> logger, IGenericRepository<Course> courseRepository, IGenericRepository<Student> studentRepository, IStudentService studentService, IRestClient client, IConfiguration configuration, IMediator mediator, IStudentQueries studentQueries)
        {
            _logger = logger;
            this.studentService = studentService;
            this.client = client;
            this.configuration = configuration;
            this.courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _mediator = mediator;
            _studentQueries = studentQueries;
            user = new User(this.client, this.configuration);
        }

        [HttpGet, Route("[action]")]
        public async Task<IActionResult> Get()
        {
             var studentId = await user.GetUserIdFromToken(Request.Headers["Authorization"]);
            if(studentId == null)
            {
                return BadRequest(new ApiResult
                {
                    Message = "User not found",
                    Succeeded = false
                });
            }
            var student = await _studentQueries.GetStudentById(studentId.Value, "StudentCourses"); //using cqrs query
            return Ok(new ApiResult
            {
                Message = "Retrieved successfully",
                Result = new
                {
                    student.Id,
                    student.Age,
                    student.Email,
                    student.Gender,
                    student.Firstname,
                    student.Lastname,
                    student.StudentAddress,
                    student.StudentCourses
                }
            });
        }

        [HttpGet, Route("courses/{pageSize:int}/{pageIndex:int}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Course>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetCourses(int pageSize = 10, int pageIndex = 0)
        {
            var courses = await courseRepository.GetAll(c => c.IsActive, "Department");
            var pagedCourses = courses.Skip(pageSize * pageIndex).Take(pageSize);
            int totalItem = courses.Count;
            var model = new PaginatedItemsViewModel<Course>(pageIndex, pageSize, totalItem, pagedCourses);
            return Ok(new ApiResult
            {
                Message = "Retrieved successfully",
                Result = model
            });
        }

        [HttpGet, Route("courses/hasenrolled/{courseId:int}")]

        public async Task<IActionResult> HasEnrolled(int courseId)
        {
            var studentId = await user.GetUserIdFromToken(Request.Headers["Authorization"]);
            if (studentId == null)
            {
                return BadRequest(new ApiResult
                {
                    Message = "User not found",
                    Succeeded = false
                });
            }
            var student = await _studentRepository.Get(s => s.Id == studentId);
            bool hasenrolled = student.HasEnrolled(studentId.Value, courseId);      
            return Ok(new ApiResult
            {
                Message = "Retrieved successfully",
                Result = hasenrolled
            });
        }



        [HttpPut, Route("enroll"), ValidateModel]
        public async Task<IActionResult> EnrollCourse([FromBody]EnrollCourseDTO course)
        {
            try
            {
                var studentId  = await user.GetUserIdFromToken(Request.Headers["Authorization"]);
                if (studentId == null)
                {
                    return BadRequest(new ApiResult
                    {
                        Message = "User not found",
                        Succeeded = false
                    });
                }
                //await studentService.EnrollCourse(studentId.Value,course.CourseId);
                await _mediator.Send(new EnrollCourseCommand(studentId.Value, course.CourseId));
                return Ok(new ApiResult
                {
                    Message = "Course enrolled successfully",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ApiResult
                {
                    Message = "An error occured",
                    Succeeded = false
                });
            }
        }

    }
}
