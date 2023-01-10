using System;
using System.Collections.Generic;
using Infrastructure.Core.Exceptions;
using Invoicing.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;

namespace Invoicing.Test.Filters
{
    [TestFixture]
    public class InvoicingFilterTests
    {
        private const string ExceptionMessage = "unit test";

        [Test]
        public void ExceptionFilter_BadRequestTest()
        {
            // Arrange
            var exceptionFilter = new ExceptionHandlerFilter();
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new BaseException(ExceptionMessage)
            };

            // Act
            exceptionFilter.OnException(exceptionContext);

            // Assert
            Assert.IsTrue(exceptionFilter.Order > 0);
            var contentResult = exceptionContext.Result as ObjectResult;
            Assert.AreEqual((int)System.Net.HttpStatusCode.BadRequest, contentResult?.StatusCode);
        }

        [Test]
        public void ExceptionFilter_InternalServerTest()
        {
            // Arrange
            var exceptionFilter = new ExceptionHandlerFilter();
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception(ExceptionMessage)
            };

            // Act
            exceptionFilter.OnException(exceptionContext);

            // Assert
            var contentResult = exceptionContext.Result as StatusCodeResult;
            Assert.AreEqual((int)System.Net.HttpStatusCode.InternalServerError, contentResult?.StatusCode);
        }
    }
}
