using AutoMapper;
using Invoicing.Api.Domain.Models;

namespace Invoicing.Api.RestClients.Xero.Models.MappingConfigs
{
    public class XeroInvoiceMappingProfile : Profile
    {
        public XeroInvoiceMappingProfile()
        {
            CreateMap<XeroInvoice, Invoice>()
                .ForMember(dest => dest.InvoiceDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.OutstandingAmount, opt => opt.MapFrom(src => src.AmountDue));
        }
    }
}
