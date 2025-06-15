using Ecommerce.Catalog.Domain.IServices;
using Ecommerce.Catalog.Domain.ModelMetas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Catalog.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [SwaggerTag("GetAll,Insert,Update,Delete,GetDetail Company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Get All Companies", Description = "Requires login verification!", OperationId = "GetAllCompanies", Tags = new[] { "Company" })]
        [Route("get-all"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _companyService.GetAllAsync();
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Insert company", Description = "Requires login verification!", OperationId = "InsertCompany", Tags = new[] { "Company" })]
        [Route("insert"), AcceptVerbs("POST")]
        public async Task<IActionResult> InsertAsync([FromBody]CompanyMeta companyMeta)
        {
            var result = await _companyService.InsertAsync("Admin","NguyenDacQuang",companyMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[Company] CompanyController InsertCompany Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Update company", Description = "Requires login verification!", OperationId = "UpdateCompanies", Tags = new[] { "Company" })]
        [Route("update/{id}"), AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateAsync(string id,[FromBody]CompanyMeta companyMeta)
        {
            var result = await _companyService.UpdateAsync("Admin", "NguyenDacQuang",id, companyMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[Company] CompanyController UpdateCompany Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Get detail company", Description = "Requires login verification!", OperationId = "GetDetailCompany", Tags = new[] { "Company" })]
        [Route("get-detail/{id}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetDetailAsync(string id)
        {
            var result = await _companyService.GetDetailAsync(id);
            if (result.Code <= 0)
            {
                _logger.LogError("[Company] CompanyController GetDetailCompany Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Delete company", Description = "Requires login verification!", OperationId = "DeleteCompany", Tags = new[] { "Company" })]
        [Route("delete/{id}"), AcceptVerbs("DELETE")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _companyService.DeleteAsync("Admin", "NguyenDacQuang",id);
            if (result.Code <= 0)
            {
                _logger.LogError("[Company] CompanyController DeleteCompany Error");
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
