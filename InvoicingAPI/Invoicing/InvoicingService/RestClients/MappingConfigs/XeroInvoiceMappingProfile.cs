using AutoMapper;
using InvoicingService.Domain.Models;
using InvoicingService.RestClients.Xero.Entities;

namespace InvoicingService.RestClients.MappingConfigs
{
    /// <summary/>
    public class XeroInvoiceMappingProfile : Profile
    {
        /// <summary/>
        public XeroInvoiceMappingProfile()
        {
            CreateMap<XeroInvoice, Invoice>()
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.OutstandingAmount, opt => opt.MapFrom(src => src.AmountDue));
        }
    }
}
