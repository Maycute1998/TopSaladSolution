using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Model.Products;
using TopSaladSolution.Common.Constant;
using TopSaladSolution.Interface.Services;
using TopSaladSolution.Infrastructure.Repositories;
using TopSaladSolution.Common.Enums;
using Microsoft.EntityFrameworkCore;
using TopSaladSolution.Infrastructure.EF;
using System.Linq;
using TopSaladSolution.Model.PagingRequest;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using OfficeOpenXml;

namespace TopSaladSolution.Service
{
    public class ProductService : IProductService
    {
        //private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private TopSaladDbContext _context;

        public ProductService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<ProductService> logger, 
            TopSaladDbContext context
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _context = context;
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
                    LanguageId = 1,
                    SeoTitle= request?.Name,
                    SeoAlias= request?.Name
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
            var product = await _unitOfWork.ProductRepository.GetSingleAsync(x => x.Id == id, include: x => x.Include(a => a.ProductTranslations));
            return _mapper.Map<ProductVM>(product);
        }

        public async Task<ProductVM> GetByCategoryId(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetSingleAsync(x => x.Id == id, include: x => x.Include(a => a.ProductTranslations));
            return _mapper.Map<ProductVM>(product);
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(ProductPagingRequest productPagingRequest)
        {
            // Select product
            var query = from product in _context.Products
                        join productTrans in _context.ProductTranslations on product.Id equals productTrans.ProductId
                        join subCategory in _context.SubCategories on product.SubCategoryId equals subCategory.Id
                        join subCategoryTrans in _context.SubCategoryTranslations on product.SubCategoryId equals subCategoryTrans.SubCategoryId
                        join category in _context.Categories on subCategory.CategoryId equals category.Id
                        join categoryTrans in _context.CategoryTranslations on category.Id equals categoryTrans.CategoryId

                        select new { product, productTrans, subCategoryTrans, category, categoryTrans };

            // Filter
            if (!string.IsNullOrEmpty(productPagingRequest.Keyword))
            {
                query = query.Where(x => x.productTrans.Name.Contains(productPagingRequest.Keyword));
            }

            if(productPagingRequest.CategoryId != null)
            {
                query = query.Where(x => x.category.Id == productPagingRequest.CategoryId);
            }

            int totalRow = await query.CountAsync();

            var results = query.Skip((productPagingRequest.PageIndex - 1) * productPagingRequest.PageSize)
                .Take(productPagingRequest.PageSize)
                    .Select(x => new ProductViewModel()
                     {
                        Id = x.product.Id,
                        Name = x.productTrans.Name,
                        SubCategoryName = x.subCategoryTrans.Name,
                        CategoryName = x.categoryTrans.Name,
                        Description = x.productTrans.Description,
                        OriginalPrice = x.product.OriginalPrice,
                        Stock = x.product.Stock,
                        Views = x.product.Views,
                        Status = x.product.Status,
                    });

            var pageResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                Items = await results.ToListAsync()
            };

            return pageResult;
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

        public async Task<List<ProductCreateRequest>> ImportProduct(IFormFile formFile, CancellationToken cancellationToken)
        {
            var productList = new List<ProductCreateRequest>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        productList.Add(new ProductCreateRequest
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Description = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            SubCategoryId = int.Parse(worksheet.Cells[row, 3].Value.ToString()),
                            OriginalPrice = decimal.Parse(worksheet.Cells[row, 4].Value.ToString()),
                            Stock = int.Parse(worksheet.Cells[row, 5].Value.ToString()),
                            CreatedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow,
                            Status = ItemStatus.Active
                        });
                    }
                }
            }

            try
            {
                foreach (var product in productList)
                {
                    if (product != null)
                    {
                        await Create(product);
                    }
                }
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
                _logger.LogError($"{Message.CreatedFailed}, {ex.Message}");
            }

            return productList;
        }
    }
}
