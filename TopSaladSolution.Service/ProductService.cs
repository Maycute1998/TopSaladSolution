using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Common.Repositories;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Interface;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IRepository<Product> productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> Create(ProductCreateRequest request)
        {
            try
            {
                var newProduct = _mapper.Map<Product>(request);
                await _productRepository!.Insert(newProduct);
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Insert Success"
                };
                _logger.LogInformation("Insert Success");
                return result;
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
                _logger.LogError($"Insert Failed {ex.Message}");
                return result;
            }
        }

        public async Task Delete(int productId)
        {
            try
            {
                var product = await _productRepository.GetById(productId);
                await _productRepository.Delete(product);
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Item has been deleted."
                };
                _logger.LogInformation($"{result.Message}");
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
                _logger.LogError($"Insert Failed {result.Message}");
            }
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            var result = await _productRepository.GetAll();
            return _mapper.Map<List<ProductVM>>(result);
        }

        public async Task<ProductVM> GetById(int id)
        {
            var result = await _productRepository.GetById(id);
            return _mapper.Map<ProductVM>(result);
        }

        public Task<List<ProductVM>> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ProductEditRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                await _productRepository.Delete(product);
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Item has been deleted."
                };
                _logger.LogInformation($"{result.Message}");
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
                _logger.LogError($"Insert Failed {result.Message}");
            }
        }
    }
}
