using System;
using System.Linq;
using Infrastructure.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvoicingService.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BaseException exception)
            {
                var dictionary = new ModelStateDictionary();

                dictionary.AddModelError("Message", exception.Message);
                dictionary.Keys.Append("errors");
                context.Result = new BadRequestObjectResult(dictionary);
                context.ExceptionHandled = true;
            }

            if (context.Exception is Exception)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                context.ExceptionHandled = true;
            }
        }
    }
}
