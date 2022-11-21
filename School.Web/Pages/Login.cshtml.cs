using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.Helpers;
using School.Web.Services;
using School.Web.ViewModel;

namespace School.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IRestClient client;
        private readonly IConfiguration configuration;
        private readonly Student student;
        public string ErrMsg { get; set; }

        public LoginModel(IRestClient client, IConfiguration configuration)
        {
            this.client = client;
            this.configuration = configuration;
            student = new Student(this.client, this.configuration);
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string token = await student.Login(model.Email, model.Password);
            if (!string.IsNullOrEmpty(token))
            {
                Response.Cookies.Append("token", $"Bearer {token}", new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                });
                return RedirectToPage("./Index");
            }
            else
            {
                ErrMsg = "Cannot autheticate student";
                return Page();
            }
        }
    }
}
