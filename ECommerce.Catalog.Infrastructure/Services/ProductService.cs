using Ecommerce.Catalog.Domain.ModelMetas;
using Ecommerce.Catalog.Domain.Models;
using Ecommerce.Products.Domain.IRepositories;
using Ecommerce.Products.Domain.Models;
using ECommerce.Catalog.Domain.IRepositories;
using ECommerce.Catalog.Domain.IServices;
using ECommerce.Catalog.Domain.ViewModels;
using ECommerce.Infrastructure.Messages.Core;
using ECommerce.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<ProductSearchViewModel>> GetAllAsync(string companyId)
        {
            return await _productRepository.GetAll(companyId);
        }
        public async Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, ProductMeta productMeta)
        {
            var productId = Guid.NewGuid().ToString();
            var checkExistName = await _productRepository.CheckExistNameAsync(productId, productMeta.CompanyId, productMeta.Name,productMeta.CategoryId);
            if (checkExistName)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists, productMeta.Name));

            var product = new Product
            {
                Id = productId,
                CompanyId = productMeta.CompanyId,
                Name = productMeta.Name,
                Description = productMeta.Description,
                Price = productMeta.Price,
                CategoryId = productMeta.CategoryId,
                IsActive = productMeta.IsActive,
                ConcurrencyStamp = productId,
                CreateTime = DateTime.Now,
                CreatorId = creatorId,
                CreatorFullName = creatorFullName,
            };

            var result = await _productRepository.InsertAsync(product);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(result, SuccessMessage.GetSuccessMessage(SuccessMessage.CreateSuccessful, "product"),null,productId);
        }

        public async Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string id, ProductMeta productMeta)
        {
            var info = await _productRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<string>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Product"));

            if (info.CompanyId != productMeta.CompanyId)
                return new ActionResultResponse<string>(-2, ErrorMessage.NotHavePermission);

            var checkExistName = await _productRepository.CheckExistNameAsync(id, info.CompanyId, productMeta.Name,productMeta.CategoryId);
            if (checkExistName)
                return new ActionResultResponse<string>(-4, ErrorMessage.GetErrorMessage(ErrorMessage.Exists, productMeta.Name));

            if (info.ConcurrencyStamp != productMeta.ConcurrencyStamp)
                return new ActionResultResponse<string>(-3, ErrorMessage.AlreadyUpdatedByAnother);

            info.CompanyId = productMeta.CompanyId;
            info.Name = productMeta.Name;
            info.CategoryId = productMeta.CategoryId;
            info.Description = productMeta.Description;
            info.IsActive = productMeta.IsActive;
            info.ConcurrencyStamp = Guid.NewGuid().ToString();
            info.LastUpdate = DateTime.Now;
            info.LastUpdateUserId = lastUpdateUserId;
            info.LastUpdateFullName = lastUpdateFullName;

            var result = await _productRepository.UpdateAsync(info);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.UpdateSuccessful, "product"),null,id);
        }
        public async Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string id)
        {
            var info = await _productRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Product"));

            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = deleteUserId;
            info.DeleteFullName = deleteFullName;

            var result = await _productRepository.DeleteAsync(info);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, ErrorMessage.SomethingWentWrong);
            return new ActionResultResponse<string>(1, SuccessMessage.GetSuccessMessage(SuccessMessage.DeleteSuccessful, "product"),null,id);
        }

        public async Task<ActionResultResponse<ProductDetailViewModel>> GetDetailAsync(string id)
        {
            var info = await _productRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<ProductDetailViewModel>(-5, ErrorMessage.GetErrorMessage(ErrorMessage.NotExists, "Product"));

            var response = new ProductDetailViewModel
            {
                Id = id,
                CompanyId = info.CompanyId,
                Name = info.Name,
                Description = info.Description,
                CategoryId = info.CategoryId,
                CategoryName = await _categoryRepository.GetNameAsync(info.CategoryId),
                Price = info.Price,
                IsActive = info.IsActive,
                ConcurrencyStamp = info.ConcurrencyStamp
            };

            return new ActionResultResponse<ProductDetailViewModel>
            {
                Code = 1,
                Data = response,
            };
        }

        
    }
}
