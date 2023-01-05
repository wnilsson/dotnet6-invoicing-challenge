using System;
using Invoicing.Api.RestClients.Myob;
using Invoicing.Api.RestClients.Xero;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicing.Api.RestClients
{
    public interface IInvoiceClientFactory
    {
        IInvoiceClient GetInvoiceClient(string providerCode);
    }

    public class InvoiceClientFactory : IInvoiceClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public InvoiceClientFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Returns an invoice client based on provider code
        /// </summary>
        public IInvoiceClient GetInvoiceClient(string providerCode)
        {
            switch (providerCode)
            {
                case "XERO": 
                    return (IInvoiceClient)_serviceProvider.GetRequiredService(typeof(XeroClient));
                case "MYOB": 
                    return (IInvoiceClient)_serviceProvider.GetRequiredService(typeof(MyobClient));
                default: 
                    throw new NotImplementedException();
            }
        }
    }
}
