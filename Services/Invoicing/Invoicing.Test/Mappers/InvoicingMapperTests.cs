using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Invoicing.Api.Domain.Models;
using Invoicing.Api.Models;
using Invoicing.Api.Models.MappingConfigs;
using Invoicing.Api.RestClients.Xero.Models;
using Invoicing.Api.RestClients.Xero.Models.MappingConfigs;
using NUnit.Framework;

namespace Invoicing.Test.Mappers
{
    [TestFixture]
    public class InvoicingMapperTests
    {
        [Test]
        public void XeroInvoiceMapperTest()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new XeroInvoiceMappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            var invoiceDate = DateTime.Now;

            var xeroInvoices = new List<XeroInvoice>
            {
                new XeroInvoice
                {
                    InvoiceNumber = 1, CustomerId = 1, CustomerName = "fred", Date = invoiceDate.AddDays(-200), AmountPaid = 150, AmountDue = 30
                },
                new XeroInvoice
                {
                    InvoiceNumber = 2, CustomerId = 1, CustomerName = "bob", Date = invoiceDate.AddDays(-130), AmountPaid = 2000, AmountDue = 0
                }
            };

            // Act
            var invoices = xeroInvoices.Select(x => mapper.Map<Invoice>(x)).ToList();
            
            // Assert
            Assert.AreEqual(invoices.Count, 2);
            var invoice = invoices.First();
            Assert.AreEqual(invoice.CustomerName, "fred");
            Assert.AreEqual(invoice.OriginalAmount, xeroInvoices.First().OriginalAmount);
            Assert.AreEqual(invoice.OutstandingAmount, 30);
            Assert.AreEqual(invoice.InvoiceDate, invoiceDate.AddDays(-200));
        }

        [Test]
        public void InvoiceSummaryMapperTest()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new InvoiceSummaryMappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            var invoiceDate = DateTime.Now;

            var invoices = new List<Invoice>
            {
                new Invoice { CustomerName = "fred", InvoiceDate = invoiceDate, OriginalAmount = 4000, OutstandingAmount = 1000 },
                new Invoice { CustomerName = "bob", InvoiceDate = invoiceDate, OriginalAmount = 100000, OutstandingAmount = 2000 }
            };

            // Act
            var invoiceSummaries = invoices.Select(x => mapper.Map<InvoiceSummaryItemViewModel>(x)).ToList();
            
            // Assert
            Assert.AreEqual(invoiceSummaries.Count, 2);
            var invoiceSummary = invoiceSummaries.First();
            Assert.AreEqual(invoiceSummary.CustomerName, "fred");
            Assert.AreEqual(invoiceSummary.OriginalAmount, 4000);
            Assert.AreEqual(invoiceSummary.OutstandingAmount, 1000);
            Assert.AreEqual(invoiceSummary.IssueDate, invoiceDate.ToString("dd-MMM-yyyy"));

            var invoiceHealth = new InvoiceHealthViewModel { InvoiceSummary = invoiceSummaries, IsHealthy = true, CustomerId = 1 };
            Assert.AreEqual(invoiceHealth.InvoiceSummary.Count(), 2);
        }
    }
}
