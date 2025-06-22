using ECommerce.Carts.Domain.ModelMetas;
using ECommerce.Carts.Domain.ViewModel;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Carts.Domain.IServices
{
    public interface ICartService
    {
        Task<List<CartDetailViewModel>> GetCartWithDetailsAsync(string userId);
        Task<ActionResultResponse<string>> InsertCartAsync(string userId, CartItemMeta cartItemMeta);
        Task<ActionResultResponse<string>> UpdateCartAsync(string userId, CartItemMeta cartItemMeta);
        Task<ActionResultResponse> RemoveItemInCartAsync(string userId,string productId);
        Task<ActionResultResponse<string>> CheckoutAsync(string userId);
        Task<ActionResultResponse> ClearCartAsync(string userId);
    }
}
