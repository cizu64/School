using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.Helpers;
using School.Web.Services;
using School.Web.ViewModel;

namespace School.Web.Pages
{
    public class TodoModel : PageModel
    {
        private readonly IRestClient client;
        private readonly IConfiguration configuration;
        private readonly Todo todo;
        public TodoModel(IRestClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
            todo = new Todo(this.client, this.configuration);
        }
      
        [BindProperty]
        public TodoVM Todo { get; set; }
        public async Task<IActionResult> OnGetAsync(int pageSize = 10, int pageIndex = 0)
        {
            var token = Request.Cookies["token"];
            var auth = await new CheckAuth(configuration).IsAuth(Request);
            if (!auth)
            {
                return RedirectToPage("./Login");
            }
            Todo = await todo.GetTodos(token, pageIndex, pageSize);
            return Page();
        }
       
    }
}
