using Microsoft.AspNetCore.Mvc.Filters;
using School.Web.Helpers;
using School.Web.Services;
using System.Linq;

namespace School.Web.Filter
{
    public class PageFilter : IAsyncPageFilter
    {
        private readonly IConfiguration _config;
        private readonly User user;
        public PageFilter(IConfiguration config)
        {
            _config = config;
            user = new User(_config);
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            try
            {
                var path = context.HttpContext.Request.Path;
                //if user is already on login page, dont redirect. This code is required to avoid many redirects
                if (path.Equals("/Login", StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }

                //An array of pages that doesn't need authorized to be allowed 
                //string[] allowedPages = new[] { "/Privacy" };
                //var found = Array.Find(allowedPages, x => x == path);
                //if (!string.IsNullOrEmpty(found))
                //{
                //    return;
                //}


                var token = context.HttpContext.Request.Cookies["token"]; //for testing purposes
                if (string.IsNullOrEmpty(token))
                {
                    context.HttpContext.Response.Redirect("/Login");
                }
                var usid = await user.GetUserIdFromToken(token);
                if (usid is null)
                {
                    context.HttpContext.Response.Redirect("/Login");
                }
                //await next.Invoke();

            }
            catch (NullReferenceException ex)
            {
                context.HttpContext.Response.Redirect("/Login");
            }

        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            try
            {
                var path = context.HttpContext.Request.Path;
                //if user is already on login page, dont redirect. This code is required to avoid many redirects
                if (path.Equals("/Login", StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }

                //An array of pages that doesn't need authorized to be allowed 
                //string[] allowedPages = new[] { "/Privacy" };
                //var found = Array.Find(allowedPages, x => x == path);
                //if (!string.IsNullOrEmpty(found))
                //{
                //    return;
                //}


                var token = context.HttpContext.Request.Cookies["token"]; //for testing purposes
                if (string.IsNullOrEmpty(token))
                {
                    context.HttpContext.Response.Redirect("/Login");
                }
                var usid = await user.GetUserIdFromToken(token);
                if (usid is null)
                {
                    context.HttpContext.Response.Redirect("/Login");
                }
            }
            catch (Exception ex)
            {
                context.HttpContext.Response.Redirect("/Login");
            }
        }
    }
}
