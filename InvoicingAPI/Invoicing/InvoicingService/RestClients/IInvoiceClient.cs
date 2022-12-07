using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoicingService.Domain.Models;

namespace InvoicingService.RestClients
{
    /// <summary/>
    public interface IInvoiceClient
    {
        /// <summary>
        /// Get all invoices for company with Id = companyId from fromDate
        /// </summary>
        Task<List<Invoice>> GetInvoiceSummaryFromDateAsync(int companyId, DateTime fromDate);
    }
}
