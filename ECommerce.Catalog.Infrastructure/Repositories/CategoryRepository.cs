using Dapper;
using Ecommerce.Catalog.Domain.Models;
using Ecommerce.Products.Domain.IRepositories;
using ECommerce.Catalog.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(string connectionString, ILogger<CategoryRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public async Task<List<CategorySearchViewModel>> GetAll(string companyId)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@CompanyId", companyId);

                var results = await con.QueryAsync<CategorySearchViewModel>("[dbo].[spCategory_SelectAll]", commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Category] CategoryRepository GetAllAsync Error: {ex.Message}");
                return [];
            }
        }
        public async Task<int> InsertAsync(Category category)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", category.Id);
                    param.Add("@CompanyId", category.CompanyId);
                    param.Add("@Name", category.Name);
                    param.Add("@Description", category.Description);
                    param.Add("@IsActive", category.IsActive);
                    param.Add("@IsDelete", category.IsDelete);
                    param.Add("@ConcurrencyStamp", category.ConcurrencyStamp);
                    param.Add("@CreateTime", category.CreateTime);
                    param.Add("@CreatorId", category.CreatorId);
                    param.Add("@CreatorFullName", category.CreatorFullName);
                    if (category.LastUpdate != null && category.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", category.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", category.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", category.LastUpdateFullName);

                    if (category.DeleteTime != null && category.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", category.DeleteTime);
                    }
                    param.Add("@DeleteUserId", category.DeleteUserId);
                    param.Add("@DeleteFullName", category.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spCategory_Insert]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Category] CategoryRepository InsertAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<int> UpdateAsync(Category category)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", category.Id);
                    param.Add("@CompanyId", category.CompanyId);
                    param.Add("@Name", category.Name);
                    param.Add("@Description", category.Description);
                    param.Add("@IsActive", category.IsActive);
                    param.Add("@IsDelete", category.IsDelete);
                    param.Add("@ConcurrencyStamp", category.ConcurrencyStamp);
                    param.Add("@CreateTime", category.CreateTime);
                    param.Add("@CreatorId", category.CreatorId);
                    param.Add("@CreatorFullName", category.CreatorFullName);
                    if (category.LastUpdate != null && category.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", category.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", category.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", category.LastUpdateFullName);

                    if (category.DeleteTime != null && category.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", category.DeleteTime);
                    }
                    param.Add("@DeleteUserId", category.DeleteUserId);
                    param.Add("@DeleteFullName", category.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spCategory_Update]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Category] CategoryRepository UpdateAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<int> DeleteAsync(Category category)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", category.Id);
                    param.Add("@DeleteUserId", category.DeleteUserId);
                    param.Add("@DeleteFullName", category.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spCategory_DeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Category] CategoryRepository DeleteAsync Error: {ex.Message}");
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
                    rowAffected = await con.ExecuteAsync("[dbo].[spCategory_ForceDeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Category] CategoryRepository ForceDeleteAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@Id", id);
                return await con.QuerySingleOrDefaultAsync<Category>("[dbo].[spCategory_GetByID]", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Category] CategoryRepository GetByIdAsync Error: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> CheckExistNameAsync(string id, string companyId, string name)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
				SELECT IIF (EXISTS (SELECT 1 FROM Category WHERE Id != @Id AND Name = @Name AND IsDelete = 0 AND CompanyId=@CompanyId), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id,CompanyId=companyId, Name = name });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Category] CategoryRepository CheckExistNameAsync Error: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetNameAsync(string id)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var query = "SELECT Name FROM Category WHERE Id = @Id AND IsDelete=0";
                return await con.QueryFirstOrDefaultAsync<string>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Category] GetNameAsync CategoryRepository Error.");
                return string.Empty;
            }
        }
    }
}
