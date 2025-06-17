using Azure.Core;
using BCrypt.Net;
using ECommerce.Infrastructure.Messages.Core;
using ECommerce.Infrastructure.Models;
using ECommerce.Users.Domain.IRepositories;
using ECommerce.Users.Domain.IServices;
using ECommerce.Users.Domain.ModelMetas;
using ECommerce.Users.Domain.Models;
using ECommerce.Users.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Users.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserSearchViewModel>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, UserCreateMeta userCreateMeta)
        {
            var userId = Guid.NewGuid().ToString();

            var checkExistUsername = await _userRepository.CheckExistUserNameAsync(userId, userCreateMeta.UserName);
            if(checkExistUsername)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists,userCreateMeta.UserName));

            var checkExistEmail = await _userRepository.CheckExistEmailAsync(userId, userCreateMeta.Email);
            if (checkExistEmail)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists, userCreateMeta.Email));

            var user = new User
            {
                Id = userId,
                UserName = userCreateMeta.UserName,
                Email = userCreateMeta.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateMeta.Password),
                FullName = userCreateMeta.FullName,
                CreatorId = creatorId,
                CreatorFullName = creatorFullName,
                CreateTime = DateTime.Now,
                ConcurrencyStamp = userId
            };

            var result = await _userRepository.InsertAsync(user);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.CreateSuccessful,"user"));
        }

        public async Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string id, UserUpdateMeta userUpdateMeta)
        {
            var info = await _userRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<string>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "User"));

            var checkExistUsername = await _userRepository.CheckExistUserNameAsync(id, userUpdateMeta.UserName);
            if (checkExistUsername)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists, userUpdateMeta.UserName));

            var checkExistEmail = await _userRepository.CheckExistEmailAsync(id, userUpdateMeta.Email);
            if (checkExistEmail)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists, userUpdateMeta.Email));

            if (info.ConcurrencyStamp != userUpdateMeta.ConcurrencyStamp)
                return new ActionResultResponse<string>(-3, ErrorMessage.AlreadyUpdatedByAnother);

            info.UserName = userUpdateMeta.UserName;
            info.Email = userUpdateMeta.Email;
            info.FullName = userUpdateMeta.FullName;
            info.IsActive = userUpdateMeta.IsActive;
            info.ConcurrencyStamp = Guid.NewGuid().ToString();
            info.LastUpdate= DateTime.Now;
            info.LastUpdateUserId = lastUpdateUserId;
            info.LastUpdateFullName = lastUpdateFullName;

            var result = await _userRepository.UpdateAsync(info);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.CreateSuccessful, "user"));
        }
        public async Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string id)
        {
            var info = await _userRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "User"));

            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = deleteUserId;
            info.DeleteFullName = deleteFullName;

            var result = await _userRepository.DeleteAsync(info);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.DeleteSuccessful, "user"));
        }
        public async Task<ActionResultResponse<UserDetailViewModel>> GetDetailAsync(string id)
        {
            var info = await _userRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<UserDetailViewModel>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Product"));

            var response = new UserDetailViewModel
            {
                Id = id,
                UserName = info.UserName,
                Email = info.Email,
                FullName = info.FullName,
                IsActive = info.IsActive,
                ConcurrencyStamp = info.ConcurrencyStamp
            };

            return new ActionResultResponse<UserDetailViewModel>
            {
                Code = 1,
                Data = response,
            };
        }     
    }
}
