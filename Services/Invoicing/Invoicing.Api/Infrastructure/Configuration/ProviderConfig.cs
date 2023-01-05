using System.Diagnostics.CodeAnalysis;
using Invoicing.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoicing.Api.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ProviderConfig : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProviderCode).HasColumnName("ProviderCode").HasMaxLength(10).IsRequired();

            builder.Property(x => x.ProviderName).HasColumnName("ProviderName").HasMaxLength(200).IsRequired();

            // Seed some dummy data
            builder.HasData(new Provider { Id = 1, ProviderCode = "XERO", ProviderName = "Xero" });
        }
    }
}
