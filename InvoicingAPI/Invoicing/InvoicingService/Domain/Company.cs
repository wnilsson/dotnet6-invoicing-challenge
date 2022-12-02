using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoicingService.Domain
{
    /// <summary/>
    public class Company 
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Company primary contact
        /// </summary>
        public string CompanyContact { get; set; }

        /// <summary>
        /// Company Url
        /// </summary>
        public string CompanyUrl { get; set; }

        /// <summary/>
        public virtual IList<CompanyProvider> CompanyProvider { get; set; }
}
}
