using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Invoicing.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.Api.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class InvoicingDbContext : DbContext
    {
        public InvoicingDbContext(DbContextOptions<InvoicingDbContext> options) : base(options) { }
        
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<CompanyProvider> CompanyProviders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Scans the assembly for all types that implement IEntityTypeConfiguration and registers each one automatically
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
