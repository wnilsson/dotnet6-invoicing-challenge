﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Infrastructure.Core.Models;

namespace InvoicingService.Domain.Models
{
    /// <summary/>
    [ExcludeFromCodeCoverage]
    public class Company : IAggregateRoot
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
