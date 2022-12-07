using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoicingService.RestClients;
using Moq;
using NUnit.Framework;
using InvoicingService.Domain.Extensions;
using InvoicingService.Domain.Models;

namespace InvoicingService.UnitTests.Domain
{
    [TestFixture]
    public class InvoiceHealthServiceTests
    {
        private const int HealthPeriodDays = 90;
        private static readonly DateTime FromDate = DateTime.Now.AddMonths(-6);
        
        [Test]
        public void HealthyTest_Customer1()
        {
            const int customerId = 1;
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(customerId, FromDate))
                .Returns(Task.Run(() => new List<Invoice>
                {
                    new("fred", DateTime.Now, 100000, 0),
                    new("bob", DateTime.Now, 100000, 100000)
                }));

            var invoices = mockInvoiceClient.Object.GetInvoiceSummaryFromDateAsync(customerId, FromDate).Result;
            Assert.IsNotEmpty(invoices);

            // All properties should have data
            Assert.IsTrue(invoices.All(x => x.InvoiceDate != DateTime.MinValue && !string.IsNullOrEmpty(x.CustomerName) && x.OriginalAmount > 0));
            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < FromDate));
            // Company 1 should be healthy 
            Assert.IsTrue(invoices.GetHealthStatus(HealthPeriodDays));
        }

        [Test]
        public void UnHealthyOldUnpaidInvoiceTest_Customer2()
        {
            const int customerId = 2;
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(customerId, FromDate))
                .Returns(Task.Run(() => new List<Invoice>
                {
                    new("fred", DateTime.Now, 100000, 0),
                    new("bob", DateTime.Now.AddDays(-91), 100000, 100000) // unpaid invoice older than 90 days
                }));

            var invoices = mockInvoiceClient.Object.GetInvoiceSummaryFromDateAsync(customerId, FromDate).Result;
            Assert.IsNotEmpty(invoices);

            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < FromDate));
            // Company 2 should be unhealthy 
            Assert.IsFalse(invoices.GetHealthStatus(HealthPeriodDays));
        }

        [Test]
        public void UnHealthySumLessThan100KTest_Customer3()
        {
            const int customerId = 3;
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(customerId, FromDate))
                .Returns(Task.Run(() => new List<Invoice>
                {
                    new("fred", DateTime.Now, 50000, 0),
                    new("bob", DateTime.Now, 20000, 20000) // sum of invoices is less than 100k
                }));

            var invoices = mockInvoiceClient.Object.GetInvoiceSummaryFromDateAsync(customerId, FromDate).Result;
            Assert.IsNotEmpty(invoices);

            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < FromDate));
            // Company 3 should be unhealthy 
            Assert.IsFalse(invoices.GetHealthStatus(HealthPeriodDays));
        }
    }
}