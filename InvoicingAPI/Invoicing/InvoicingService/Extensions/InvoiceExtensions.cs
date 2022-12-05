using System;
using System.Collections.Generic;
using System.Linq;
using InvoicingService.Domain;

namespace InvoicingService.Extensions
{
    /// <summary/>
    public static class InvoiceExtensions
    {
        /// <summary>
        /// Get the overall health status for a set of invoices
        /// A healthy account is
        /// - No outstanding invoice is older than 90 days
        /// - the sum of invoices’ original amount is greater than 100k
        /// </summary>
        public static bool GetHealthStatus(this List<Invoice> invoices, int healthPeriodDays)
        {
            return invoices.Where(x => (DateTime.Now - x.InvoiceDate).Days > healthPeriodDays).All(x => x.OutstandingAmount == 0) &&
                   invoices.Where(y => (DateTime.Now - y.InvoiceDate).Days <= healthPeriodDays).Sum(y => y.OriginalAmount) > 100000;
        }
    }
}
