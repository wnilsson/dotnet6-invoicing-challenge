using System.Threading.Tasks;
using Invoicing.Api.Domain;
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
            var mockRepo = new Mock<ICompanyProviderRepository>();
            mockRepo.Setup(x => x.SingleOrDefaultAsync(y => y.Provider, z => z.CompanyId == 1))
                .Returns(Task.Run(TestHelper.GetCompanyProvider));
            // Act
            var result = mockRepo.Object.SingleOrDefaultAsync(x => x.Provider, y => y.CompanyId == 1).Result;
            // Assert
            Assert.AreEqual(result.Provider.ProviderCode, TestHelper.XeroProviderCode);
        }
    }
}
