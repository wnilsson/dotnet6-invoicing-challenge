using System;
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
    public class InvoiceClientFactoryTests
    {
        private readonly Mock<IServiceProvider> _serviceProvider;

        public InvoiceClientFactoryTests()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AddProfile(new XeroInvoiceMappingProfile()); });
            var mapper = mappingConfig.CreateMapper();
            _serviceProvider = new Mock<IServiceProvider>();
            _serviceProvider.Setup(x => x.GetService(typeof(XeroClient))).Returns(new XeroClient(mapper));
            _serviceProvider.Setup(x => x.GetService(typeof(MyobClient))).Returns(new MyobClient(mapper));
        }
        
        [Test]
        public void XeroClientFactoryTest()
        {
            // Act
            var client = new InvoiceClientFactory(_serviceProvider.Object).GetInvoiceClient("XERO");
            // Assert
            Assert.IsTrue(client is XeroClient);
        }

        [Test]
        public void MyobClientFactoryTest()
        {
            // Act
            var client = new InvoiceClientFactory(_serviceProvider.Object).GetInvoiceClient("MYOB");
            // Assert
            Assert.IsTrue(client is MyobClient);
        }

        [Test]
        public void UnimplementedClientFactoryTest()
        {
            // Act/Assert
            Assert.Throws<NotImplementedException>(() => new InvoiceClientFactory(_serviceProvider.Object).GetInvoiceClient("XXXX"));
        }
    }
}
