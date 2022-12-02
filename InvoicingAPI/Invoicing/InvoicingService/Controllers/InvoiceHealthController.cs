using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvoicingService.Api.Models;
using InvoicingService.Domain;
using InvoicingService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InvoicingService.Controllers
{
    /// <summary/>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InvoiceHealthController : ControllerBase
    {
        private readonly IInvoiceHealthService _invoiceHealthService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ICompanyProviderRepository _repository;

        /// <summary/>
        public InvoiceHealthController(
            IInvoiceHealthService healthService,
            ICompanyProviderRepository repository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _invoiceHealthService = healthService;
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// Return a customers account health info and a summary of recent invoices
        /// </summary>
        [HttpGet]
        [Route("{customerId}/take/{take}")]
        public async Task<IActionResult> GetInvoiceHealthSummary(int customerId, int take = 10)
        {
            // Get the provider code for customerId
            var companyProvider = await _repository.SingleOrDefaultAsync(x => x.Provider, y => y.CompanyId == customerId);
            if (companyProvider == null) return BadRequest($"Provider not found for customerId {customerId}");

            // Get an invoice summary via the customers 3rd party provider for a date period
            var fromDate = DateTime.Now.AddMonths(-Convert.ToInt32(_configuration["InvoicePeriodMonths"]));
            List <Invoice> invoices = await _invoiceHealthService.GetInvoicesFromDate(customerId, fromDate, companyProvider.Provider.ProviderCode);
            if (invoices.Count == 0) return NotFound();

            var response = new InvoiceHealthViewModel
            {
                CustomerId = customerId,
                // Set the health flag of the customers accounts
                IsHealthy = _invoiceHealthService.GetHealthStatus(invoices, Convert.ToInt32(_configuration["InvoiceHealthPeriodDays"])),
                // Get the recent invoice summary results, ordered descending
                InvoiceSummary = invoices
                    .OrderByDescending(x => x.InvoiceDate)
                    .Take(take)
                    .Select(x => _mapper.Map<InvoiceSummaryItemViewModel>(x))
            };

            return Ok(response);
        }
    }
}
