using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace School.API.Helpers
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string values = "";
            if (context.ModelState.IsValid == false)
            {
                var query = from item in context.ModelState.Values.Where(x => x.Errors != null && x.Errors.Count > 0)
                            select item.Errors.Where(x => x.ErrorMessage != string.Empty);

                foreach (var items in query.FirstOrDefault())
                {
                    values = items.ErrorMessage;
                }
                context.Result = new BadRequestObjectResult(new ApiResult
                {
                    Message = values,
                    Succeeded = false
                });
            }
        }
    }
}
