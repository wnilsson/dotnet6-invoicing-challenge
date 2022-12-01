using System;
using AutoMapper;
using InvoicingService.RestClients.Myob;
using InvoicingService.RestClients.Xero;

namespace InvoicingService.RestClients
{
    public class InvoiceClientFactory
    {
        internal static IInvoiceClient GetInstance(string provider, IMapper mapper)
        {
            switch (provider)
            {
                case "XERO": return new XeroClient(mapper);
                case "MYOB": return new MyobClient(mapper);
                default: throw new NotImplementedException();
            }
        }
    }
}
