using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.Helpers;
using School.Web.Services;
using School.Web.ViewModel;

namespace School.Web.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly IRestClient client;
        private readonly IConfiguration configuration;
        private readonly Course course;
        private readonly Student student;
        public CoursesModel(IRestClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
            course = new Course(this.client, this.configuration);
            student = new Student(this.client, this.configuration);
            ApiUrl = this.configuration["ApiUrl"];
        }
        [BindProperty]
        public string ApiUrl { get; set; }
        [BindProperty]
        public CourseVM Course { get; set; }
        public async Task<IActionResult> OnGetAsync(int pageSize=10, int pageIndex=0)
        {
            Course = await course.GetCourses(Request.Cookies["token"],pageIndex, pageSize);
            ViewData["StudentCourse"] = await student.GetStudent(Request.Cookies["token"]);
            return Page();
        }
    }
}
