using ECommerce.Infrastructure.Constants;
using ECommerce.Orders.Domain.Models;
using ECommerce.Orders.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Orders.Domain.IRepositories
{
    public interface IOrderRepository
    {
        Task<int> UpdateStatusAsync(string orderId, OrderStatus status);
        Task<List<OrderSearchViewModel>> GetAllOrdersByUserId(string userId);
        Task<Order> GetOrderAsync(string orderId);
    }
}
