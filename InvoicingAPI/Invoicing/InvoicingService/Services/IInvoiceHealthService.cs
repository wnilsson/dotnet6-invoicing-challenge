using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvoicingService.Domain;

namespace InvoicingService.Services
{
    /// <summary/>
    public interface IInvoiceHealthService
    {
        /// <summary/>
        Task<List<Invoice>> GetInvoicesFromDate(int customerId, DateTime fromDate, string providerCode);

        /// <summary/>
        bool GetHealthStatus(List<Invoice> invoices, int healthPeriodDays);
    }
}
