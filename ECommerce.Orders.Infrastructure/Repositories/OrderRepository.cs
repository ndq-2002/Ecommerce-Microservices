using Dapper;
using ECommerce.Infrastructure.Constants;
using ECommerce.Orders.Domain.IRepositories;
using ECommerce.Orders.Domain.Models;
using ECommerce.Orders.Domain.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Orders.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(string connectionString, ILogger<OrderRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public Task<List<OrderSearchViewModel>> GetAllOrdersByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrderAsync(string orderId)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@Id", orderId);
                return await con.QuerySingleOrDefaultAsync<Order>("[dbo].[spOrder_SelectByID]", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[dbo].[spPosition_SelectByID] GetInfoAsync PositionRepository Error.");
                return null;
            }
        }

        public async Task<int> UpdateStatusAsync(string orderId, OrderStatus status)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@OrderId", orderId);
                    param.Add("@Status", status);

                    rowAffected = await con.ExecuteAsync("[dbo].[spOrder_UpdateStatus]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[dbo].[spOrder_UpdateStatus] UpdateStatusAsync OrderRepository Error.");
                return -1;
            }
        }
    }
}
