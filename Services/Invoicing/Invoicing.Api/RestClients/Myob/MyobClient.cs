using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AutoMapper;
using Invoicing.Api.Domain.Models;

namespace Invoicing.Api.RestClients.Myob
{
    [ExcludeFromCodeCoverage]
    public class MyobClient : IInvoiceClient
    {
        private readonly IMapper _mapper;

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
