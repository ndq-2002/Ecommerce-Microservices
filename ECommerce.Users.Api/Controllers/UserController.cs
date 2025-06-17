using ECommerce.Users.Domain.IServices;
using ECommerce.Users.Domain.ModelMetas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Users.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("GetAll,Insert,Update,Delete,GetDetail User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Get All Users", Description = "Requires login verification!", OperationId = "GetAllUsers", Tags = new[] { "User" })]
        [Route("get-all/{companyId}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Insert User", Description = "Requires login verification!", OperationId = "InsertUser", Tags = new[] { "User" })]
        [Route("insert"), AcceptVerbs("POST")]
        public async Task<IActionResult> InsertAsync([FromBody] UserCreateMeta userCreateMeta)
        {
            var result = await _userService.InsertAsync("Admin", "NguyenDacQuang", userCreateMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[User] UserController InsertUser Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Update User", Description = "Requires login verification!", OperationId = "UpdateCompany", Tags = new[] { "User" })]
        [Route("update/{id}"), AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UserUpdateMeta userUpdateMeta)
        {
            var result = await _userService.UpdateAsync("Admin", "NguyenDacQuang", id, userUpdateMeta);
            if (result.Code <= 0)
            {
                _logger.LogError("[User] UserController UpdateUser Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Get detail User", Description = "Requires login verification!", OperationId = "GetDetailUser", Tags = new[] { "User" })]
        [Route("get-detail/{id}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetDetailAsync(string id)
        {
            var result = await _userService.GetDetailAsync(id);
            if (result.Code <= 0)
            {
                _logger.LogError("[User] UserController GetDetailUser Error");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Delete User", Description = "Requires login verification!", OperationId = "DeleteUser", Tags = new[] { "User" })]
        [Route("delete/{id}"), AcceptVerbs("DELETE")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _userService.DeleteAsync("Admin", "NguyenDacQuang", id);
            if (result.Code <= 0)
            {
                _logger.LogError("[User] UserController DeleteUser Error");
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
