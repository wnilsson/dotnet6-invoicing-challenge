using System;
using System.Linq;
using AutoMapper;
using Invoicing.Api.RestClients;
using Invoicing.Api.RestClients.Myob;
using Invoicing.Api.RestClients.Xero;
using Invoicing.Api.RestClients.Xero.Models.MappingConfigs;
using Moq;
using NUnit.Framework;

namespace Invoicing.Test.Domain
{
    [TestFixture]
    public class InvoiceClientTests
    {
        private readonly Mock<IServiceProvider> _serviceProvider;

        public InvoiceClientTests()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new XeroInvoiceMappingProfile()); });
            var mapper = mappingConfig.CreateMapper();
            _serviceProvider = new Mock<IServiceProvider>();
            _serviceProvider.Setup(x => x.GetService(typeof(XeroClient))).Returns(new XeroClient(mapper));
            _serviceProvider.Setup(x => x.GetService(typeof(MyobClient))).Returns(new MyobClient(mapper));
        }
        
        [Test]
        public void XeroClientTest()
        {
            
            var xeroClient = new InvoiceClientFactory(_serviceProvider.Object).GetInvoiceClient("XERO");
            // Act
            var invoices = xeroClient.GetInvoiceSummaryFromDateAsync(1, DateTime.Now.AddMonths(-6)).Result;
            // Assert
            Assert.IsTrue(invoices.Count > 0);
            Assert.IsTrue(invoices.Any(x => x.CustomerName == "Donald Duck"));
        }

        [Test]
        public void MyobClientThrowsNotImplementedTest()
        {
            var myobClient = new InvoiceClientFactory(_serviceProvider.Object).GetInvoiceClient("MYOB");
            // Act/Assert
            Assert.Throws<NotImplementedException>(() => myobClient.GetInvoiceSummaryFromDateAsync(1, DateTime.Now.AddMonths(-6)));
        }

        [Test]
        public void InvalidClientThrowsNotImplementedTest()
        {
            // Act/Assert
            Assert.Throws<NotImplementedException>(() => new InvoiceClientFactory(_serviceProvider.Object).GetInvoiceClient("XXXX"));
        }
    }
}
