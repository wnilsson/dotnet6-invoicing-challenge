namespace Invoicing.Api.Models
{
    /// <summary>
    /// Invoice Summary 
    /// </summary>
    public class InvoiceSummaryItemViewModel
    {
        /// <summary>
        /// Invoice Customer's Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Invoice Date of issue
        /// </summary>
        public string IssueDate { get; set; }

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
