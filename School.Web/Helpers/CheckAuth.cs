using School.Web.Services;

namespace School.Web.Helpers
{
    public class CheckAuth
    {
        private readonly User user;
        private readonly IConfiguration _configuration;

        public CheckAuth(IConfiguration configuration)
        {
            _configuration = configuration;
            user = new User(_configuration);

        }
        public async Task<bool> IsAuth(HttpRequest request)
        {
            int? usid = null;
            if (!request.HttpContext.Request.Cookies.ContainsKey("token"))
            {
                usid = null;
            }
            else
            {
                usid = await user.GetUserIdFromToken(request.HttpContext.Request.Cookies["token"]);
            }

            bool auth = usid is null ? false : true;
            request.HttpContext.Response.Cookies.Append("IsAuth", auth.ToString());
            return auth;
        }
    }
}
