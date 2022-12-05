using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InvoicingService.Domain;

namespace InvoicingService.RestClients.Myob
{
    /// <summary/>
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
