﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Infrastructure.Core.Exceptions;
using InvoicingService.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace InvoicingService.Test.Domain
{
    [TestFixture]
    public class InvoicingFilterTests
    {
        [Test]
        public void ExceptionFilter_BadRequestTest()
        {
            var exceptionFilter = new ExceptionHandlerFilter();
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new BaseException("unit test")
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
            var exceptionFilter = new ExceptionHandlerFilter();
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception("unit test")
            };

            // Act
            exceptionFilter.OnException(exceptionContext);

            // Assert
            var contentResult = exceptionContext.Result as StatusCodeResult;
            Assert.AreEqual((int)System.Net.HttpStatusCode.InternalServerError, contentResult?.StatusCode);
        }
    }
}
