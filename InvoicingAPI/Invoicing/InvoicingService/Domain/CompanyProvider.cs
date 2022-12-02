using Infrastructure.Core.Models;

namespace InvoicingService.Domain
{
    /// <summary/>
    public class CompanyProvider : IAggregateRoot
    {
        /// <summary/>
        public CompanyProvider() { }

        /// <summary>
        /// CompanyProvider Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Providers client secret for the associated company
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Flag to indicate if the company provider is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary/>
        public int CompanyId { get; set; }
        /// <summary/>
        public Company Company { get; set; }

        /// <summary/>
        public int ProviderId { get; set; }
        /// <summary/>
        public Provider Provider { get; set; }

    }
}
