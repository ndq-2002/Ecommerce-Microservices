using Dapper;
using ECommerce.Carts.Domain.IRepositories;
using ECommerce.Carts.Domain.ViewModel;
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
    public class CatalogRepository : ICatalogRepository
    {
        private readonly string _catalogConnectionString;
        private readonly ILogger<CatalogRepository> _logger;

        public CatalogRepository(string catalogConnectionString, ILogger<CatalogRepository> logger)
        {
            _catalogConnectionString = catalogConnectionString;
        }

        public async Task<List<ProductCatalogViewModel>> GetDetailListPrductsAsync(string ids)
        {
            try
            {
                using SqlConnection con = new(_catalogConnectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@Ids", ids);

                var results = await con.QueryAsync<ProductCatalogViewModel>("[dbo].[spProduct_GetDetailListPrducts]",param, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository GetDetailListPrductsAsync Error: {ex.Message}");
                return [];
            }
        }
    }
}
