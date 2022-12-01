using System;
using System.Linq;
using Infrastructure.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvoicingService.Filters
{
    public class ExceptionHandlerFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BaseException exception)
            {
                var dictionary = new ModelStateDictionary();

                dictionary.AddModelError("Message", exception.Message);
                dictionary.Keys.Append("errors");
                context.Result = new BadRequestObjectResult(dictionary);
                context.ExceptionHandled = true;

                return;
            }
            
            if (context.Exception is Exception)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                context.ExceptionHandled = true;
            }
        }
    }
}
