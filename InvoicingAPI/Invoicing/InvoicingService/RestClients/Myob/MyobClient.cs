using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using InvoicingService.Domain.Models;

namespace InvoicingService.RestClients.Myob
{
    /// <summary/>
    [ExcludeFromCodeCoverage]
    public class MyobClient : IInvoiceClient
    {
        private readonly IMapper _mapper;

        /// <summary/>
        public MyobClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Get all MYOB invoices for companyId from date
        /// </summary>
        public Task<List<Invoice>> GetInvoiceSummaryFromDateAsync(int companyId, DateTime fromDate)
        {
            throw new NotImplementedException();
        }
    }
}
