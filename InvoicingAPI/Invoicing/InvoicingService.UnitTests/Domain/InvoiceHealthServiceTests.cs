using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvoicingService.Domain;
using InvoicingService.Domain.MappingConfigs;
using InvoicingService.RestClients;
using InvoicingService.Services;
using Moq;
using NUnit.Framework;

namespace InvoicingService.UnitTests.Domain
{
    [TestFixture]
    public class InvoiceHealthServiceTests
    {
        private readonly IMapper _mapper;
        private const string ProviderCode = "XERO";
        private static readonly DateTime FromDate = DateTime.Now.AddMonths(-6);

        public InvoiceHealthServiceTests()
        {
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new InvoiceMappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Test]
        public void HealthyTest_Customer1()
        {
            var mockServiceClient = new Mock<IInvoiceClient>();
            mockServiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(1, FromDate))
                .Returns(Task.Run(() => new List<Invoice>
                {
                    new("fred", DateTime.Now, 100000, 0),
                    new("bob", DateTime.Now, 100000, 100000)
                }));

            IInvoiceHealthService service = new InvoiceHealthService(_mapper, mockServiceClient.Object);
            var invoices = service.GetInvoicesFromDate(1, FromDate, ProviderCode).Result;

            Assert.IsNotEmpty(invoices);
            // All properties should have data
            Assert.IsTrue(invoices.All(x => x.InvoiceDate != DateTime.MinValue && !string.IsNullOrEmpty(x.CustomerName) && x.OriginalAmount > 0));
            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < FromDate));
            // Company 1 should be healthy 
            Assert.IsTrue(service.GetHealthStatus(invoices, 90));
        }

        [Test]
        public void UnHealthyOldUnpaidInvoiceTest_Customer2()
        {
            var mockServiceClient = new Mock<IInvoiceClient>();
            mockServiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(2, FromDate))
                .Returns(Task.Run(() => new List<Invoice>
                {
                    new("fred", DateTime.Now, 100000, 0),
                    new("bob", DateTime.Now.AddDays(-91), 100000, 100000) // unpaid invoice older than 90 days
                }));

            IInvoiceHealthService service = new InvoiceHealthService(_mapper, mockServiceClient.Object);
            var invoices = service.GetInvoicesFromDate(2, FromDate, ProviderCode).Result;

            Assert.IsNotEmpty(invoices);
            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < FromDate));
            // Company 2 should be unhealthy 
            Assert.IsFalse(service.GetHealthStatus(invoices, 90));
        }

        [Test]
        public void UnHealthySumLessThan100KTest_Customer3()
        {
            var mockServiceClient = new Mock<IInvoiceClient>();
            mockServiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(3, FromDate))
                .Returns(Task.Run(() => new List<Invoice>
                {
                    new("fred", DateTime.Now, 50000, 0),
                    new("bob", DateTime.Now, 20000, 20000) // sum of invoices is less than 100k
                }));

            IInvoiceHealthService service = new InvoiceHealthService(_mapper, mockServiceClient.Object);
            var invoices = service.GetInvoicesFromDate(3, FromDate, ProviderCode).Result;

            Assert.IsNotEmpty(invoices);
            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < FromDate));
            // Company 3 should be unhealthy 
            Assert.IsFalse(service.GetHealthStatus(invoices, 90));
        }
    }
}