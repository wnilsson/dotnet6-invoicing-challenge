using System;
using System.Collections.Generic;
using Invoicing.Api.Domain.Models;

namespace Invoicing.Test
{
    internal static class TestHelper
    {
        internal static readonly DateTime FromDate = DateTime.Today.AddMonths(-6);
        internal const string XeroProviderCode = "XERO";
        internal const int HealthPeriodDays = 90;
        internal const int InvoicePeriodMonths = 6;

        internal static CompanyProvider GetCompanyProvider()
        {
            var provider = new Provider { Id = 1, ProviderCode = XeroProviderCode, ProviderName = XeroProviderCode };
            var company = new Company { Id = 1, CompanyName = "Attrib", CompanyContact = "info@company.com", CompanyUrl = "company.com" };

            var companyProvider = new CompanyProvider { Id = 1, CompanyId = 1, ProviderId = 1, Provider = provider, Company = company, IsActive = true, ClientSecret = "xxxxxxx" };
            provider.CompanyProvider = new List<CompanyProvider> { companyProvider };
            company.CompanyProvider = new List<CompanyProvider> { companyProvider };
            return companyProvider;
        }

        internal static List<Invoice> GetInvoices()
        {
            return new List<Invoice>
            {
                new() { CustomerName = "fred", InvoiceDate = DateTime.Now, OriginalAmount = 80000, OutstandingAmount = 0 },
                new() { CustomerName = "bob", InvoiceDate = DateTime.Now, OriginalAmount = 100000, OutstandingAmount = 100000 }
            };
        }
    }
}
