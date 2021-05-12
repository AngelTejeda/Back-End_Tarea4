using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_4.Models;

namespace Tarea_4.ActionFilters
{
    public class EmployeePersonalInfo_EnsureMatchingIds : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            int id = (int)context.ActionArguments["id"];

            if (context.ActionArguments["modifiedEmployee"] is EmployeePersonalInfoDTO employee && employee.Id != id)
            {
                context.ModelState.AddModelError(nameof(employee.Id), $"{nameof(id)} and {nameof(employee.Id)} must have the same value.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
