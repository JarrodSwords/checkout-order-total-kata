using AutoMapper;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Markdown, MarkdownDto>();
            CreateMap<UpsertProductDto, Product>();
            CreateMap<UpsertProductMarkdownDto, Markdown>();
        }
    }
}