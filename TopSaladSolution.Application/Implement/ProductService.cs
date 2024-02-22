using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net;
using TopSaladSolution.Application.Interfaces;
using TopSaladSolution.Common.Constant;
using TopSaladSolution.Common.Enums;
using TopSaladSolution.Common.Utilities;
using TopSaladSolution.DataAccess.Common.UnitOfWorkBase.Interfa;
using TopSaladSolution.Infrastructure.EF;
using TopSaladSolution.Infrastructure.Entities;
using TopSaladSolution.Infrastructure.Repositories;
using TopSaladSolution.Model.PagingRequest;
using TopSaladSolution.Model.Products;
using TopSaladSolution.Offices.ImportExcel;

namespace TopSaladSolution.Application.Implement
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private TopSaladDbContext _context;

        private readonly IStorageService _storageService;

        public ProductService(
            IMapper mapper,
            ILogger<ProductService> logger,
            TopSaladDbContext context,
            IStorageService storageService, 
            IUnitOfWorkPool uowPool, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _storageService = storageService;
            _uow = uowPool.Get(configuration["Systems:Pool:Default"]);
        }

        public async Task<string> SaveImage(IFormFile file)
        {
            //var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString();
            var extention = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extention}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return _storageService.GetFileUrl(fileName);
        }
        
        public async Task<object> Create(ProductCreateRequest request)
        {
            try
            {
                var context = _uow.GetRepository<Product>();
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
                    SeoTitle = request?.Name,
                    SeoAlias = request?.Name
                };

                // Save image
                if (request.ThumbnailImage != null)
                {
                    newProduct.ProductImages = new List<ProductImage>()
                    {
                        new ProductImage()
                        {
                            Caption = request.Name,
                            CreatedDate= DateTime.Now,
                            FileSize = request.ThumbnailImage.Length,
                            ImagePath = await SaveImage(request.ThumbnailImage),
                            IsDefault = true,
                            SortOrder = 1
                        }
                    };
                }

                newProduct.ProductTranslations.Add(productTranslation);
                await context.Add(newProduct);
                await _uow.SaveChangesAsync();
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
                    ex.Message
                };
                _logger.LogError($"{Message.CreatedFailed}: {ex.Message}");
                return result;
            }
        }

        //public async Task<object> Create(ProductCreateRequest request)
        //{
        //    try
        //    {
        //        var result = CreateSlide();
        //        dynamic dynamicResult = result;
        //        var status = dynamicResult.status;
        //        var message = dynamicResult.message;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        var result = new
        //        {
        //            StatusCode = HttpStatusCode.BadRequest,
        //            Message = ex.Message
        //        };
        //        _logger.LogError($"{Message.CreatedFailed}: {ex.Message}");
        //        return result;
        //    }
        //}

        //public async Task<List<ProductViewModel>> GetAllAsync()
        //{
        //    // Select product
        //    var query = from product in _context.Products
        //                join productTrans in _context.ProductTranslations on product.Id equals productTrans.ProductId
        //                join subCategory in _context.SubCategories on product.SubCategoryId equals subCategory.Id
        //                join subCategoryTrans in _context.SubCategoryTranslations on product.SubCategoryId equals subCategoryTrans.SubCategoryId
        //                join category in _context.Categories on subCategory.CategoryId equals category.Id
        //                join categoryTrans in _context.CategoryTranslations on category.Id equals categoryTrans.CategoryId

        //                select new { product, productTrans, subCategoryTrans, category, categoryTrans };

        //    // Filter
        //    var results = await query.Select(x => new ProductViewModel()
        //    {
        //        Id = x.product.Id,
        //        Name = x.productTrans.Name,
        //        SubCategoryName = x.subCategoryTrans.Name,
        //        CategoryName = x.categoryTrans.Name,
        //        Description = x.productTrans.Description,
        //        Details = x.productTrans.Details,
        //        OriginalPrice = x.product.OriginalPrice,
        //        Stock = x.product.Stock,
        //        Views = x.product.Views,
        //        Status = x.product.Status,
        //    }).ToListAsync();

        //    return results;
        //}

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            // Select product
            var query = await GetProducts();
            return query;
        }

        public async Task<ProductVM> GetById(int id)
        {
            var product = await _uow.GetRepository<Product>().GetSingleAsync(x => x.Id == id, include: x => x.Include(a => a.ProductTranslations));
            return _mapper.Map<ProductVM>(product);
        }

        public async Task<ProductVM> GetByCategoryId(int id)
        {
            var product = await _uow.GetRepository<Product>().GetSingleAsync(x => x.Id == id, include: x => x.Include(a => a.ProductTranslations));
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

            if (productPagingRequest.CategoryId != null)
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
                        Details = x.productTrans.Details,
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
               var context = _uow.GetRepository<Product>();
               var contextProductTrans = _uow.GetRepository<ProductTranslation>();
               var product = await context.GetSingleAsync(x => x.Id == request.Id);
               var productTranslation = await contextProductTrans.GetSingleAsync(x => x.ProductId == request.Id);

                if (product is null || productTranslation is null)
                {
                    throw new Exception();
                }
                productTranslation.Name = request.Name;
                productTranslation.Description = request.Description;
                productTranslation.Details = request.Details;
                product.OriginalPrice = request.OriginalPrice;

                // Update image
                if (request.ThumbnailImage != null)
                {
                    var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);

                    if (thumbnailImage is null)
                    {
                        var imgPath = await SaveImage(request.ThumbnailImage);
                        product.ProductImages = new List<ProductImage>()
                        {
                            new ProductImage()
                            {
                                Caption = request.Name,
                                CreatedDate= DateTime.Now,
                                ModifiedDate= DateTime.Now,
                                FileSize = request.ThumbnailImage.Length,
                                ImagePath = imgPath,
                                IsDefault = true,
                                SortOrder = 1
                            }
                        };
                    }
                    else
                    {
                        thumbnailImage.ModifiedDate = DateTime.Now;
                        thumbnailImage.FileSize = request.ThumbnailImage.Length;
                        thumbnailImage.ImagePath = await SaveImage(request.ThumbnailImage);
                        _context.ProductImages.Update(thumbnailImage);
                    }
                }

                context.Update(product);
                await _uow.SaveChangesAsync();
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Updated successfully"
                };
                _logger.LogInformation($"{Message.Updated} {result.Message}");
                return result;
            }
            catch (Exception ex)
            {
                var result = new
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ex.Message
                };
                _logger.LogError($"{Message.UpdatedFailed} {result.Message}");
                return result;
            }
        }

        public async Task<object> SoftDelete(ProductSoftDeleteRequest request)
        {
            try
            {
                var context = _uow.GetRepository<Product>();
                var product = _mapper.Map<Product>(request);
                request.Status = ItemStatus.InActive;
                var images = _context.ProductImages.Where(i => i.ProductId == request.Id);
                foreach (var image in images)
                {
                    await _storageService.DeleteFileAsync(image.ImagePath);
                }
                context.Delete(product);
                await _uow.SaveChangesAsync();
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
                    ex.Message
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
                var data = ImportBuilder.ImportExcel(stream);

                for (var i = 0; i < data.Rows.Count; i++)
                {
                    productList.Add(new ProductCreateRequest
                    {
                        Name = data.Rows[i]["Name"].ToString(),
                        Description = data.Rows[i]["Description"].ToString(),
                        Details = data.Rows[i]["Ingredients"].ToString(),
                        SubCategoryId = int.Parse(data.Rows[i]["SubCategoryId"].ToString()),
                        OriginalPrice = decimal.Parse(data.Rows[i]["OriginalPrice"].ToString()),
                        Stock = int.Parse(data.Rows[i]["Stock"].ToString()),
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Status = ItemStatus.Active
                    });
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
                    ex.Message
                };
                _logger.LogError($"{Message.CreatedFailed}, {ex.Message}");
            }

            return productList;
        }

        public Task<int> AddImages(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(int productId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Example Usage
        /// </summary>
        /// <returns></returns>
        private async Task<List<ProductViewModel>> GetProducts()
        {
            var result = await _uow.SQLHelper().CreateNewSqlCommand()
                .ExecuteListReaderAsync<ProductViewModel>
                (
                    sProcName: "sp_GetAllProducts"
                );
            return result;
        }

        private object CreateSlide()
        {
            _uow.SQLHelper().CreateNewSqlCommand()
                .AddParameter("P_Name", "Test 1")
                .AddParameter("P_Description", "Test 1")
                .AddParameter("P_Url", "Test 1")
                .AddParameter("P_Image", "Test 1")
                .AddParameter("P_SortOrder", 1)
                .AddParameter("P_Status", 1)
                .ExecuteNonQuery
                (
                    sProcName: "sp_Create_Slides"
                    , commandType: CommandType.StoredProcedure
                    , out var status
                    , out var message
                );
            var outStatus = status;
            var outMessage = message;
            return new
            {
                status,
                message
            };
        }
    }
}
