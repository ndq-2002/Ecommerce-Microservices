using ECommerce.Infrastructure.Models;
using ECommerce.Orders.Domain.Constants;
using ECommerce.Orders.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Orders.Domain.IServices
{
    public interface IOrderService
    {
        Task<List<OrderSearchViewModel>> GetAllOrdersAsync(string userId);
        Task<ActionResultResponse<OrderDetailViewModel>> GetDetailAsync(string orderId);
        Task<ActionResultResponse<string>> UpdateStatusAsync(string orderId, OrderStatus status);
    }
}
