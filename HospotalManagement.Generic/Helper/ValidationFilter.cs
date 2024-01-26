using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospotalManagement.Generic.Helper
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var mesage = string.Join(Environment.NewLine, context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
                context.Result = new BadRequestObjectResult(Newtonsoft.Json.JsonConvert.SerializeObject(new { status = false, message = mesage }));
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
