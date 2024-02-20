
using Microsoft.AspNetCore.Http;
using TopSaladSolution.Model.PagingRequest;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.Interface.Services
{
    public interface IProductService
    {
        Task<object> Create(ProductCreateRequest request);
        Task<object> Update(ProductEditRequest request);
        Task<object> SoftDelete(ProductSoftDeleteRequest request);
        Task<List<ProductViewModel>> GetAllAsync();
        Task<ProductVM> GetById(int id);
        Task<PagedResult<ProductViewModel>> GetAllPaging(ProductPagingRequest productPagingRequest);
        Task<List<ProductCreateRequest>> ImportProduct(IFormFile formFile, CancellationToken cancellationToken);
        Task<int> AddImages(int productId);
        Task<int> UpdateImage(int productId, string caption, bool isDefault);

    }
}
