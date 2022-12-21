using AutoMapper;
using NUnit.Framework;
using System;
using System.Linq;
using InvoicingService.RestClients;
using InvoicingService.RestClients.MappingConfigs;

namespace InvoicingService.Test.Domain
{
    [TestFixture]
    public class InvoiceClientTests
    {
        private readonly IMapper _mapper;

        public InvoiceClientTests()
        {
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new XeroInvoiceMappingProfile()); });
            _mapper = mappingConfig.CreateMapper();
        }
        
        [Test]
        public void XeroClientTest()
        {
            var xeroClient = new InvoiceClientFactory().GetInstance("XERO", _mapper);
            var invoices = xeroClient.GetInvoiceSummaryFromDateAsync(1, DateTime.Now.AddMonths(-6)).Result;
            Assert.IsTrue(invoices.Count > 0);
            Assert.IsTrue(invoices.Any(x => x.CustomerName == "Donald Duck"));
        }

        [Test]
        public void MyobClientThrowsNotImplementedTest()
        {
            var xeroClient = new InvoiceClientFactory().GetInstance("MYOB", _mapper);
            Assert.Throws<NotImplementedException>(() => xeroClient.GetInvoiceSummaryFromDateAsync(1, DateTime.Now.AddMonths(-6)));
        }

        [Test]
        public void InvalidClientThrowsNotImplementedTest()
        {
            Assert.Throws<NotImplementedException>(() => new InvoiceClientFactory().GetInstance("XXXX", _mapper));
        }
    }
}
