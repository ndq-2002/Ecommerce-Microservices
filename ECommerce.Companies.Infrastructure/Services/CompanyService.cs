using Ecommerce.Companies.Domain.IRepositories;
using Ecommerce.Companies.Domain.IServices;
using Ecommerce.Companies.Domain.ModelMetas;
using Ecommerce.Companies.Domain.Models;
using Ecommerce.Companies.Domain.ViewModels;
using ECommerce.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Companies.Infrastructure.Services
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
        public async Task<ActionResultResponse<string>> InsertAsync(string creatorId, string creatorFullName, string creatorAvatar, CompanyMeta companyMeta)
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

        public Task<ActionResultResponse<string>> UpdateAsync(string lastUpdateUserId, string lastUpdateFullName, string lastUpdateAvatar, string id, CompanyMeta companyMeta)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResultResponse<CompanyDetailViewModel>> GetDetailAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResultResponse> DeleteAsync(string deleteUserId, string deleteFullName, string deleteAvatar, string id)
        {
            throw new NotImplementedException();
        }

    }
}
