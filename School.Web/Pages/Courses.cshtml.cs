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
        private readonly User user;
        public CoursesModel(IRestClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
            course = new Course(this.client, this.configuration);
            student = new Student(this.client, this.configuration);
            user = new User(this.configuration); 
            ApiUrl = this.configuration["ApiUrl"];
        }
        [BindProperty]
        public string ApiUrl { get; set; }
        [BindProperty]
        public CourseVM Course { get; set; }
        public async Task<IActionResult> OnGetAsync(int pageSize=10, int pageIndex=0)
        {
            var token = Request.Cookies["token"];
            var auth = await new CheckAuth(configuration).IsAuth(Request);
            if (!auth)
            {
                return RedirectToPage("./Login");
            }          
            Course = await course.GetCourses(token,pageIndex, pageSize);
            ViewData["StudentCourse"] = await student.GetStudent(token);
            ViewData["token"] = token;
            return Page();
        }
    }
}
