using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopSaladSolution.Application.Interfaces;
using TopSaladSolution.Model.PagingRequest;
using TopSaladSolution.Model.Products;

namespace TopSaladSolution.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _productService.GetAllAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet(Name = "GetAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ProductPagingRequest productPagingRequest)
        {
            try
            {
                return Ok(await _productService.GetAllPaging(productPagingRequest));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _productService.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromQuery]ProductCreateRequest product)
        {
            try
            {
                var newProduct = await _productService.Create(product);
                return Ok(newProduct);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromQuery] ProductEditRequest product)
        {
            var updatedProduct = await _productService.Update(product);
            return new ObjectResult(updatedProduct);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(ProductSoftDeleteRequest product)
        {
            try
            {
                var deletedProduct = await _productService.SoftDelete(product);
                return Ok(deletedProduct);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost("ImportProducts")]
        [Authorize]
        public async Task<ImportResponse<List<ProductCreateRequest>>> ImportProductsFromFile(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return ImportResponse<List<ProductCreateRequest>>.GetResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return ImportResponse<List<ProductCreateRequest>>.GetResult(-1, "Not Support file extension");
            }

            var newProducts = await _productService.ImportProduct(formFile, cancellationToken);
            return ImportResponse<List<ProductCreateRequest>>.GetResult(0, "Import Success", newProducts);
        }
    }
}
