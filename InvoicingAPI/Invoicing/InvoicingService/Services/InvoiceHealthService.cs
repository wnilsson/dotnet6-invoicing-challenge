using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvoicingService.Domain;
using InvoicingService.RestClients;

namespace InvoicingService.Services
{
    /// <summary/>
    public class InvoiceHealthService : IInvoiceHealthService
    {
        private readonly IMapper _mapper;
        private IInvoiceClient _invoiceClient;

        /// <summary/>
        public InvoiceHealthService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Unit testing
        /// </summary>
        public InvoiceHealthService(IMapper mapper, IInvoiceClient invoiceClient)
        {
            _mapper = mapper;
            _invoiceClient = invoiceClient;
        }        

        /// <summary>
        /// Fetch all invoices from fromDate to now for customer customerId, ordered by invoice date
        /// </summary>
        public Task<List<Invoice>> GetInvoicesFromDate(int customerId, DateTime fromDate)
        {
            // ToDo - fetch the provider Id from DB using customerId
            _invoiceClient ??= InvoiceClientFactory.GetInstance("XERO", _mapper);

            return _invoiceClient.GetInvoiceSummaryFromDate(customerId, fromDate);
        }

        /// <summary>
        /// Get the overall health status for a set of invoices
        /// A healthy account is
        /// - No outstanding invoice is older than 90 days
        /// - the sum of invoices’ original amount is greater than 100k
        /// </summary>
        public bool GetHealthStatus(List<Invoice> invoices, int healthPeriodDays)
        {
            return invoices.Where(x => (DateTime.Now - x.InvoiceDate).Days > healthPeriodDays).All(x => x.OutstandingAmount == 0) &&
                   invoices.Where(y => (DateTime.Now - y.InvoiceDate).Days <= healthPeriodDays).Sum(y => y.OriginalAmount) > 100000;
        }
    }
}
