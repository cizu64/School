using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.DTO;
using School.Web.Helpers;
using School.Web.Services;

namespace School.Web.Pages
{
    public class AddTodoModel : PageModel
    {
        private readonly IRestClient client;
        private readonly IConfiguration configuration;
        private readonly Todo todo;
        public string ErrMsg { get; set; }
        public string SucMsg { get; set; }

        public AddTodoModel(IRestClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
            todo = new Todo(this.client, this.configuration);
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["token"];
            var auth = await new CheckAuth(configuration).IsAuth(Request);
            if (!auth)
            {
                return RedirectToPage("./Login");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(TodoDTO model)
        {
            var token = Request.Cookies["token"];
            var auth = await new CheckAuth(configuration).IsAuth(Request);
            if (!auth)
            {
                return RedirectToPage("./Login");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            dynamic result = await todo.AddTodo(Request.Cookies["token"],model);
            if (result!=null&&result.succeeded == true)
            {
                SucMsg = result.message;
                return Page();
            }
            else
            {
                ErrMsg = result.message;
                return Page();
            }
        }
    }
}
