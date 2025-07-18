﻿using Ecommerce.Catalog.Domain.ModelMetas;
using Ecommerce.Catalog.Domain.Models;
using Ecommerce.Catalog.Domain.ViewModels;
using Ecommerce.Products.Domain.IRepositories;
using Ecommerce.Products.Domain.Models;
using ECommerce.Catalog.Domain.IServices;
using ECommerce.Catalog.Domain.ViewModels;
using ECommerce.Infrastructure.Messages.Core;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategorySearchViewModel>> GetAllAsync(string companyId)
        {
            return await _categoryRepository.GetAll(companyId);
        }
        public async Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, CategoryMeta categoryMeta)
        {
            var categoryId = Guid.NewGuid().ToString();
            var checkExistName = await _categoryRepository.CheckExistNameAsync(categoryId,categoryMeta.CompanyId, categoryMeta.Name);
            if (checkExistName)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists, categoryMeta.Name));

            var category = new Category
            {
                Id = categoryId,
                CompanyId = categoryMeta.CompanyId,
                Name = categoryMeta.Name,
                Description = categoryMeta.Description,
                IsActive = categoryMeta.IsActive,
                ConcurrencyStamp = categoryId,
                CreateTime = DateTime.Now,
                CreatorId = creatorId,
                CreatorFullName = creatorFullName
            };

            var result = await _categoryRepository.InsertAsync(category);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(result, SuccessMessage.GetSuccessMessage(SuccessMessage.CreateSuccessful, "category"),null,categoryId);
        }

        public async Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string id, CategoryMeta categoryMeta)
        {
            var info = await _categoryRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<string>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Category"));

            if (info.CompanyId != categoryMeta.CompanyId)
                return new ActionResultResponse<string>(-2, ErrorMessage.NotHavePermission);

            var checkExistName = await _categoryRepository.CheckExistNameAsync(info.Id,categoryMeta.CompanyId, categoryMeta.Name);
            if (checkExistName)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists, categoryMeta.Name));

            if (info.ConcurrencyStamp != categoryMeta.ConcurrencyStamp)
                return new ActionResultResponse<string>(-3, ErrorMessage.AlreadyUpdatedByAnother);


            info.CompanyId = categoryMeta.CompanyId;
            info.Name = categoryMeta.Name;
            info.Description = categoryMeta.Description;
            info.IsActive = categoryMeta.IsActive;
            info.ConcurrencyStamp = Guid.NewGuid().ToString();
            info.LastUpdate = DateTime.Now;
            info.LastUpdateUserId = lastUpdateUserId;
            info.LastUpdateFullName = lastUpdateFullName;

            var result = await _categoryRepository.UpdateAsync(info);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.UpdateSuccessful, "category"),null, id);
        }
        public async Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string id)
        {
            var info = await _categoryRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Category"));

            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = deleteUserId;
            info.DeleteFullName = deleteFullName;

            var result = await _categoryRepository.DeleteAsync(info);
            if(result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.DeleteSuccessful, "category"),null,id);
        }
        public async Task<ActionResultResponse<CategoryDetailViewModel>> GetDetailAsync(string id)
        {
            var info = await _categoryRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<CategoryDetailViewModel>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Category"));

            var response = new CategoryDetailViewModel
            {
                Id = id,
                CompanyId = info.CompanyId,
                Name = info.Name,
                Description = info.Description,
                IsActive = info.IsActive
            };

            return new ActionResultResponse<CategoryDetailViewModel>
            {
                Code = 1,
                Data = response,
            };
        }  
    }
}
