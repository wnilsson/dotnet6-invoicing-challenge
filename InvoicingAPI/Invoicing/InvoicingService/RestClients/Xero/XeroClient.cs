using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvoicingService.Domain.Models;
using InvoicingService.RestClients.Xero.Entities;

namespace InvoicingService.RestClients.Xero
{
    /// <summary/>
    public class XeroClient : IInvoiceClient
    {
        private readonly IMapper _mapper;

        /// <summary/>
        public XeroClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Xero invoices for companyId from date
        /// </summary>
        public async Task<List<Invoice>> GetInvoiceSummaryFromDateAsync(int companyId, DateTime fromDate)
        {
            var response = new List<Invoice>();

            // Just return filtered mock data using the dictionary for this exercise
            var data = await GetData(companyId).ConfigureAwait(false);
            if (data != null)
            {
                response = data
                    .Where(x => x.Date >= fromDate)
                    .Select(x => _mapper.Map<Invoice>(x))
                    .OrderBy(x => x.InvoiceDate)
                    .ToList();
            }

            return response;
        }

        /// <summary>
        /// ToDo - change to call Xero APIs using HttpClient instead of using mock data 
        /// </summary>
        private Task<List<XeroInvoice>> GetData(int companyId)
        {
            return Task.Run(() => XeroMockData.Data.ContainsKey(companyId) ? XeroMockData.Data[companyId].ToList() : null);
        }
    }
}
