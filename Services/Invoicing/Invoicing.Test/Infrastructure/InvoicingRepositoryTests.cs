using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicing.Api.Domain;
using Invoicing.Api.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Invoicing.Test.Infrastructure
{
    [TestFixture]
    public class InvoicingRepositoryTests
    {
        [Test]
        public void CompanyProviderRepoTest()
        {
            // Arrange
            const string providerCode = "Xero";
            var provider = new Provider { Id = 1, ProviderCode = providerCode, ProviderName = providerCode };
            var company = new Company { Id = 1, CompanyName = "Attrib", CompanyContact = "info@company.com", CompanyUrl = "company.com" };
            var companyProvider = new CompanyProvider { Id = 1, CompanyId = 1, ProviderId = 1, Provider = provider, Company = company, IsActive = true, ClientSecret = "xxxxxxx" };
            provider.CompanyProvider = new List<CompanyProvider> { companyProvider };
            company.CompanyProvider = new List<CompanyProvider> { companyProvider };
            var mockRepo = new Mock<ICompanyProviderRepository>();
            mockRepo.Setup(x => x.SingleOrDefaultAsync(y => y.Provider, z => z.CompanyId == 1)).Returns(Task.Run(() => companyProvider));

            // Act
            var result = mockRepo.Object.SingleOrDefaultAsync(x => x.Provider, y => y.CompanyId == 1).Result;
            // Assert
            Assert.AreEqual(result.Provider.ProviderCode, providerCode);
        }
    }
}
