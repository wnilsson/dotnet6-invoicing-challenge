using System;

namespace Invoicing.Api.RestClients.Xero.Models
{
    /// <summary>
    /// Using property names similar to the GET invoices summary only Xero API
    /// https://api-explorer.xero.com/accounting/invoices/getinvoices?query-summaryonly=true
    /// </summary>
    public class XeroInvoice
    {
        /// <summary/>
        public int InvoiceNumber { get; set; }

        /// <summary/>
        public int CustomerId { get; set; }

        /// <summary/>
        public string CustomerName { get; set; }

        /// <summary/>
        public DateTime Date { get; set; }

        /// <summary/>
        public decimal AmountDue { get; set; }

        /// <summary/>
        public decimal AmountPaid { get; set; }

        /// <summary/>
        public decimal OriginalAmount => AmountDue + AmountPaid;
    }
}
