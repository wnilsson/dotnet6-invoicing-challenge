using System;
using System.Linq;
using System.Reflection;
using Infrastructure.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WatchDog;

namespace Invoicing.Api.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BaseException baseException)
            {
                var dictionary = new ModelStateDictionary();
                dictionary.AddModelError("Message", baseException.Message);
                dictionary.Keys.Append("errors");
                context.Result = new BadRequestObjectResult(dictionary);
                context.ExceptionHandled = true;
                LogError(baseException, MethodBase.GetCurrentMethod()?.Name);
            }

            else if (context.Exception is { } exception)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                context.ExceptionHandled = true;
                LogError(exception, MethodBase.GetCurrentMethod()?.Name);
            }
        }

        private static void LogError(Exception exception, string callerName)
        {
            try
            {
                WatchLogger.LogError(exception.GetFullException(), callerName);
            }
            catch
            { 
                // Just suppress in the unlikely event of failure
            }
        }
    }
}
