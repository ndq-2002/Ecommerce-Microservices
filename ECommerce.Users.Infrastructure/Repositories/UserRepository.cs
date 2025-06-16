using ECommerce.Users.Domain.IRepositories;
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
        public Task<int> InsertAsync(Domain.Models.User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Domain.Models.User user)
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteAsync(Domain.Models.User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> ForceDeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Models.User> GetByIdAsync(string id)
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
