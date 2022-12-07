using AutoMapper;
using InvoicingService.Domain.Models;
using InvoicingService.RestClients.Xero.Entities;

namespace InvoicingService.Domain.MappingConfigs
{
    /// <summary/>
    public class InvoiceMappingProfile : Profile
    {
        /// <summary/>
        public InvoiceMappingProfile()
        {
            CreateMap<XeroInvoice, Invoice>()
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.OutstandingAmount, opt => opt.MapFrom(src => src.AmountDue));
        }
    }
}
