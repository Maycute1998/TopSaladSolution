using AutoMapper;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.Service.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductCreateRequest>().ReverseMap();
            CreateMap<Product, ProductEditRequest>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
        }
    }
}
