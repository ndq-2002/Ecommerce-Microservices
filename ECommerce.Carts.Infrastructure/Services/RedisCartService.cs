using ECommerce.Carts.Domain.IServices;
using ECommerce.Carts.Domain.ModelMetas;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Carts.Infrastructure.Services
{
    public class RedisCartService : IRedisCartService
    {
        private readonly IDatabase _redisService;
        public RedisCartService(IConnectionMultiplexer redisService)
        {
            _redisService = redisService.GetDatabase();
        }

        public async Task<List<CartItemMeta>> GetCartItemsAsync(string userId)
        {
            var json = await _redisService.StringGetAsync(GetKey(userId));
            return string.IsNullOrEmpty(json)
                ? new List<CartItemMeta>()
                : JsonSerializer.Deserialize<List<CartItemMeta>>(json)!;
        }

        public async Task SetCartAsync(string userId, List<CartItemMeta> items)
        {
            var json = JsonSerializer.Serialize(items);
            await _redisService.StringSetAsync(GetKey(userId), json);
        }
        public async Task ClearCartAsync(string userId) => await _redisService.KeyDeleteAsync(GetKey(userId));

        private string GetKey(string userId) => $"cart:{userId}";
    }
}
