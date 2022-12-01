using AutoMapper;
using InvoicingService.Api.Models;
using InvoicingService.Domain;

namespace InvoicingService.Models.MappingConfigs
{
    /// <summary/>
    public class InvoiceSummaryMappingProfile : Profile
    {
        /// <summary/>
        public InvoiceSummaryMappingProfile()
        {
            CreateMap<Invoice, InvoiceSummaryItemViewModel>()
                .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.InvoiceDate.ToString("dd-MMM-yyyy")));
        }
    }
}
