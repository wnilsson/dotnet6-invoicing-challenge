using System.Diagnostics.CodeAnalysis;
using Invoicing.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoicing.Api.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName).HasColumnName("CompanyName").HasMaxLength(200).IsRequired();

            builder.Property(x => x.CompanyContact).HasColumnName("CompanyContact").HasMaxLength(200).IsRequired(false);

            builder.Property(x => x.CompanyUrl).HasColumnName("CompanyUrl").HasMaxLength(200).IsRequired(false);

            // Seed some dummy data
            builder.HasData(
                new Company { Id = 1, CompanyName = "Company ABC" },
                new Company { Id = 2, CompanyName = "Company DEF" },
                new Company { Id = 3, CompanyName = "Company XYZ" });
        }
    }
}
