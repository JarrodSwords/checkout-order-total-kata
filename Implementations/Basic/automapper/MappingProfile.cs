using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;
using UnitsNet;

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

            CreateMap<Product, IProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "wat"));

            CreateMap<EachesProduct, EachesProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "eaches"));

            CreateMap<MassProduct, MassProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "mass"));

            CreateMap<EachesProduct, ProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "eaches"));

            CreateMap<MassProduct, ProductDto>()
                .ForMember(d => d.SellByType, o => o.MapFrom(s => "mass"));

            CreateMap<Mass, IMassDto>()
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Value))
                .ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit));
        }
    }
}
