using AutoMapper;
using Invoicing.Api.Domain.Models;

namespace Invoicing.Api.Models.MappingConfigs
{
    public class InvoiceSummaryMappingProfile : Profile
    {
        public InvoiceSummaryMappingProfile()
        {
            CreateMap<Invoice, InvoiceSummaryItemViewModel>()
                .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.InvoiceDate.ToString("dd-MMM-yyyy")));
        }
    }
}
