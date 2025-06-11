using Ecommerce.Companies.Domain.IRepositories;
using Ecommerce.Companies.Domain.Models;
using Ecommerce.Companies.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ECommerce.Companies.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<CompanyRepository> _logger;
        public CompanyRepository(string connectionString,ILogger<CompanyRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public async Task<List<CompanySearchViewModel>> GetAllAsync()
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var results = await con.QueryAsync<CompanySearchViewModel>("[dbo].[spCompany_SelectAll]", commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository GetAllAsync Error: {ex.Message}");
                return [];
            } 
        }
        public async Task<int> InsertAsync(Company company)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", company.Id);
                    param.Add("@Code", company.Code);
                    param.Add("@Name", company.Name);
                    param.Add("@Description", company.Description);
                    param.Add("@IsActive", company.IsActive);
                    param.Add("@IsDelete", company.IsDelete);
                    param.Add("@ConcurrencyStamp", company.ConcurrencyStamp);
                    param.Add("@CreateTime", company.CreateTime);
                    param.Add("@CreatorId", company.CreatorId);
                    param.Add("@CreatorFullName", company.CreatorFullName);
                    if (company.LastUpdate != null && company.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", company.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", company.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", company.LastUpdateFullName);

                    if (company.DeleteTime != null && company.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", company.DeleteTime);
                    }
                    param.Add("@DeleteUserId", company.DeleteUserId);
                    param.Add("@DeleteFullName", company.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spCompany_Insert]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository InsertAsync Error: {ex.Message}");
                return -1;
            }
        }
        public async Task<int> UpdateAsync(Company company)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", company.Id);
                    param.Add("@Code", company.Code);
                    param.Add("@Name", company.Name);
                    param.Add("@Description", company.Description);
                    param.Add("@IsActive", company.IsActive);
                    param.Add("@IsDelete", company.IsDelete);
                    param.Add("@ConcurrencyStamp", company.ConcurrencyStamp);
                    param.Add("@CreateTime", company.CreateTime);
                    param.Add("@CreatorId", company.CreatorId);
                    param.Add("@CreatorFullName", company.CreatorFullName);
                    if (company.LastUpdate != null && company.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", company.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", company.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", company.LastUpdateFullName);

                    if (company.DeleteTime != null && company.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", company.DeleteTime);
                    }
                    param.Add("@DeleteUserId", company.DeleteUserId);
                    param.Add("@DeleteFullName", company.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spCompany_Update]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository UpdateAsync Error: {ex.Message}");
                return -1;
            }
        }
        public async Task<Company> GetByIdAsync(string id)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@Id", id);
                return await con.QuerySingleOrDefaultAsync<Company>("[dbo].[spCompany_GetByID]", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository GetByIdAsync Error: {ex.Message}");
                return null;
            }

        }
        public async Task<int> DeleteAsync(Company company)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", company.Id);
                    param.Add("@DeleteUserId", company.DeleteUserId);
                    param.Add("@DeleteFullName", company.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spCompany_DeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository DeleteAsync Error: {ex.Message}");
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
                    rowAffected = await con.ExecuteAsync("[dbo].[spCompany_ForceDeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository ForceDeleteAsync Error: {ex.Message}");
                return -1;
            }

        }

        public async Task<bool> CheckExistCodeAsync(string id, string code)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
				SELECT IIF (EXISTS (SELECT 1 FROM Companys WHERE Id != @Id AND Code = @Code AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id, Code = code });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository CheckExistCodeAsync Error: {ex.Message}");
                return false;
            }

        }

        public async Task<bool> CheckExistNameAsync(string id, string name)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
				SELECT IIF (EXISTS (SELECT 1 FROM Companys WHERE Id != @Id AND Name = @Name AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id, Name = name });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Company] CompanyRepository CheckExistNameAsync Error: {ex.Message}");
                return false;
            }
        }
    }
}
