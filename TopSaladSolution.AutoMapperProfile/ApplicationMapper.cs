using AutoMapper;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.AutoMapperProfile
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //CreateMap<Product, ProductVM>()
            //    .ForMember(dest => dest.ProductTranslations, opt => opt.MapFrom(src => src));
            //CreateMap<Product, ProductVM>()
            //.ForMember(dest => dest.ProductTranslations, opt => opt.MapFrom<ICollection<ProductTranslation>>(src => src.ProductTranslations));
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<ProductTranslation, ProductTranslationVM>().ReverseMap();
            CreateMap<Product, ProductCreateRequest>().ReverseMap();
            CreateMap<Product, ProductEditRequest>().ReverseMap();
        }
    }
}