using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Invoicing.Api.Domain;
using Invoicing.Api.Domain.Extensions;
using Invoicing.Api.Domain.Models;
using Invoicing.Api.Models;
using Invoicing.Api.RestClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Invoicing.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class InvoiceHealthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ICompanyProviderRepository _repository;
        private readonly IInvoiceClientFactory _invoiceClientFactory;

        public InvoiceHealthController(
            ICompanyProviderRepository repository, 
            IInvoiceClientFactory invoiceClientFactory,
            IMapper mapper, 
            IConfiguration configuration)
        {
            _repository = repository;
            _invoiceClientFactory = invoiceClientFactory;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// Return a customer's (organisation) account health info and a summary of recent invoices
        /// GET /api/v1/InvoiceHealth[?customerId=1&summaryCount=10]
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<InvoiceHealthViewModel>> GetInvoiceHealthSummaryAsync(
            [FromQuery] [Required] int customerId,
            [FromQuery] int summaryCount = 10)
        {
            // Get the provider code for customerId
            var companyProvider = await _repository.SingleOrDefaultAsync(x => x.Provider, y => y.CompanyId == customerId).ConfigureAwait(false);
            if (companyProvider == null) return BadRequest($"Provider not found for customerId {customerId}");
            // Use factory to get the required invoice client for the provider code
            var invoiceClient = _invoiceClientFactory.GetInvoiceClient(companyProvider.Provider.ProviderCode);

            // Get an invoice summary via the customers 3rd party provider for a date period
            var fromDate = DateTime.Now.AddMonths(-Convert.ToInt32(_configuration["InvoicePeriodMonths"]));
            List<Invoice> invoices = await invoiceClient.GetInvoiceSummaryFromDateAsync(customerId, fromDate).ConfigureAwait(false);
            if (invoices.Count == 0) return NotFound();

            var response = new InvoiceHealthViewModel
            {
                CustomerId = customerId,
                // Set the health flag of the customers accounts
                IsHealthy = invoices.GetHealthStatus(Convert.ToInt32(_configuration["InvoiceHealthPeriodDays"])),
                // Get the recent invoice summary results, ordered descending
                InvoiceSummary = invoices.OrderByDescending(x => x.InvoiceDate).Take(summaryCount)
                    .Select(x => _mapper.Map<InvoiceSummaryItemViewModel>(x))
            };

            return Ok(response);
        }
    }
}
