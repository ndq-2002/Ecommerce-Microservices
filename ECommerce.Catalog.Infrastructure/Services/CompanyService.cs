using Ecommerce.Catalog.Domain.IRepositories;
using Ecommerce.Catalog.Domain.IServices;
using Ecommerce.Catalog.Domain.ModelMetas;
using Ecommerce.Catalog.Domain.Models;
using Ecommerce.Catalog.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<List<CompanySearchViewModel>> GetAllAsync()
        {
            return await _companyRepository.GetAllAsync();
        }
        public async Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, CompanyMeta companyMeta)
        {
            var companyId = Guid.NewGuid().ToString();

            var checkExistCode = await _companyRepository.CheckExistCodeAsync(companyId, companyMeta.Code);
            if (checkExistCode)
                return new ActionResultResponse<string>(-3, $"[Company] CompanyService: {companyMeta.Code} existed.");

            var checkExistName = await _companyRepository.CheckExistNameAsync(companyId, companyMeta.Name);
            if (checkExistName)
                return new ActionResultResponse<string>(-3, $"[Company] CompanyService: {companyMeta.Name} existed.");

            var company = new Company
            {
                Id = companyId,
                Code = companyMeta.Code,
                Name = companyMeta.Name,
                Description = companyMeta.Description,
                IsActive = companyMeta.IsActive,
                ConcurrencyStamp = companyId,
                CreateTime = DateTime.Now,
                CreatorId = creatorId,
                CreatorFullName = creatorFullName,
            };

            var result = await _companyRepository.InsertAsync(company);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, "");
            return new ActionResultResponse<string>(result, "");

        }

        public async Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string id, CompanyMeta companyMeta)
        {
            var info = await _companyRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<string>(-2, "");

            var checkExistCode = await _companyRepository.CheckExistCodeAsync(info.Id, companyMeta.Code);
            if (checkExistCode)
                return new ActionResultResponse<string>(-3, $"[Company] CompanyService: {companyMeta.Code} existed.");

            var checkExistName = await _companyRepository.CheckExistNameAsync(info.Id, companyMeta.Name);
            if (checkExistName)
                return new ActionResultResponse<string>(-3, $"[Company] CompanyService: {companyMeta.Name} existed.");

            if (info.ConcurrencyStamp != companyMeta.ConcurrencyStamp)
                return new ActionResultResponse<string>(-4, "");

            info.Code = companyMeta.Code;
            info.Name = companyMeta.Name;
            info.Description = companyMeta.Description;
            info.IsActive = companyMeta.IsActive;
            info.ConcurrencyStamp = Guid.NewGuid().ToString();
            info.LastUpdate = DateTime.Now;
            info.LastUpdateUserId = lastUpdateUserId;
            info.LastUpdateFullName = lastUpdateFullName;

            var result = await _companyRepository.UpdateAsync(info);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, "");
            return new ActionResultResponse<string>(1, "");
        }
        public async Task<ActionResultResponse<CompanyDetailViewModel>> GetDetailAsync(string id)
        {
            var info = await _companyRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse<CompanyDetailViewModel>(-2, "");

            var response = new CompanyDetailViewModel
            {
                Id = id,
                Code = info.Code,
                Name = info.Name,
                Description = info.Description,
                IsActive = info.IsActive
            };

            return new ActionResultResponse<CompanyDetailViewModel>
            {
                Code = 1,
                Data = response,
            };
        }

        public async Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string id)
        {
            var info = await _companyRepository.GetByIdAsync(id);
            if (info == null)
                return new ActionResultResponse(-2, "");

            info.DeleteTime = DateTime.Now;
            info.DeleteUserId = deleteUserId;
            info.DeleteFullName = deleteFullName;

            var result = await _companyRepository.DeleteAsync(info);
            if (result <= 0)
                return new ActionResultResponse<string>(-1, "");
            return new ActionResultResponse<string>(1, "");
        }
    }
}
