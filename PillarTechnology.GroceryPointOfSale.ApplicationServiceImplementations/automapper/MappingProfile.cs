using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
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
