using System;

namespace Invoicing.Api.Domain.Models
{
    /// <summary>
    /// Business domain model object
    /// </summary>
    public class Invoice
    {
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
