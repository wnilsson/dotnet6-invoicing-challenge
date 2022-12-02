using InvoicingService.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InvoicingService.DataAccess.Configuration
{
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
