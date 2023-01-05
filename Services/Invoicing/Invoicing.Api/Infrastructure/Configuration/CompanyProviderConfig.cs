using System.Diagnostics.CodeAnalysis;
using Invoicing.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoicing.Api.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CompanyProviderConfig : IEntityTypeConfiguration<CompanyProvider>
    {
        public void Configure(EntityTypeBuilder<CompanyProvider> builder)
        {
            builder.ToTable("CompanyProviders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("Id");

            builder.Property(x => x.ClientSecret).HasColumnName("ClientSecret").HasMaxLength(100).HasDefaultValue("000000").IsRequired();

            builder.Property(x => x.IsActive).IsRequired();

            // Setup navigation properties
            builder.HasOne(x => x.Company).WithMany(x => x.CompanyProvider).HasForeignKey(x => x.CompanyId);
            builder.HasOne(x => x.Provider).WithMany(x => x.CompanyProvider).HasForeignKey(x => x.ProviderId);

            // Seed some dummy data
            builder.HasData(
                new CompanyProvider { Id = 1, CompanyId = 1, ProviderId = 1, IsActive = true },
                new CompanyProvider { Id = 2, CompanyId = 2, ProviderId = 1, IsActive = true },
                new CompanyProvider { Id = 3, CompanyId = 3, ProviderId = 1, IsActive = true });
        }
    }
}
