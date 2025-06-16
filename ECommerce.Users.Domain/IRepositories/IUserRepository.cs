using ECommerce.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Users.Domain.IRepositories
{
    public interface IUserRepository
    {
        //Task<List<UserSearchViewModel>> GetAllAsync();
        Task<int> InsertAsync(User user);
        Task<int> UpdateAsync(User user);
        Task<int> DeleteAsync(User user);
        Task<int> ForceDeleteAsync(string id);
        Task<User> GetByIdAsync(string id);
        Task<bool> CheckExistUserNameAsync(string id, string username);
        Task<bool> CheckExistEmailAsync(string id, string email);
    }
}
