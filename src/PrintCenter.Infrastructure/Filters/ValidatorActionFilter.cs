using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace PrintCenter.Infrastructure.Filters
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ModelState.IsValid)
                return;

            var result = new ContentResult();
            var errors = filterContext.ModelState.ToDictionary(valuePair => valuePair.Key, valuePair => valuePair.Value.Errors.Select(x => x.ErrorMessage).ToArray());

            var content = JsonConvert.SerializeObject(new { errors });
            result.Content = content;
            result.ContentType = "application/json";

            filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
            filterContext.Result = result;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}