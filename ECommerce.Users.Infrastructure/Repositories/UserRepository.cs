using Dapper;
using ECommerce.Users.Domain.IRepositories;
using ECommerce.Users.Domain.Models;
using ECommerce.Users.Domain.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(string connectionString, ILogger<UserRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }
        public async Task<List<UserSearchViewModel>> GetAllAsync()
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var results = await con.QueryAsync<UserSearchViewModel>("[dbo].[spUser_SelectAll]", commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository GetAllAsync Error: {ex.Message}");
                return [];
            }
        }
        public async Task<int> InsertAsync(User user)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", user.Id);
                    param.Add("@UserName", user.UserName);
                    param.Add("@Email", user.Email);
                    param.Add("@FullName", user.FullName);
                    param.Add("@PasswordHash", user.PasswordHash);
                    param.Add("@IsActive", user.IsActive);
                    param.Add("@IsDelete", user.IsDelete);
                    param.Add("@ConcurrencyStamp", user.ConcurrencyStamp);
                    param.Add("@CreateTime", user.CreateTime);
                    param.Add("@CreatorId", user.CreatorId);
                    param.Add("@CreatorFullName", user.CreatorFullName);
                    if (user.LastUpdate != null && user.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", user.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", user.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", user.LastUpdateFullName);

                    if (user.DeleteTime != null && user.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", user.DeleteTime);
                    }
                    param.Add("@DeleteUserId", user.DeleteUserId);
                    param.Add("@DeleteFullName", user.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spUser_Insert]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository InsertAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<int> UpdateAsync(User user)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", user.Id);
                    param.Add("@UserName", user.UserName);
                    param.Add("@Email", user.Email);
                    param.Add("@FullName", user.FullName);
                    param.Add("@PasswordHash", user.PasswordHash);
                    param.Add("@IsActive", user.IsActive);
                    param.Add("@IsDelete", user.IsDelete);
                    param.Add("@ConcurrencyStamp", user.ConcurrencyStamp);
                    param.Add("@CreateTime", user.CreateTime);
                    param.Add("@CreatorId", user.CreatorId);
                    param.Add("@CreatorFullName", user.CreatorFullName);
                    if (user.LastUpdate != null && user.LastUpdate != DateTime.MinValue)
                    {
                        param.Add("@LastUpdate", user.LastUpdate);
                    }
                    param.Add("@LastUpdateUserId", user.LastUpdateUserId);
                    param.Add("@LastUpdateFullName", user.LastUpdateFullName);

                    if (user.DeleteTime != null && user.DeleteTime != DateTime.MinValue)
                    {
                        param.Add("@DeleteTime", user.DeleteTime);
                    }
                    param.Add("@DeleteUserId", user.DeleteUserId);
                    param.Add("@DeleteFullName", user.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spUser_Update]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository UpdateAsync Error: {ex.Message}");
                return -1;
            }
        }
        public async Task<int> DeleteAsync(User user)
        {
            try
            {
                int rowAffected = 0;
                using (SqlConnection con = new(_connectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        await con.OpenAsync();

                    DynamicParameters param = new();
                    param.Add("@Id", user.Id);
                    param.Add("@DeleteUserId", user.DeleteUserId);
                    param.Add("@DeleteFullName", user.DeleteFullName);
                    rowAffected = await con.ExecuteAsync("[dbo].[spUser_DeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository DeleteAsync Error: {ex.Message}");
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
                    rowAffected = await con.ExecuteAsync("[dbo].[spUser_ForceDeleteByID]", param, commandType: CommandType.StoredProcedure);
                }
                return rowAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository ForceDeleteAsync Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                DynamicParameters param = new();
                param.Add("@Id", id);
                return await con.QuerySingleOrDefaultAsync<User>("[dbo].[spUser_GetByID]", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository GetByIdAsync Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CheckExistEmailAsync(string id, string email)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
				SELECT IIF (EXISTS (SELECT 1 FROM User WHERE Id != @Id AND Email = @Email AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id, Email = email });
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository CheckExistEmailAsync Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CheckExistUserNameAsync(string id, string username)
        {
            try
            {
                using SqlConnection con = new(_connectionString);
                if (con.State == ConnectionState.Closed)
                    await con.OpenAsync();

                var sql = @"
				SELECT IIF (EXISTS (SELECT 1 FROM User WHERE Id != @Id AND UserName = @UserName AND IsDelete = 0), 1, 0)";

                var result = await con.ExecuteScalarAsync<bool>(sql, new { Id = id, UserName = username});
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[User] UserRepository CheckExistUserNameAsync Error: {ex.Message}");
                return false;
            }
        }
    }
}
