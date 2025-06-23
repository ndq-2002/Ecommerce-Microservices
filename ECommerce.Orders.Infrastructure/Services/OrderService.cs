using ECommerce.Infrastructure.Messages.Core;
using ECommerce.Infrastructure.Models;
using ECommerce.Orders.Domain.Constants;
using ECommerce.Orders.Domain.IRepositories;
using ECommerce.Orders.Domain.IServices;
using ECommerce.Orders.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Orders.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderSearchViewModel>> GetAllOrdersAsync(string userId)
        {
            return await _orderRepository.GetAllOrdersByUserId(userId);
        }

        public async Task<ActionResultResponse<OrderDetailViewModel>> GetDetailAsync(string orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
                return new ActionResultResponse<OrderDetailViewModel>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Order"));
        
            var detail = new OrderDetailViewModel
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                listOrders = null
            };

            return new ActionResultResponse<OrderDetailViewModel>
            {
                Code = 1,
                Data = detail,
            };
        }

        public async Task<ActionResultResponse<string>> UpdateStatusAsync(string orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
                return new ActionResultResponse<string>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Order"));

            if (order.Status != OrderStatus.Confirmed && order.Status != OrderStatus.Shipped)
                return new ActionResultResponse<string>(-99, "Order is not in a valid state for completion");

            order.Status = status;
            var result = await _orderRepository.UpdateStatusAsync(orderId, status);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.UpdateSuccessful, "order"));
        }
    }
}
