using ECommerce.Users.Domain.IRepositories;
using ECommerce.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        //public Task<List<UserSearchViewModel>> GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}
        public Task<int> InsertAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> ForceDeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExistEmailAsync(string id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExistUserNameAsync(string id, string username)
        {
            throw new NotImplementedException();
        }
    }
}
