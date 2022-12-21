using Microsoft.EntityFrameworkCore;
using System.Reflection;
using InvoicingService.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace InvoicingService.DataAccess
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
