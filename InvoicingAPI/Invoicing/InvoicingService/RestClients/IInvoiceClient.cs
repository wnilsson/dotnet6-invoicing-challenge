using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoicingService.Domain;

namespace InvoicingService.RestClients
{
    public interface IInvoiceClient
    {
        Task<List<Invoice>> GetInvoiceSummaryFromDateAsync(int companyId, DateTime fromDate);
    }
}
