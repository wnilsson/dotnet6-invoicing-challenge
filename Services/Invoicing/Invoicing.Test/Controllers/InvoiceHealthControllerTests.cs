using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Invoicing.Api.Controllers;
using Invoicing.Api.Domain;
using Invoicing.Api.Domain.Models;
using Invoicing.Api.Models;
using Invoicing.Api.Models.MappingConfigs;
using Invoicing.Api.RestClients;
using Invoicing.Api.RestClients.Xero;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Invoicing.Test.Controllers
{
    [TestFixture]
    public class InvoiceHealthControllerTests
    {
        private readonly IInvoiceClientFactory _factory;
        private readonly ICompanyProviderRepository _repository;
        private readonly IConfiguration _configuration;

        public InvoiceHealthControllerTests()
        {
            // Arrange repo
            var companyProvider = new CompanyProvider { Id = 1, CompanyId = 1, ProviderId = 1, Provider = new Provider { Id = 1, ProviderCode = "XERO" }, Company = new Company { Id = 1 } };
            var mockRepo = new Mock<ICompanyProviderRepository>();
            mockRepo.Setup(x => x.SingleOrDefaultAsync(y => y.Provider, z => z.CompanyId == 1)).Returns(Task.Run(() => companyProvider));
            _repository = mockRepo.Object;
            
            // Arrange factory
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(1, DateTime.Today.AddMonths(-6)))
                .Returns(Task.Run(() => new List<Invoice>
                {
                    new() { CustomerName = "fred", InvoiceDate = DateTime.Now, OriginalAmount = 100000, OutstandingAmount = 0 },
                    new() { CustomerName = "john", InvoiceDate = DateTime.Now.AddMonths(-1), OriginalAmount = 500, OutstandingAmount = 500 }

                }));
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(XeroClient))).Returns(mockInvoiceClient.Object);
            _factory = new InvoiceClientFactory(serviceProvider.Object);

            // Arrange config
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["InvoicePeriodMonths"]).Returns("6");
            mockConfig.Setup(x => x["InvoiceHealthPeriodDays"]).Returns("90");
            _configuration = mockConfig.Object;

        }

        [Test]
        public async Task GetInvoiceHealthSummaryAsyncTest()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new InvoiceSummaryMappingProfile()); });
            // Act
            var controller = new InvoiceHealthController(_repository, _factory, mappingConfig.CreateMapper(), _configuration);
            var actionResult = await controller.GetInvoiceHealthSummaryAsync(1, 2);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var response = ((OkObjectResult)actionResult.Result)?.Value as InvoiceHealthViewModel;
            Assert.IsTrue(response?.InvoiceSummary.Count() == 2);
            Assert.IsTrue(response.IsHealthy);
        }
    }
}