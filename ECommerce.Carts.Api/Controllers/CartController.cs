using ECommerce.Carts.Domain.IServices;
using ECommerce.Carts.Domain.ModelMetas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ECommerce.Carts.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Insert,Update,Delete,GetDetail Cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [SwaggerOperation(Summary = "Get Cart By User", Description = "Requires login verification!", OperationId = "GetCartByUser", Tags = new[] { "Cart" })]
        [Route("get-detail/{userId}"), AcceptVerbs("GET")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCartWithDetailsAsync(userId);
            return Ok(cart);
        }

        [SwaggerOperation(Summary = "Insert Cart", Description = "Requires login verification!", OperationId = "InsertCart", Tags = new[] { "Cart" })]
        [Route("add/{userId}"), AcceptVerbs("POST")]
        public async Task<IActionResult> AddItem(string userId, [FromBody] CartItemMeta item)
        {
            var result = await _cartService.InsertCartAsync(userId, item);
            if (result.Code <= 0)
            {
                _logger.LogError("[Cart] CartController InsertCartAsync error.");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Update Cart", Description = "Requires login verification!", OperationId = "UpdateCart", Tags = new[] { "Cart" })]
        [Route("update/{userId}"), AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateItem(string userId, [FromBody] CartItemMeta item)
        {
            var result = await _cartService.UpdateCartAsync(userId, item);
            if (result.Code <= 0)
            {
                _logger.LogError("[Cart] CartController UpdateCartAsync error.");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Delete Cart", Description = "Requires login verification!", OperationId = "DeleteCart", Tags = new[] { "Cart" })]
        [Route("remove/{userId}/{productId}"), AcceptVerbs("DELETE")]
        public async Task<IActionResult> RemoveItem(string userId, string productId)
        {
            var result = await _cartService.RemoveItemInCartAsync(userId, productId);
            if (result.Code <= 0)
            {
                _logger.LogError("[Cart] CartController RemoveProductInCartAsync error.");
                return BadRequest(result);
            }
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Clear Cart", Description = "Requires login verification!", OperationId = "ClearCart", Tags = new[] { "Cart" })]
        [Route("clear/{userId}"), AcceptVerbs("POST")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            var result = await _cartService.ClearCartAsync(userId);
            if (result.Code <= 0)
            {
                _logger.LogError("[Cart] CartController ClearCartAsync error.");
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
