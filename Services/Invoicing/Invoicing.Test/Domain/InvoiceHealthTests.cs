using System;
using System.Linq;
using System.Threading.Tasks;
using Invoicing.Api.Domain.Extensions;
using Invoicing.Api.RestClients;
using Moq;
using NUnit.Framework;

namespace Invoicing.Test.Domain
{
    [TestFixture]
    public class InvoiceHealthTests
    {
        [Test]
        public void HealthyTest_Customer1()
        {
            // Arrange
            const int customerId = 1;
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(customerId, TestHelper.FromDate))
                .Returns(Task.Run(TestHelper.GetInvoices));
            
            // Act
            var invoices = mockInvoiceClient.Object.GetInvoiceSummaryFromDateAsync(customerId, TestHelper.FromDate).Result;
            // Assert
            Assert.IsNotEmpty(invoices);
            // All properties should have data
            Assert.IsTrue(invoices.All(x => x.InvoiceDate != DateTime.MinValue && !string.IsNullOrEmpty(x.CustomerName) && x.OriginalAmount > 0));
            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < TestHelper.FromDate));
            // Company 1 should be healthy 
            Assert.IsTrue(invoices.GetHealthStatus(TestHelper.HealthPeriodDays));
        }

        [Test]
        public void UnHealthyOldUnpaidInvoiceTest_Customer2()
        {
            // Arrange
            const int customerId = 2;
            var mockInvoices = TestHelper.GetInvoices();
            // unpaid invoice older than 90 days
            mockInvoices.Last().InvoiceDate = DateTime.Now.AddDays(-91);
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(customerId, TestHelper.FromDate))
                .Returns(Task.Run(() => mockInvoices));

            // Act
            var invoices = mockInvoiceClient.Object.GetInvoiceSummaryFromDateAsync(customerId, TestHelper.FromDate).Result;
            // Assert
            Assert.IsNotEmpty(invoices);
            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < TestHelper.FromDate));
            // Company 2 should be unhealthy 
            Assert.IsFalse(invoices.GetHealthStatus(TestHelper.HealthPeriodDays));
        }

        [Test]
        public void UnHealthySumLessThan100KTest_Customer3()
        {
            // Arrange
            const int customerId = 3;
            var mockInvoices = TestHelper.GetInvoices();
            // sum of invoices is less than 100k
            mockInvoices.Last().OriginalAmount = mockInvoices.Last().OutstandingAmount = 1000;
            var mockInvoiceClient = new Mock<IInvoiceClient>();
            mockInvoiceClient.Setup(x => x.GetInvoiceSummaryFromDateAsync(customerId, TestHelper.FromDate))
                .Returns(Task.Run(() => mockInvoices));

            // Act
            var invoices = mockInvoiceClient.Object.GetInvoiceSummaryFromDateAsync(customerId, TestHelper.FromDate).Result;
            // Assert
            Assert.IsNotEmpty(invoices);
            // None should be older than 6 months
            Assert.IsFalse(invoices.Any(x => x.InvoiceDate < TestHelper.FromDate));
            // Company 3 should be unhealthy 
            Assert.IsFalse(invoices.GetHealthStatus(TestHelper.HealthPeriodDays));
        }
    }
}