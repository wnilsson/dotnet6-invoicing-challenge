using System;
using System.Linq;
using Infrastructure.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvoicingService.Filters
{
    /// <summary/>
    public class ExceptionHandlerFilter : IExceptionFilter, IOrderedFilter
    {
        /// <summary/>
        public int Order => int.MaxValue - 10;

        /// <summary/>
        public void OnException(ExceptionContext context)
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
                return;
            }
        }
    }
}
