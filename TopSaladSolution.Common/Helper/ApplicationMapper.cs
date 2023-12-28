using AutoMapper;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.Common.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Product, ProductVM>().ReverseMap();
        }
        
    }
}
