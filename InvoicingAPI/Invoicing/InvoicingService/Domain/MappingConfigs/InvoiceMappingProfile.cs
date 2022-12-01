using AutoMapper;
using InvoicingService.RestClients.Xero.Entities;

namespace InvoicingService.Domain.MappingConfigs
{
    /// <summary/>
    public class InvoiceMappingProfile : Profile
    {
        /// <summary/>
        public InvoiceMappingProfile()
        {
            CreateMap<XeroInvoice, InvoicingService.Domain.Invoice>()
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.OutstandingAmount, opt => opt.MapFrom(src => src.AmountDue));
        }
    }
}
