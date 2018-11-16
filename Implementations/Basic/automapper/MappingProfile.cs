using AutoMapper;
using PointOfSale.ApplicationServices;
using PointOfSale.Domain;

namespace PointOfSale.ApplicationServiceImplementations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BuyNForXAmountSpecial, BuyNForXAmountSpecialDto>();
            CreateMap<BuyNGetMAtXPercentOffSpecial, BuyNGetMAtXPercentOffSpecialDto>();
            CreateMap<Markdown, MarkdownDto>();
            CreateMap<UpsertProductArgs, Product>();
            CreateMap<UpsertProductMarkdownArgs, Markdown>();
        }
    }
}
