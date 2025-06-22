using Dapper;
using Ecommerce.Catalog.Domain.Models;
using ECommerce.Catalog.Domain.IRepositories;
using ECommerce.Catalog.Domain.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(string connectionString, ILogger<ProductRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public async Task<List<ProductSearchViewModel>> GetAll(string companyId)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@CompanyId", companyId);

                var results = await con.QueryAsync<ProductSearchViewModel>("[dbo].[spProduct_SelectAll]", commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository GetAllAsync Error: {ex.Message}");
                return [];
            }
        }
        public async Task<int> InsertAsync(Product product)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", product.Id);
                    param.Add("@CompanyId", product.CompanyId);
                    param.Add("@Name", product.Name);
                    param.Add("@Description", product.Description);
                    param.Add("@Price", product.Price);
                    param.Add("@CategoryId", product.CategoryId);
                    param.Add("@IsActive", product.IsActive);
                    param.Add("@IsDelete", product.IsDelete);
                    param.Add("@ConcurrencyStamp", product.ConcurrencyStamp);
                    param.Add("@CreateTime", product.CreateTime);
                    param.Add("@CreatorId", product.CreatorId);
                    param.Add("@CreatorFullName", product.CreatorFullName);
                    if (product.LastUpdate != null && product.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", product.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", product.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", product.LastUpdateFullName);

                    if (product.DeleteTime != null && product.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", product.DeleteTime);
                    }
                    param.Add("@DeleteUserId", product.DeleteUserId);
                    param.Add("@DeleteFullName", product.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spProduct_Insert]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository InsertAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<int> UpdateAsync(Product product)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", product.Id);
                    param.Add("@CompanyId", product.CompanyId);
                    param.Add("@Name", product.Name);
                    param.Add("@Description", product.Description);
                    param.Add("@Price", product.Price);
                    param.Add("@CategoryId", product.CategoryId);
                    param.Add("@IsActive", product.IsActive);
                    param.Add("@IsDelete", product.IsDelete);
                    param.Add("@ConcurrencyStamp", product.ConcurrencyStamp);
                    param.Add("@CreateTime", product.CreateTime);
                    param.Add("@CreatorId", product.CreatorId);
                    param.Add("@CreatorFullName", product.CreatorFullName);
                    if (product.LastUpdate != null && product.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", product.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", product.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", product.LastUpdateFullName);

                    if (product.DeleteTime != null && product.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", product.DeleteTime);
                    }
                    param.Add("@DeleteUserId", product.DeleteUserId);
                    param.Add("@DeleteFullName", product.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spProduct_Update]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository UpdateAsync Error: {ex.Message}");
                return -1;
            }
        }
        public async Task<int> DeleteAsync(Product product)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", product.Id);
                    param.Add("@DeleteUserId", product.DeleteUserId);
                    param.Add("@DeleteFullName", product.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spProduct_DeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository DeleteAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<int> ForceDeleteAsync(string id)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", id);
                    rowAffected = await con.ExecuteAsync("[dbo].[spProduct_ForceDeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository ForceDeleteAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@Id", id);
                return await con.QuerySingleOrDefaultAsync<Product>("[dbo].[spProduct_GetByID]", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository GetByIdAsync Error: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> CheckExistNameAsync(string id, string companyId, string name, string categoryId)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
				SELECT IIF (EXISTS (SELECT 1 FROM Product WHERE Id != @Id AND Name = @Name AND IsDelete = 0 AND CompanyId=@CompanyId and CategoryId=@CategoryId), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id, Name = name, CompanyId = companyId,CategoryId=categoryId });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Product] ProductRepository CheckExistNameAsync Error: {ex.Message}");
                return false;
            }
        }
    }
}
