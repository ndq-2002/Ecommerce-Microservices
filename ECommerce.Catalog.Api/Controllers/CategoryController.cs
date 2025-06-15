using Ecommerce.Catalog.Domain.IServices;
using Ecommerce.Catalog.Domain.ModelMetas;
using Ecommerce.Products.Domain.Models;
using ECommerce.Catalog.Domain.IServices;
using ECommerce.Catalog.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Catalog.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("GetAll,Insert,Update,Delete,GetDetail Category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Get All Categories", Description = "Requires login verification!", OperationId = "GetAllCategories", Tags = new[] { "Category" })]
        [Route("get-all/{companyId}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetAllAsync(string companyId)
        {
            var result = await _categoryService.GetAllAsync(companyId);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Insert Category", Description = "Requires login verification!", OperationId = "InsertCategory", Tags = new[] { "Category" })]
        [Route("insert"), AcceptVerbs("POST")]
        public async Task<IActionResult> InsertAsync([FromBody] CategoryMeta categoryMeta)
        {
            var result = await _categoryService.InsertAsync("Admin", "NguyenDacQuang", categoryMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[Category] CategoryController InsertCategory Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Update Category", Description = "Requires login verification!", OperationId = "UpdateCompany", Tags = new[] { "Category" })]
        [Route("update/{id}"), AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] CategoryMeta categoryMeta)
        {
            var result = await _categoryService.UpdateAsync("Admin", "NguyenDacQuang", id, categoryMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[Category] CategoryController UpdateCategory Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Get detail Category", Description = "Requires login verification!", OperationId = "GetDetailCategory", Tags = new[] { "Category" })]
        [Route("get-detail/{id}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetDetailAsync(string id)
        {
            var result = await _categoryService.GetDetailAsync(id);
            if (result.Code <= 0)
            {
                _logger.LogError("[Category] CategoryController GetDetailCategory Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Delete Category", Description = "Requires login verification!", OperationId = "DeleteCategory", Tags = new[] { "Category" })]
        [Route("delete/{id}"), AcceptVerbs("DELETE")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _categoryService.DeleteAsync("Admin", "NguyenDacQuang", id);
            if (result.Code <= 0)
            {
                _logger.LogError("[Category] CategoryController DeleteCategory Error");
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
