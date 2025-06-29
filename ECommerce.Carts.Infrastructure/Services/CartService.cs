using ECommerce.Carts.Domain.IRepositories;
using ECommerce.Carts.Domain.IServices;
using ECommerce.Carts.Domain.ModelMetas;
using ECommerce.Carts.Domain.Models;
using ECommerce.Carts.Domain.ViewModel;
using ECommerce.Infrastructure.Messages.Core;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Carts.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IRedisCartService _redisCartService;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IOrderRepository _orderRepository;

        public CartService(IRedisCartService redisCartService, ICatalogRepository catalogRepository, IOrderRepository orderRepository)
        {
            _redisCartService = redisCartService;
            _catalogRepository = catalogRepository;
            _orderRepository = orderRepository;
        }
        public async Task<ActionResultResponse<string>> InsertCartAsync(string userId, CartItemMeta cartItemMeta)
        {
            var cart = await _redisCartService.GetCartItemsAsync(userId);
            var existing = cart.FirstOrDefault(x => x.ProductId == cartItemMeta.ProductId);
            if (existing != null) existing.Quantity += cartItemMeta.Quantity;
            else cart.Add(cartItemMeta);

            var result = await _redisCartService.SetCartAsync(userId, cart);
            if (!result)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.CreateSuccessful, "cart"));
        }
        public async Task<ActionResultResponse<string>> UpdateCartAsync(string userId, CartItemMeta cartItemMeta)
        {
            var cart = await _redisCartService.GetCartItemsAsync(userId);
            var found = cart.FirstOrDefault(x => x.ProductId == cartItemMeta.ProductId);
            if (found != null) found.Quantity = cartItemMeta.Quantity;

            var result = await _redisCartService.SetCartAsync(userId, cart);
            if (!result)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.UpdateSuccessful, "cart"));
        }
        public async Task<List<CartDetailViewModel>> GetCartAsync(string userId)
        {
            return await GetDetailAsync(userId);
        }
        public async Task<ActionResultResponse> RemoveItemInCartAsync(string userId, string productId)
        {
            var cart = await _redisCartService.GetCartItemsAsync(userId);
            cart.RemoveAll(x => x.ProductId == productId);
            var result = await _redisCartService.SetCartAsync(userId, cart);
            if (!result)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.DeleteSuccessful, "item"));
        }
        public async Task<ActionResultResponse<string>> CheckoutAsync(string userId)
        {
            var orderId = Guid.NewGuid().ToString();
            var orderClient = new OrderClient
            {
                Id = orderId,
                UserId = userId,
                CreateTime = DateTime.Now,
                OrderDate = DateTime.Now,
            };

            DataTable dt = new();
            dt.Columns.Add("ProductId", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("UnitPrice", typeof(decimal));

            var listProducts = await GetDetailAsync(userId);
            foreach (var item in listProducts)
            {
                dt.Rows.Add(item.ProductId, item.Quantity, item.Price);
            }

            var result = await _orderRepository.CreateOrderByTableTypeAsync(orderClient, dt);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);

            await _redisCartService.ClearCartAsync(userId);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.CreateSuccessful, "order"),null,orderId);

        }

        public async Task<ActionResultResponse> ClearCartAsync(string userId)
        {
            var result = await _redisCartService.ClearCartAsync(userId);
            if (!result)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.DeleteSuccessful, "cart"));
        }

        private async Task<List<CartDetailViewModel>> GetDetailAsync(string userId)
        {
            var cart = await _redisCartService.GetCartItemsAsync(userId);
            var productIds = cart.Select(x => x.ProductId).ToList();
            var products = await _catalogRepository.GetDetailListPrductsAsync(String.Join(',', productIds));

            return cart.Select(i =>
            {
                var p = products.FirstOrDefault(x => x.ProductId == i.ProductId);
                return new CartDetailViewModel
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductName = p?.ProductName ?? "",
                    Price = p?.Price ?? 0
                };
            }).ToList();
        }
    }
}
