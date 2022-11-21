using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.DTO;
using School.Web.Helpers;
using School.Web.Services;
using School.Web.ViewModel;

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
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(TodoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            model.StudentId = int.Parse(Request.Cookies["studentId"]);
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
