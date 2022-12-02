using System;
using System.Collections.Generic;

namespace InvoicingService.Domain
{
    /// <summary/>
    public class Provider
    {
        /// <summary>
        /// Provider Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Provider code
        /// </summary>
        public string ProviderCode { get; set; }

        /// <summary>
        /// Provider name
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary/>
        public virtual IList<CompanyProvider> CompanyProvider { get; set; }
    }
}
