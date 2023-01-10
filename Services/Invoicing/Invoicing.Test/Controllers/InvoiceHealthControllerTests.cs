using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Core.Exceptions;
using Invoicing.Api.Controllers;
using Invoicing.Api.Domain;
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
            var mockRepo = new Mock<ICompanyProviderRepository>();
            mockRepo.Setup(x => x.SingleOrDefaultAsync(y => y.Provider, z => z.CompanyId == 1))
                .Returns(Task.Run(TestHelper.GetCompanyProvider));
            _repository = mockRepo.Object;
            
            // Arrange factory
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(1, TestHelper.FromDate))
                .Returns(Task.Run(TestHelper.GetInvoices));
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(XeroClient))).Returns(mockInvoiceClient.Object);
            _factory = new InvoiceClientFactory(serviceProvider.Object);

            // Arrange config
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["InvoicePeriodMonths"]).Returns(TestHelper.InvoicePeriodMonths.ToString);
            mockConfig.Setup(x => x["InvoiceHealthPeriodDays"]).Returns(TestHelper.HealthPeriodDays.ToString);
            _configuration = mockConfig.Object;
        }

        [Test]
        public async Task GetInvoiceHealthSummaryAsyncTest()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new InvoiceSummaryMappingProfile()); });
            var controller = new InvoiceHealthController(_repository, _factory, mappingConfig.CreateMapper(), _configuration);
            // Act
            var actionResult = await controller.GetInvoiceHealthSummaryAsync(1, 2);
            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var response = ((OkObjectResult)actionResult.Result)?.Value as InvoiceHealthViewModel;
            Assert.IsTrue(response?.InvoiceSummary.Count() == 2);
            Assert.IsTrue(response.IsHealthy);
        }

        [Test]
        [Ignore("test build failure")]
        public void GetInvoiceHealthSummaryAsyncThrowsTest()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new InvoiceSummaryMappingProfile()); });
            var controller = new InvoiceHealthController(_repository, _factory, mappingConfig.CreateMapper(), _configuration);
            // Act/Assert
            Assert.ThrowsAsync<BaseException>(() => controller.GetInvoiceHealthSummaryAsync(3, 1));
        }
    }
}