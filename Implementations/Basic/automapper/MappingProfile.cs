using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
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
            CreateMap<EachesProduct, EachesProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "eaches"));
            CreateMap<MassProduct, MassProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "mass"));
            CreateMap<EachesProduct, ProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "eaches"));
            CreateMap<MassProduct, ProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "mass"));
        }
    }
}
