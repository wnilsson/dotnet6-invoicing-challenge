using System;

namespace InvoicingService.Domain
{
    /// <summary/>
    public class Invoice
    {
        /// <summary/>
        public Invoice() { }

        /// <summary>
        /// Unit testing
        /// </summary>
        public Invoice(string custName, DateTime invDate, decimal origAmt, decimal outAmt)
        {
            CustomerName = custName;
            InvoiceDate = invDate;
            OriginalAmount = origAmt;
            OutstandingAmount = origAmt;
        }

        /// <summary>
        /// Customers name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Invoice Date
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Original invoice amount
        /// </summary>
        public decimal OriginalAmount { get; set; }

        /// <summary>
        /// Outstanding invoice amount
        /// </summary>
        public decimal OutstandingAmount { get; set; }
    }
}
