using Dapper;
using ECommerce.Carts.Domain.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Carts.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _orderConnectionString;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(string orderConnectionString, ILogger<OrderRepository> logger)
        {
            _orderConnectionString = orderConnectionString;
            _logger = logger;
        }
        public async Task<string> CreateOrderByTableTypeAsync(string userId, DataTable dt)
        {
            try
            {
                using SqlConnection con = new(_orderConnectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var param = new DynamicParameters();
                param.Add("@UserId",userId);
                // Tạo SqlParameter cho Table-Valued Parameter
                param.Add("@List", dt.AsTableValuedParameter("dbo.OrderType"));
                var result = await con.QueryFirstOrDefaultAsync<string>("[dbo].[spOrder_InsertByTableType]", param, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[dbo].[spOrder_InsertByTableType] OrderRepository InsertByTableTypeAsync Error.");
                return null;
            }
        }
    }
}
