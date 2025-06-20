using ECommerce.Carts.Domain.ModelMetas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Carts.Domain.IServices
{
    public interface IRedisCartService
    {
        Task<List<CartItemMeta>> GetCartItemsAsync(string userId);
        Task SetCartAsync(string userId, List<CartItemMeta> items);
        Task ClearCartAsync(string userId);
    }
}
