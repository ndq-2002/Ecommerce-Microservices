using ECommerce.Infrastructure.Models;
using ECommerce.Users.Domain.ModelMetas;
using ECommerce.Users.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Users.Domain.IServices
{
    public interface IUserService
    {
        Task<List<UserSearchViewModel>> GetAllAsync();
        Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, UserCreateMeta userCreateMeta);
        Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string id, UserUpdateMeta userUpdateMeta);
        Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string id);
        Task<ActionResultResponse<UserDetailViewModel>> GetDetailAsync(string id);
    }
}
