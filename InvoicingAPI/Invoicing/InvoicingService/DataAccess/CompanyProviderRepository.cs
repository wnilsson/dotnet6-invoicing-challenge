using Infrastructure.Core.DataAccess.EF;
using InvoicingService.Domain;
using InvoicingService.Domain.Models;

namespace InvoicingService.DataAccess
{
    public class CompanyProviderRepository : Repository<CompanyProvider>, ICompanyProviderRepository
    {
        public CompanyProviderRepository(InvoicingDbContext context) : base(context)
        {

        }
    }
}
