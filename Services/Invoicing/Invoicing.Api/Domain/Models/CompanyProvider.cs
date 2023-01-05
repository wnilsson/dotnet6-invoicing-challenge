using Infrastructure.Core.Models;

namespace Invoicing.Api.Domain.Models
{
    public class CompanyProvider : IAggregateRoot
    {
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

        // Relationships
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
