using System;
using InvoicingService.RestClients.Myob;
using InvoicingService.RestClients.Xero;
using Microsoft.Extensions.DependencyInjection;

namespace InvoicingService.RestClients
{
    /// <summary/>
    public interface IInvoiceClientFactory
    {
        /// <summary/>
        IInvoiceClient GetInvoiceClient(string providerCode);
    }

    /// <summary/>
    public class InvoiceClientFactory : IInvoiceClientFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary/>
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
