using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.Products;
using TopSaladSolution.Common.Constant;
using TopSaladSolution.Interface.Services;
using TopSaladSolution.Infrastructure.Repositories;
using TopSaladSolution.Common.Enums;

namespace TopSaladSolution.Service
{
    public class ProductService : IProductService
    {
        //private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> Create(ProductCreateRequest request)
        {
            try
            {
                var newProduct = _mapper.Map<Product>(request);
                newProduct.CreatedDate = DateTime.Now;
                newProduct.ModifiedDate = DateTime.Now;

                var productTranslation = new ProductTranslation
                {
                    Name = request?.Name,
                    Description = request?.Description,
                    Details = request?.Details,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };

                newProduct.ProductTranslations.Add(productTranslation);
                await _unitOfWork.ProductRepository.Add(newProduct);
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = Message.CreatedSuccess
                };
                _logger.LogInformation($"{result.Message}: {newProduct.Id}");
                return result;
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
                _logger.LogError($"{Message.CreatedFailed}: {ex.Message}");
                return result;
            }
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            var result = await _unitOfWork.ProductRepository.GetAll();
            return _mapper.Map<List<ProductVM>>(result);
        }

        public async Task<ProductVM> GetById(int id)
        {
            var result = await _unitOfWork.ProductRepository.GetById(id);
            return _mapper.Map<ProductVM>(result);
        }

        public Task<List<ProductVM>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<object> Update(ProductEditRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                await _unitOfWork.ProductRepository.Update(product);
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = Message.Updated
                };
                _logger.LogInformation($"{result.Message}");
                return result;
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
                _logger.LogError($"{Message.UpdatedFailed} {result.Message}");
                return result;
            }
        }

        public async Task<object> SoftDelete(ProductSoftDeleteRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                request.Status = ItemStatus.InActive;
                await _unitOfWork.ProductRepository.Remove(product);
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = Message.Removed
                };
                _logger.LogInformation($"{result.Message}");
                return result;
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
                _logger.LogError($"{Message.RemovedFailed} {result.Message}");
                return result;
            }
        }
    }
}
