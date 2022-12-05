using System;
using AutoMapper;
using InvoicingService.RestClients.Myob;
using InvoicingService.RestClients.Xero;

namespace InvoicingService.RestClients
{
    /// <summary/>
    public interface IInvoiceClientFactory
    {
        IInvoiceClient GetInstance(string providerCode, IMapper mapper);
    }

    public class InvoiceClientFactory : IInvoiceClientFactory
    {
        /// <summary>
        /// Returns an invoice client based on provider code
        /// </summary>
        public IInvoiceClient GetInstance(string providerCode, IMapper mapper)
        {
            switch (providerCode)
            {
                case "XERO": return new XeroClient(mapper);
                case "MYOB": return new MyobClient(mapper);
                default: throw new NotImplementedException();
            }
        }
    }
}
