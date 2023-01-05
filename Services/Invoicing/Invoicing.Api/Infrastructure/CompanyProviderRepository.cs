using System.Diagnostics.CodeAnalysis;
using Infrastructure.Core.DataAccess.EF;
using Invoicing.Api.Domain;
using Invoicing.Api.Domain.Models;

namespace Invoicing.Api.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class CompanyProviderRepository : Repository<CompanyProvider>, ICompanyProviderRepository
    {
        public CompanyProviderRepository(InvoicingDbContext context) : base(context)
        {

        }
    }
}
