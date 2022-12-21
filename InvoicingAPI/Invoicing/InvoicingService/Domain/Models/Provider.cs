using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Infrastructure.Core.Models;

namespace InvoicingService.Domain.Models
{
    /// <summary/>
    [ExcludeFromCodeCoverage]
    public class Provider : IAggregateRoot
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
