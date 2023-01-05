using System.Collections.Generic;

namespace Invoicing.Api.Models
{
    /// <summary>
    /// Invoice Health
    /// </summary>
    public class InvoiceHealthViewModel
    {
        /// <summary>
        /// The Id of the customer (organisation)
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Flag to determine overall account health
        /// </summary>
        public bool IsHealthy { get; set; }

        /// <summary>
        /// Most recent invoices in summary form
        /// </summary>
        public IEnumerable<InvoiceSummaryItemViewModel> InvoiceSummary { get; set; }
    }
}
