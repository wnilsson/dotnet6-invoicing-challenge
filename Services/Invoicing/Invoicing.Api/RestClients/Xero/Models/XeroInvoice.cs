using System;

namespace Invoicing.Api.RestClients.Xero.Models
{
    /// <summary>
    /// Using property names similar to the GET invoices summary only Xero API
    /// https://api-explorer.xero.com/accounting/invoices/getinvoices?query-summaryonly=true
    /// </summary>
    public class XeroInvoice
    {
        public int InvoiceNumber { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public DateTime Date { get; set; }

        public decimal AmountDue { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal OriginalAmount => AmountDue + AmountPaid;
    }
}
