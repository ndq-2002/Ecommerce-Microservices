using Ecommerce.Products.Domain.Models;
using ECommerce.Catalog.Domain.IServices;
using ECommerce.Catalog.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Catalog.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("GetAll,Insert,Update,Delete,GetDetail Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Get All Products", Description = "Requires login verification!", OperationId = "GetAllProducts", Tags = new[] { "Product" })]
        [Route("get-all/{companyId}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetAllAsync(string companyId)
        {
            var result = await _productService.GetAllAsync(companyId);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Insert Product", Description = "Requires login verification!", OperationId = "InsertProduct", Tags = new[] { "Product" })]
        [Route("insert"), AcceptVerbs("POST")]
        public async Task<IActionResult> InsertAsync([FromBody] ProductMeta productMeta)
        {
            var result = await _productService.InsertAsync("Admin", "NguyenDacQuang", productMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[Product] ProductController InsertProduct Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Update Product", Description = "Requires login verification!", OperationId = "UpdateCompany", Tags = new[] { "Product" })]
        [Route("update/{id}"), AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] ProductMeta productMeta)
        {
            var result = await _productService.UpdateAsync("Admin", "NguyenDacQuang", id, productMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[Product] ProductController UpdateProduct Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Get detail Product", Description = "Requires login verification!", OperationId = "GetDetailProduct", Tags = new[] { "Product" })]
        [Route("get-detail/{id}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetDetailAsync(string id)
        {
            var result = await _productService.GetDetailAsync(id);
            if (result.Code <= 0)
            {
                _logger.LogError("[Product] ProductController GetDetailProduct Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Delete Product", Description = "Requires login verification!", OperationId = "DeleteProduct", Tags = new[] { "Product" })]
        [Route("delete/{id}"), AcceptVerbs("DELETE")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _productService.DeleteAsync("Admin", "NguyenDacQuang", id);
            if (result.Code <= 0)
            {
                _logger.LogError("[Product] ProductController DeleteProduct Error");
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
